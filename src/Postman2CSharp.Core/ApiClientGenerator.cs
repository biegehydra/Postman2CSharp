using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Xml;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis;
using Newtonsoft.Json.Linq;
using Postman2CSharp.Core.Models;
using Postman2CSharp.Core.Models.PostmanCollection;
using Postman2CSharp.Core.Models.PostmanCollection.Http;
using Xamasoft.JsonClassGenerator;
using Xamasoft.JsonClassGenerator.CodeWriters;
using JsonSerializer = System.Text.Json.JsonSerializer;
using Xamasoft.JsonClassGenerator.CodeWriterConfiguration;
using Xamasoft.JsonClassGenerator.Models;
using Postman2CSharp.Core.Infrastructure;
using Postman2CSharp.Core.Utilities;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
#pragma warning disable CS0162 // Unreachable code detected

namespace Postman2CSharp.Core;

public class ApiClientGenerator
{
    public ApiClientGenerator(PostmanCollection collection, ApiClientGeneratorOptions options)
    {
        PostmanCollection = collection;
        Options = options;
    }

    #region ProgressCallbacks

    public event Func<string, Task>? OnStageChanged;
    public event Func<float, Task>? ProgressCallback;

    private async Task RaiseProgressCallback(float progress)
    {
        var handlers = ProgressCallback;
        if (handlers != null)
        {
            var tasks = handlers.GetInvocationList()
                .Cast<Func<float, Task>>()
                .Select(handler => handler(progress))
                .ToArray();

            await Task.WhenAll(tasks);
        }
    }
    private async Task RaiseStageChangedCallback(string stage)
    {
        var handlers = OnStageChanged;
        if (handlers != null)
        {
            var tasks = handlers.GetInvocationList()
                .Cast<Func<string, Task>>()
                .Select(handler => handler(stage))
                .ToArray();

            await Task.WhenAll(tasks);
        }
    }

    #endregion

    private PostmanCollection PostmanCollection { get; set; }
    private ApiClientGeneratorOptions Options { get; set; }

    private int _processedRequests = 0;
    private int _totalRequests = 0;
    private int TotalRequest => _totalRequests != 0 ? _totalRequests : _totalRequests = PostmanCollection
        .GetRootCollections().Sum(x => x.RequestItems()?
            .Where(y => Uri.TryCreate(y.Request?.Url.Raw.ReplaceBrackets(), UriKind.Absolute, out var uri) && 
                        (uri.Scheme == Uri.UriSchemeHttp || uri.Scheme == Uri.UriSchemeHttps)).Count() ?? 0);

    public async Task<List<ApiClient>> GenerateApiClients()
    {
        var apiClients = new List<ApiClient>();
        await RaiseStageChangedCallback("Getting roots...");

        PostmanCollection.RemoveRequestsWithoutUrl();

        PostmanCollection.CascadeAuth();

        var rootItems = PostmanCollection.GetRootCollections();


        RootItemHelpers.RemoveUnusedParameters(rootItems, Options.ApiClientOptions);

        var allVariableUsages = await DoPreProcessing(rootItems);

        if (Options.ApiClientOptions.RootDefinition == RootDefinition.PerAuthority)
        {
            rootItems = RootItemHelpers.ResortByAuthority(rootItems, PostmanCollection.Info.Name);
        }

        await RaiseStageChangedCallback("Generating ApiClients...");
        var i = 0;
        foreach (var rootItem in rootItems)
        {
            var variableUsages = allVariableUsages[i++];
            if (!rootItem.HasRequests()) continue;
            if (Options.ApiClientOptions.RootDefinition != RootDefinition.Manual && !rootItem.HasCommonAuthority())
            {
                var splitItems = RootItemHelpers.GroupRequestsByAuthority(rootItem);
                foreach (var splitItem in splitItems)
                {
                    var newRootItem = new CollectionItem()
                    {
                        Name = splitItem.Key,
                        Item = splitItem.Value,
                        Auth = rootItem.Auth
                    };
                    var splitApiClient = await CreateApiClient(newRootItem, variableUsages);
                    apiClients.Add(splitApiClient);
                }
                continue;
            }
            var apiClient = await CreateApiClient(rootItem, variableUsages);
            apiClients.Add(apiClient);
        }
        await RaiseProgressCallback(1);
        return apiClients;
    }

    private async Task<List<List<VariableUsage>>> DoPreProcessing(List<CollectionItem> rootItems)
    {
        List<List<VariableUsage>> allVariableUsages = new();
        await RaiseStageChangedCallback("Extracting variables...");
        var first = true;
        // Doing all the variable extraction and replacement here allows the TotalRequests to be accurate TODO: For some reason it's still not 100% accurate
        foreach (var rootItem in rootItems)
        {
            var variableUsages = VariableExtractor.ExtractAndReplaceVariableUsagesWithPrivateVariables(rootItem, PostmanCollection.Variable ?? new());
            allVariableUsages.Add(variableUsages);
            if (PostmanCollection.Variable != null)
            {
                VariableExtractor.ReplaceVariableUsagesInVariableUsages(PostmanCollection.Variable);
                if (first)
                {
                    first = false;
                    RootItemHelpers.ReplaceAllVariableUsagesInAllAuths(rootItem, PostmanCollection.Auth, PostmanCollection.Variable);
                }
            }

            if (Options.ApiClientOptions.MakePathCollectionVariablesFunctionParameters)
            {
                RootItemHelpers.MakePathCollectionVariablesFunctionParameters(rootItem, variableUsages);
            }
            RootItemHelpers.ReplacePathVariablesWithInterpolatedVariableUsages(rootItem);
            RootItemHelpers.FixUrlsMissingScheme(rootItem);
            RootItemHelpers.RemoveRequestsWithInvalidUrls(rootItem);
        }

        return allVariableUsages;
    }


    private async Task<ApiClient> CreateApiClient(CollectionItem rootItem, List<VariableUsage> variableUsages)
    {
        RootItemHelpers.NormalizeRequestItemNames(rootItem);

        string? ignore = PostmanCollection.GetRootCollections().Count > 1 && PostmanCollection.Info.Name.Length > 4
            ? PostmanCollection.Info.Name
            : null;

        var allCallNames = rootItem.RequestItems()?.Select(x => x.Name).ToList() ?? new ();
        var commonBase = Utils.GetCommonBase(allCallNames, ignore);
        commonBase ??= Utils.GetLongestSubstring(allCallNames, ignore);
        string normalizedNameSpace;
        if (Options.ApiClientOptions.RootDefinition == RootDefinition.Manual || commonBase == null || allCallNames.Count == 1)
        {
            normalizedNameSpace = Utils.NormalizeToCsharpPropertyName(rootItem.Name);
        }
        else
        {
            normalizedNameSpace = Utils.NormalizeToCsharpPropertyName(commonBase);
        }
        var name = normalizedNameSpace + "ApiClient";
        var leastPossibleUri = rootItem.FindLeastPossibleUri(variableUsages);

        ReplaceHttpSchemaIfInUrlAndVariableInsideOfUrl(variableUsages, leastPossibleUri);

        var commonHeaders = rootItem.GetCommonHeaders();
        var uniqueNamesList = new List<string>() { normalizedNameSpace, name };
        var (httpCalls, totalClassesGeneratedFromHttpCalls, duplicateRoots, graphQlQueriesCode) = await GetHttpCalls(rootItem, commonHeaders, normalizedNameSpace, uniqueNamesList);

        var apiClient = new ApiClient(rootItem.Description, normalizedNameSpace, leastPossibleUri, httpCalls, commonHeaders, rootItem.Auth, variableUsages,
            Options.ApiClientOptions, totalClassesGeneratedFromHttpCalls + 1, duplicateRoots, graphQlQueriesCode);
        return apiClient;
    }

    private static void ReplaceHttpSchemaIfInUrlAndVariableInsideOfUrl(List<VariableUsage> variableUsages, string? leastPossibleUri)
    {
        if (leastPossibleUri != null)
        {
            var lower = leastPossibleUri.ToLower();
            if (GetContentInBrackets(lower) is { } variableName)
            {
                if (variableUsages.FirstOrDefault(x =>
                        x.CSPropertyUsage(CsharpPropertyType.Private).ToLower() == variableName) is
                    { } matchingVariableUsage)
                {
                    matchingVariableUsage.Value = matchingVariableUsage.Value?.ToLower().Replace("https://", string.Empty)
                        .Replace("https://", string.Empty);
                }
            }

            static string? GetContentInBrackets(string input)
            {
                string pattern = @"https?://\{([^}]*)\}";
                Match m = Regex.Match(input, pattern);

                if (m.Success)
                {
                    return m.Groups[1].Value; // return the first capturing group
                }

                return null;
            }
        }
    }

    private async Task<(List<HttpCall> HttpCalls, int TotalClassesGenerated, List<DuplicateRoot> DuplicateRoots, string? GraphQLQueriesCode)> GetHttpCalls(CollectionItem item, List<Header> commonHeaders, string nameSpace, List<string> uniqueNames)
    {
        List<DuplicateRoot> duplicateRoots = new();
        List<HttpCall> httpCalls = new ();
        var requestItems = item.RequestItems();
        var writeFormDataComments = Options.ApiClientOptions.XmlCommentTypes.Contains(XmlCommentTypes.FormData);
        var totalClassesGenerated = 0;
        StringBuilder? queriesSb = null;
        bool firstGraphQl = true;
        if (Options.ApiClientOptions.GraphQLQueriesInSeperateFile)
        {
            queriesSb = new();
            queriesSb.AppendLine($"public static class {nameSpace}GraphQLQueries");
            queriesSb.AppendLine("{");
        }
        if(requestItems == null) return (httpCalls, totalClassesGenerated, duplicateRoots, null);

        var jsonClassGenerator = ClassGenerator();
        foreach (var requestItem in requestItems)
        {
            if (!Options.ApiClientOptions.HandleMultipleResponses)
            {
                requestItem.Response?.RemoveAll(x => !x.IsSuccessCode);
            }
            var requestDataType = Utils.GetRequestDataType(requestItem.Request!);
            var normalizedName = requestItem.NormalizedName();
            requestItem.Description = requestItem.Description.HtmlToPlainText();
            // If the ApiClient is Geolocate and the request is Geolocate, it's fine to have the same name here
            var uniqueName = normalizedName;
            if (normalizedName != nameSpace)
            {
                uniqueName = Utils.GenerateUniqueName(normalizedName, uniqueNames);
            }

            requestItem.Request!.Description = requestItem.Request.Description.HtmlToPlainText();
            string? requestSourceCode = null;
            string? requestClassName = null;
            bool requestRootWasArray = false;
            List<ClassType>? requestTypes = null;
            if (requestDataType is DataType.Json)
            {
                var json = requestItem.Request!.Body!.Raw ?? "";

                // Will fix any issues caused by collection variables
                json = VariableExtractor.ReplaceVariablesWith1(json);

                if (string.IsNullOrWhiteSpace(json))
                {
                    requestClassName = "EmptyRequest";
                }
                else
                {
                    try
                    {
                        ProcessJson(jsonClassGenerator, json, uniqueName, Consts.Request, ref requestClassName,
                            ref requestSourceCode, ref requestTypes, ref requestRootWasArray);
                    }
                    catch (Newtonsoft.Json.JsonException)
                    {
                        requestClassName = "EmptyRequest";
                        requestSourceCode = null;
                        requestTypes = null;
                        requestRootWasArray = false;
                    }
                    catch (NoClassesGeneratedException)
                    {
#if DEBUG
                        Debug.WriteLine($"Request no classes generated. {uniqueName}");
#endif
                        Reset();
                    }
                    catch (DuplicateRootException ex)
                    {
                        AddDuplicateRootUsage(ex.OriginalRootName, ex.OriginalIsRoot, requestClassName, uniqueName,
                            GeneratedClassType.Request);
                        requestClassName = ex.OriginalRootName;
                        requestRootWasArray = ex.DuplicateIsArray;
                        requestSourceCode = null;
                        requestTypes = null;
                    }
                    catch (XmlException ex)
                    {
#if DEBUG
                        Debug.WriteLine($@"{uniqueName} - Xml exception. Message: {ex.Message}, Json: {json}");
#endif
                        Reset();
                    }
                    catch (TextWasNotJsonException ex)
                    {
#if DEBUG
                        Debug.WriteLine($@"{uniqueName} - TextWasNotJsonException. Message: {ex.Message}, Text: {ex.Text}");
#endif

                        Reset();
                    }
                    catch (Exception ex)
                    {
                        Reset();
#if DEBUG
                        Debug.WriteLine(ex);
                        throw;
                        #endif
                    }
                    void Reset()
                    {
                        requestClassName = null;
                        requestSourceCode = null;
                        requestTypes = null;
                        requestRootWasArray = false;
                    }
                }
            }

            string? graphQlVariablesSourceCode = null;
            string? graphQlVariablesClassName = null;
            bool graphQlVariablesRootWasArray = false;
            List<ClassType>? graphQlVariablesTypes = null;
            if (requestDataType is DataType.GraphQl)
            {
                if (Options.ApiClientOptions.GraphQLQueriesInSeperateFile)
                {
                    if (!firstGraphQl)
                    {
                        queriesSb!.AppendLine();
                    }
                    queriesSb!.AppendLine(Consts.Indent(1) + $"public const string {uniqueName} = @\"\n{HttpUtility.JavaScriptStringEncode(requestItem.Request.Body!.Graphql!.Query).Replace(@"\r\n", "\n").Replace(@"\n", "\n").Replace("\\\"", "\"\"")}\";");
                    firstGraphQl = false;
                }
                var temp = Options.CSharpCodeWriterConfig.AttributeUsage;
                Options.CSharpCodeWriterConfig.AttributeUsage = JsonPropertyAttributeUsage.Always;
                var json = requestItem.Request.Body!.Graphql!.Variables;

                // Will fix any issues caused by collection variables
                json = VariableExtractor.ReplaceVariablesWith1(json);

                if (!string.IsNullOrWhiteSpace(json))
                {
                    try
                    {
                        
                        ProcessJson(jsonClassGenerator, json, uniqueName, Consts.GraphQLVariables, ref graphQlVariablesClassName,
                            ref graphQlVariablesSourceCode, ref graphQlVariablesTypes, ref graphQlVariablesRootWasArray);
                    }
                    catch (Newtonsoft.Json.JsonException)
                    {
#if DEBUG
                        Debug.WriteLine($"JsonException no classes generated. {uniqueName}");
#endif
                        Reset();
                    }
                    catch (NoClassesGeneratedException)
                    {
#if DEBUG
                        Debug.WriteLine($"Request no classes generated. {uniqueName}");
#endif
                        Reset();
                    }
                    catch (DuplicateRootException ex)
                    {
                        AddDuplicateRootUsage(ex.OriginalRootName, ex.OriginalIsRoot, graphQlVariablesClassName, uniqueName,
                            GeneratedClassType.Request);

                        graphQlVariablesClassName = ex.OriginalRootName;
                        graphQlVariablesRootWasArray = ex.DuplicateIsArray;
                        graphQlVariablesSourceCode = null;
                        graphQlVariablesTypes = null;
                    }
                    catch (XmlException ex)
                    {
#if DEBUG
                        Debug.WriteLine($@"{uniqueName} - Xml exception. Message: {ex.Message}, Json: {json}");
#endif
                        Reset();
                    }
                    catch (TextWasNotJsonException ex)
                    {
#if DEBUG
                        Debug.WriteLine($@"{uniqueName} - TextWasNotJsonException. Message: {ex.Message}, Text: {ex.Text}");
#endif

                        Reset();
                    }
                    catch (Exception ex)
                    {
                        Reset();
#if DEBUG
                        Debug.WriteLine(ex);
                        throw;
#endif
                    }
                    void Reset()
                    {
                        graphQlVariablesClassName = null;
                        graphQlVariablesSourceCode = null;
                        graphQlVariablesTypes = null;
                        graphQlVariablesRootWasArray = false;
                    }
                }

                Options.CSharpCodeWriterConfig.AttributeUsage = temp;
            }

            string? formClassName = null;
            string? formClassSourceCode = null;
            if (requestDataType is DataType.ComplexFormData or DataType.SimpleFormData)
            {
                formClassName = uniqueName + (requestDataType is DataType.ComplexFormData ? Consts.Classes.MultipartFormData : Consts.Classes.FormData);
                formClassName = Utils.GenerateUniqueName(formClassName, uniqueNames);
                if (requestItem.Request!.Body!.Formdata != null)
                {
                    var iFormData = requestItem.Request!.Body!.Formdata.Cast<IFormData>().Where(x => !string.IsNullOrWhiteSpace(x.Key)).ToList();
                    formClassSourceCode = GenerateFormDataClass(formClassName, iFormData, requestDataType, nameSpace, writeFormDataComments);
                    totalClassesGenerated++;
                }
                else if (requestItem.Request!.Body!.Urlencoded != null)
                {
                    var iFormData = requestItem.Request!.Body!.Urlencoded.Cast<IFormData>().Where(x => !string.IsNullOrWhiteSpace(x.Key)).ToList();
                    formClassSourceCode = GenerateFormDataClass(formClassName, iFormData, DataType.SimpleFormData, nameSpace, writeFormDataComments);
                    totalClassesGenerated++;
                }
            }

            var allResponses = requestItem.Response?.Where(x => x.Code.HasValue).GroupBy(x => x.Code!.Value).Select(x => x.First()).ToList() ?? new ();
            var allApiResponse = new List<ApiResponse>();
            foreach (var response in allResponses)
            {
                if (response.Code == null) continue;

                var responseDataType = Utils.GetResponseDataType(response);
                if (responseDataType == DataType.Json)
                {
                    string? responseSourceCode = null;
                    string? responseClassName = null;
                    bool rootWasArray = false;
                    var json = response.Body;
                    if (string.IsNullOrWhiteSpace(json))
                    {
                        allApiResponse.Add(new ApiResponse(response.Code.Value, null, null, DataType.Binary, false));
                        continue;
                    }

                    try
                    {
                        List<ClassType>? _ = null;
                        var type = allResponses.Count > 1
                            ? (HttpStatusCode) response.Code + Consts.Response
                            : Consts.Response;
                        ProcessJson(jsonClassGenerator, json, uniqueName, type, ref responseClassName,
                            ref responseSourceCode, ref _, ref rootWasArray);
                    }
                    catch (Newtonsoft.Json.JsonException)
                    {
                        responseClassName = "EmptyResponse";
                        allApiResponse.Add(new ApiResponse(response.Code.Value, responseClassName, sourceCode: null, DataType.Json, false));
                        continue;
                    }
                    catch (NoClassesGeneratedException)
                    {
                        responseClassName = "EmptyResponse";
                        allApiResponse.Add(new ApiResponse(response.Code.Value, responseClassName, sourceCode: null, DataType.Json, false));
#if DEBUG
                        Debug.WriteLine($@"Response no classes generated. {requestItem.Name}");
#endif
                        continue;
                    }
                    catch (DuplicateRootException ex)
                    {
                        AddDuplicateRootUsage(ex.OriginalRootName, ex.OriginalIsRoot, responseClassName, uniqueName, GeneratedClassType.Response);

                        responseClassName = ex.OriginalRootName;
                        allApiResponse.Add(new ApiResponse(response.Code.Value, responseClassName, sourceCode: null, DataType.Json, ex.DuplicateIsArray));
                        continue;
                    }
                    catch (XmlException ex)
                    {
#if DEBUG
                        Debug.WriteLine($@"{uniqueName} - Xml exception. Message: {ex.Message}, Json: {json}");
#endif
                        responseClassName = "EmptyResponse";
                        allApiResponse.Add(new ApiResponse(response.Code.Value, responseClassName, sourceCode: null, DataType.Json, false));
                    }
                    catch (TextWasNotJsonException ex)
                    {
#if DEBUG
                        Debug.WriteLine($@"{uniqueName} - TextWasNotJsonException. Message: {ex.Message}, Text: {ex.Text}");
#endif
                        responseClassName = "EmptyResponse";
                        allApiResponse.Add(new ApiResponse(response.Code.Value, responseClassName, sourceCode: null, DataType.Json, false));
                    }
                    catch (Exception ex)
                    {
                        responseClassName = "EmptyResponse";
                        allApiResponse.Add(new ApiResponse(response.Code.Value, responseClassName, sourceCode: null, DataType.Json, false));
#if DEBUG
                        Debug.WriteLine(ex);
                       throw;
#endif
                        continue; // do not delete
                    }
                    allApiResponse.Add(new ApiResponse(response.Code.Value, responseClassName, responseSourceCode, DataType.Json, rootWasArray));
                    continue;
                }
                else
                {
                    allApiResponse.Add(new ApiResponse(response.Code.Value, null, null, DataType.Binary, false));
                }
            }
            // If there is no success response, add one
            if (!allApiResponse.Any(x => x.IsSuccessCode))
            {
                allApiResponse.Add(new ApiResponse(200, null, null, DataType.Binary, false));
            }

            string? queryParametersSourceCode = null;
            string? queryParametersClassName = null;
            List<ClassType>? queryParameterTypes = null;
            if (requestItem.Request!.Url.Query is {Count: > 0})
            {
                var jObject = new JObject();
                var queryParameters = requestItem.Request!.Url.Query.Where(x => !string.IsNullOrWhiteSpace(x.Key)).ToList();
                foreach (var queryParameter in queryParameters)
                {
                    jObject[queryParameter.Key] = "test";
                }

                var descriptionDict = queryParameters
                    .GroupBy(x => x.Key)
                    .ToDictionary(
                        group => group.Key,
                        group => group.First().Description?.HtmlToPlainText()
                    );
                var queryParametersAsJson = jObject.ToString();
                try
                {
                    bool _ = false;
                    ProcessJson(jsonClassGenerator, queryParametersAsJson, uniqueName, Consts.Parameters,
                        ref queryParametersClassName, ref queryParametersSourceCode, ref queryParameterTypes, ref _,
                        descriptionDict);
                }
                catch (NoClassesGeneratedException)
                {
#if DEBUG
                    Debug.WriteLine($@"Query parameters no classes generated. {requestItem.Name}");
#endif
                    queryParametersClassName = null;
                    queryParametersSourceCode = null;
                }
                catch (DuplicateRootException ex)
                {
                    AddDuplicateRootUsage(ex.OriginalRootName, ex.OriginalIsRoot, queryParametersClassName, uniqueName,
                        GeneratedClassType.QueryParameters);
                    queryParametersClassName = ex.OriginalRootName;
                    queryParametersSourceCode = null;
                }
                catch (XmlException ex)
                {
                    queryParametersClassName = null;
                    queryParametersSourceCode = null;
#if DEBUG
                    Debug.WriteLine($@"{uniqueName} - Xml exception. Message: {ex.Message}, Json: {queryParametersAsJson}");
#endif
                }
                catch (TextWasNotJsonException ex)
                {
                    queryParametersClassName = null;
                    queryParametersSourceCode = null;
#if DEBUG
                    Debug.WriteLine($@"{uniqueName} - TextWasNotJsonException. Message: {ex.Message}, Text: {ex.Text}");
#endif
                }
                catch (Exception ex)
                {
                    queryParametersClassName = null;
                    queryParametersSourceCode = null;
#if DEBUG
                    Debug.WriteLine(ex);
                    throw;
#endif
                }
            }

            var uniqueHeaders = requestItem.Request.Header.Except(commonHeaders).ToList();

            var successResponseDataType = allApiResponse.FirstOrDefault()?.DataType ?? DataType.Null;
            // Multiple api responses or the only api response is not a success response
            var multipleResponse = allApiResponse.Count > 1;
            var httpClientFunction = Utils.HttpClientCall(requestItem.Request.Method, requestDataType, successResponseDataType, Options.CSharpCodeWriterConfig.AttributeLibrary, multipleResponse);

            httpCalls.Add(new ()
            {
                Name = uniqueName,
                HttpClientFunction = httpClientFunction,
                Request = requestItem.Request!,
                RequestDataType = requestDataType,
                RequestClassName = requestClassName,
                RequestSourceCode = requestSourceCode,
                RequestRootWasArray = requestRootWasArray,
                AllResponses = allApiResponse,
                QueryParameterClassName = queryParametersClassName,
                QueryParameterSourceCode = queryParametersSourceCode,
                FormDataClassName = formClassName,
                FormDataSourceCode = formClassSourceCode,
                UniqueHeaders = uniqueHeaders,
                RequestTypes = requestTypes,
                QueryParameterTypes = queryParameterTypes,
                GraphQlVariablesClassName = graphQlVariablesClassName,
                GraphQlVariablesSourceCode = graphQlVariablesSourceCode,
                GraphQlVariablesRootWasArray = graphQlVariablesRootWasArray,
                GraphQlVariablesTypes = graphQlVariablesTypes
            });

            _processedRequests += 1;
            if (_processedRequests > TotalRequest) 
                throw new Exception("Something went wrong. Processed requests greater than total requests.");
            await RaiseProgressCallback((float) _processedRequests / TotalRequest);
        }
        if (Options.ApiClientOptions.GraphQLQueriesInSeperateFile)
        {
            queriesSb!.AppendLine("}");
        }

        return (httpCalls, totalClassesGenerated, duplicateRoots, queriesSb?.ToString());
        
        void ProcessJson(JsonClassGenerator classGenerator, string json, string itemName, string itemType, ref string? className, ref string? sourceCode, 
            ref List<ClassType>? types, ref bool rootWasArray, Dictionary<string, string?>? descriptionDict = null)
        {
            className = Utils.GenerateUniqueName(itemName + itemType, uniqueNames);
            var writeComments = Options.ApiClientOptions.XmlCommentTypes.Contains(XmlCommentTypes.QueryParameters);
            classGenerator.SetRootName(className);
            if (itemType == Consts.Parameters)
            {
                classGenerator.CodeWriter = ParametersConfig(nameSpace, writeComments);
                classGenerator.SetCurrentRootIsQueryParameters(true);
            }
            else
            {
                var codeWriterClone = Options.CSharpCodeWriterConfig.Clone();
                codeWriterClone.Namespace = nameSpace;
                classGenerator.CodeWriter = new CSharpCodeWriter(codeWriterClone, writeComments);
                classGenerator.SetCurrentRootIsQueryParameters(false);
            }
            classGenerator.SetDescriptionDict(descriptionDict);
            (sourceCode, var classCount, rootWasArray) = GenerateClasses(classGenerator, json, ref types, className, descriptionDict?.Count > 0);
            totalClassesGenerated += classCount;
            if (sourceCode == string.Empty)
            {
                if (JsonSerializer.Deserialize<List<string>>(json) is { })
                {
                    className = "List<string>";
                    sourceCode = null;
                }
                else
                {
                    className = null;
                }
            }

            CSharpCodeWriter ParametersConfig(string nameSpacee, bool writeDescriptions)
            {
                var config = new CSharpCodeWriterConfig
                {
                    AttributeUsage = JsonPropertyAttributeUsage.Never,
                    Namespace = nameSpacee
                };
                return new CSharpCodeWriter(config, writeDescriptions);
            }
        }

        void AddDuplicateRootUsage(string originalClassName, bool originalIsRoot, string? intendedClassName, string requestItemName, GeneratedClassType classType)
        {
            if (duplicateRoots.All(x => x.ClassName != originalClassName))
            {
                duplicateRoots.Add(new DuplicateRoot(originalClassName, originalIsRoot, new List<DuplicateRootUsage>(), !originalIsRoot));
            }

            var duplicateRoot = duplicateRoots.First(x => x.ClassName == originalClassName);
            duplicateRoot.Usages.Add(new DuplicateRootUsage(requestItemName, intendedClassName, classType));
        }
    }

    private JsonClassGenerator ClassGenerator()
    {
        return new JsonClassGenerator(CSharpCodeWriter(), Options.ApiClientOptions.DuplicateOptions);
    }

    private CSharpCodeWriter CSharpCodeWriter()
    {
        var writeComments = Options.ApiClientOptions.XmlCommentTypes.Contains(XmlCommentTypes.QueryParameters);
        return new CSharpCodeWriter(Options.CSharpCodeWriterConfig, writeComments);
    }

    private static (string SourceCode, int ClassCount, bool RootWasArray) GenerateClasses(JsonClassGenerator jsonClassGenerator, string json, ref List<ClassType>? types, string rootClassName, bool hasDescriptions)
    {
        var (sb, rootWasArray) = jsonClassGenerator.GenerateClasses(json, false, false, out _);
        types = jsonClassGenerator.Types?.Select(x => new ClassType()
        {
            AssignedName = x.NewAssignedName,
            Fields = x.Fields.Select(y => new Field()
            {
                MemberName = y.MemberName,
                JsonMemberName = y.JsonMemberName
            }).ToList()
        }).ToList();
        var fixedSourceCode = CodeAnalysisUtils.ConsolidateAndReorder(sb.ToString(), rootClassName, out var classCount);
        if (hasDescriptions)
        {
            fixedSourceCode = fixedSourceCode.FixXmlCommentsAfterCodeAnalysis(2);
        }
        return (fixedSourceCode, classCount, rootWasArray);
    }

    private static string? GenerateFormDataClass (string? formClassName, List<IFormData>? formdatas, DataType? dataType, string nameSpace, bool writeFormDataComments)
    {
        if (formClassName == null || formdatas == null || dataType == null) return null;

        var formDataInterface = dataType switch
        {
            DataType.ComplexFormData => "IMultipartFormData",
            DataType.SimpleFormData => "IFormData",
            _ => throw new ArgumentException("Invalid data type")
        };
        var rootStr = $@"using System;
using System.Collections.Generic;
using System.Net.Http;
namespace {nameSpace}
{{
    public class {formClassName} : {formDataInterface}
    {{
    }}
}}";

        SyntaxTree tree = CSharpSyntaxTree.ParseText(rootStr);
        List<string> uniqueKeys = new ();
        var root = (CompilationUnitSyntax)tree.GetRoot();

        var newRoot = root.ReplaceNodes(root.DescendantNodes().OfType<ClassDeclarationSyntax>(),
            (node, _) =>
            {
                if (node.Identifier.Text == formClassName)
                {
                    int files = 1;
                    // Adding properties
                    foreach (var formData in formdatas)
                    {

                        string comment = "";
                        var csPropertyName = formData.CsPropertyName(CsharpPropertyType.Public);
                        var uniqueName = CreateUniqueName(csPropertyName);
                        if (writeFormDataComments && !string.IsNullOrWhiteSpace(formData.Description))
                        {
                            comment = XmlCommentHelpers.ToXmlSummary(formData.Description, Consts.Indent(3));
                        }
                        if (formData.FormDataType == FormDataType.Text)
                        {
                            var prop = SyntaxFactory.PropertyDeclaration(SyntaxFactory.ParseTypeName("string"), uniqueName)
                                .AddModifiers(SyntaxFactory.Token(SyntaxKind.PublicKeyword))
                                .AddAccessorListAccessors(
                                    SyntaxFactory.AccessorDeclaration(SyntaxKind.GetAccessorDeclaration).WithSemicolonToken(SyntaxFactory.Token(SyntaxKind.SemicolonToken)),
                                    SyntaxFactory.AccessorDeclaration(SyntaxKind.SetAccessorDeclaration).WithSemicolonToken(SyntaxFactory.Token(SyntaxKind.SemicolonToken)))
                                .WithLeadingTrivia(SyntaxFactory.ParseLeadingTrivia(comment));

                            node = node.AddMembers(prop);
                        }

                        if (formData.FormDataType == FormDataType.File)
                        {
                            var fileNumberText = files > 1 ? files.ToString() : null;
                            var propFile = SyntaxFactory.PropertyDeclaration(SyntaxFactory.ParseTypeName("StreamContent"), $"File{fileNumberText}")
                                .AddModifiers(SyntaxFactory.Token(SyntaxKind.PublicKeyword))
                                .AddAccessorListAccessors(
                                    SyntaxFactory.AccessorDeclaration(SyntaxKind.GetAccessorDeclaration).WithSemicolonToken(SyntaxFactory.Token(SyntaxKind.SemicolonToken)),
                                    SyntaxFactory.AccessorDeclaration(SyntaxKind.SetAccessorDeclaration).WithSemicolonToken(SyntaxFactory.Token(SyntaxKind.SemicolonToken)))
                                .WithLeadingTrivia(SyntaxFactory.ParseLeadingTrivia(comment));

                            var propFileName = SyntaxFactory.PropertyDeclaration(SyntaxFactory.ParseTypeName("string"), $"FileName{fileNumberText}")
                                .AddModifiers(SyntaxFactory.Token(SyntaxKind.PublicKeyword))
                                .AddAccessorListAccessors(
                                    SyntaxFactory.AccessorDeclaration(SyntaxKind.GetAccessorDeclaration).WithSemicolonToken(SyntaxFactory.Token(SyntaxKind.SemicolonToken)),
                                    SyntaxFactory.AccessorDeclaration(SyntaxKind.SetAccessorDeclaration).WithSemicolonToken(SyntaxFactory.Token(SyntaxKind.SemicolonToken)))
                                .WithLeadingTrivia(SyntaxFactory.ParseLeadingTrivia(comment));

                            node = node.AddMembers(propFile, propFileName);
                            files++;
                        }
                    }

                    uniqueKeys = new();
                    if (formDataInterface == "IMultipartFormData")
                    {
                        files = 0;
                        string formDataContent = string.Join(",\n",
                            formdatas.Select(x =>
                            {
                                if (x.FormDataType == FormDataType.File) files++;
                                var fileNumberText = files > 1 ? files.ToString() : null;
                                var csPropertyName = x.CsPropertyName(CsharpPropertyType.Public);
                                var uniqueName = CreateUniqueName(csPropertyName);
                                return x.FormDataType == FormDataType.Text
                                    ? $"{{ new StringContent({uniqueName}), \"{x.Key}\" }}"
                                    : $"{{ File{fileNumberText}, \"{x.Key}\", FileName{fileNumberText} }}";
                            }));

                        var toFormDataComplexMethod = SyntaxFactory.MethodDeclaration(SyntaxFactory.ParseTypeName("MultipartFormDataContent"), "ToMultipartFormData")
                            .AddModifiers(SyntaxFactory.Token(SyntaxKind.PublicKeyword))
                            .WithBody(SyntaxFactory.Block(SyntaxFactory.ParseStatement(
                                $"return new MultipartFormDataContent()\n{{\n{formDataContent}\n}};"
                            )));

                        node = node.AddMembers(toFormDataComplexMethod);
                    }
                    else 
                    {
                        string formDataContent = string.Join(",\n",
                            formdatas.Select(x =>
                            {
                                var csPropertyName = x.CsPropertyName(CsharpPropertyType.Public);
                                var uniqueName = CreateUniqueName(csPropertyName);
                                return $"new (\"{x.Key}\", {uniqueName} )";
                            }));

                        var toFormDataSimpleMethod = SyntaxFactory.MethodDeclaration(SyntaxFactory.ParseTypeName("FormUrlEncodedContent"), "ToFormData")
                            .AddModifiers(SyntaxFactory.Token(SyntaxKind.PublicKeyword))
                            .WithBody(SyntaxFactory.Block(SyntaxFactory.ParseStatement(
                                $"return new FormUrlEncodedContent(new KeyValuePair<string, string>[]\n{{\n{formDataContent}\n}});"
                            )));

                        node = node.AddMembers(toFormDataSimpleMethod);
                    }
                }

                return node;
            });
        return newRoot.NormalizeWhitespace().ToFullString().FixXmlCommentsAfterCodeAnalysis(2);

        string CreateUniqueName(string name)
        {
            while (uniqueKeys.Contains(name))
            {
                name = Utils.IncrementString(name);
            }
            uniqueKeys.Add(name);
            return name;
        }
    }
}