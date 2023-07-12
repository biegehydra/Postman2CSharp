using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
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
using Postman2CSharp.Core.Models.PostmanCollection.Http.Response;
using Postman2CSharp.Core.Utilities;
using System.Net;

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
    private int TotalRequest => _totalRequests != 0 ? _totalRequests : _totalRequests = PostmanCollection.GetRootCollections().Sum(x => x.RequestItems()?.Where(x => Uri.TryCreate(x.Request?.Url.Raw.ReplaceBrackets(), UriKind.Absolute, out var uri) && (uri.Scheme == Uri.UriSchemeHttp || uri.Scheme == Uri.UriSchemeHttps)).Count() ?? 0);

    public async Task<List<ApiClient>> GenerateApiClients()
    {
        var apiClients = new List<ApiClient>();
        await RaiseStageChangedCallback("Getting roots...");
        var rootItems = PostmanCollection.GetRootCollections();


        rootItems.ForEach(x => RootItemHelpers.RemoveUnusedParameters(x, Options.ApiClientOptions));
        var allVariableUsages = await DoPreProcessing(rootItems);

        if (Options.ApiClientOptions.RootDefinition == RootDefinition.PerAuthority)
        {
            rootItems = RootItemHelpers.ResortByAuthority(rootItems);
        }

        await RaiseStageChangedCallback("Generating ApiClients...");
        var i = 0;
        foreach (var rootItem in rootItems)
        {
            var variableUsages = allVariableUsages[i++];
            if (!rootItem.HasRequests()) continue;
            if (!rootItem.HasCommonAuthority())
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

        var allCallNames = rootItem.RequestItems()?.Select(x => x.Name).ToList() ?? new ();
        var commonBase = Utils.GetCommonBase(allCallNames);
        commonBase ??= Utils.GetLongestSubstring(allCallNames);
        var normalizedNameSpace = Utils.NormalizeToCsharpPropertyName(commonBase ?? Options.CSharpCodeWriterConfig.Namespace);
        var name = normalizedNameSpace + "ApiClient";
        var leastPossibleUri = rootItem.FindLeastPossibleUri();
        var commonHeaders = rootItem.GetCommonHeaders();
        var uniqueNamesList = new List<string>() { normalizedNameSpace, name };
        var (httpCalls, totalClassesGeneratedFromHttpCalls, duplicateRoots) = await GetHttpCalls(rootItem, commonHeaders, normalizedNameSpace, uniqueNamesList);
        var auth = rootItem.Auth ?? PostmanCollection.Auth;

        var apiClient = new ApiClient(name, rootItem.Description, normalizedNameSpace, leastPossibleUri, httpCalls, commonHeaders, auth, variableUsages,
            Options.ApiClientOptions, totalClassesGeneratedFromHttpCalls + 1, duplicateRoots);
        // This is generated here and not in the constructor because it allows my wasm app to lazy load a couple large dlls
        // that are used in the generation process. GenerateSourceCode was being called when I deserialized api clients from local storage
        apiClient.GenerateSourceCode();
        return apiClient;
    }

    public string GenerateUniqueName(string baseName, List<string> existingNames)
    {
        // If the base name doesn't exist in the list, return it as is
        if (!existingNames.Contains(baseName))
        {
            existingNames.Add(baseName);
            return baseName;
        }

        // If the base name does exist, append numbers starting from 2 until a unique name is found
        int counter = 2;
        while (existingNames.Contains(baseName + counter))
        {
            counter++;
        }
        existingNames.Add(baseName + counter);
        return baseName + counter;
    }

    private async Task<(List<HttpCall> HttpCalls, int TotalClassesGenerated, List<DuplicateRoot> DuplicateRoots)> GetHttpCalls(CollectionItem item, List<Header> commonHeaders, string nameSpace, List<string> uniqueNames)
    {
        List<DuplicateRoot> duplicateRoots = new();
        List<HttpCall> httpCalls = new ();
        var requestItems = item.RequestItems();
        var writeFormDataComments = Options.ApiClientOptions.XmlCommentTypes.Contains(XmlCommentTypes.FormData);
        var totalClassesGenerated = 0;

        if(requestItems == null) return (httpCalls, totalClassesGenerated, duplicateRoots);

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
                uniqueName = GenerateUniqueName(normalizedName, uniqueNames);
            }

            requestItem.Request!.Description = requestItem.Request.Description.HtmlToPlainText();
            string? requestSourceCode = null;
            string? requestClassName = null;
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
                        ProcessItem(jsonClassGenerator, json, uniqueName, Consts.Request, ref requestClassName,
                            ref requestSourceCode, ref requestTypes);
                    }
                    catch (JsonException)
                    {
                        requestClassName = "EmptyRequest";
                        requestSourceCode = null;
                        requestTypes = null;
                    }
                    catch (NoClassesGeneratedException)
                    {
#if DEBUG
                        Console.WriteLine($"Request no classes generated. {requestItem.Name}");
#endif
                        requestClassName = null;
                        requestSourceCode = null;
                        requestTypes = null;
                    }
                    catch (DuplicateRootException ex)
                    {
                        AddDuplicateRootUsage(ex.OriginalRootName, ex.OriginalIsRoot, requestClassName, uniqueName, GeneratedClassType.Request);
                        requestClassName = ex.OriginalRootName;
                        requestSourceCode = null;
                        requestTypes = null;
                    }
                    catch (Exception ex)
                    {
                        requestClassName = null;
                        requestSourceCode = null;
                        requestTypes = null;
#if DEBUG
                        Console.WriteLine(ex);
                        throw;
                        #endif
                    }
                }
            }

            string? formClassName = null;
            string? formClassSourceCode = null;
            if (requestDataType is DataType.ComplexFormData or DataType.SimpleFormData)
            {
                formClassName = uniqueName + (requestDataType is DataType.ComplexFormData ? Consts.Classes.MultipartFormData : Consts.Classes.FormData);
                formClassName = GenerateUniqueName(formClassName, uniqueNames);
                if (requestItem.Request!.Body!.Formdata != null)
                {
                    var iFormData = requestItem.Request!.Body!.Formdata.Cast<IFormData>().ToList();
                    formClassSourceCode = GenerateFormDataClass(formClassName, iFormData, requestDataType, nameSpace, writeFormDataComments);
                    totalClassesGenerated++;
                }
                else if (requestItem.Request!.Body!.Urlencoded != null)
                {
                    var iFormData = requestItem.Request!.Body!.Urlencoded.Cast<IFormData>().ToList();
                    formClassSourceCode = GenerateFormDataClass(formClassName, iFormData, DataType.SimpleFormData, nameSpace, writeFormDataComments);
                    totalClassesGenerated++;
                }
            }

            var allResponses = requestItem.Response?.Where(x => x.Code.HasValue).GroupBy(x => x.Code!.Value).Select(x => x.First()).ToList() ?? new List<Response>();
            var allApiResponse = new List<ApiResponse>();
            foreach (var response in allResponses)
            {
                if (response.Code == null) continue;

                var responseDataType = Utils.GetResponseDataType(response);
                if (responseDataType == DataType.Json)
                {
                    string? responseSourceCode = null;
                    string? responseClassName = null;
                    var json = response.Body;
                    if (string.IsNullOrWhiteSpace(json))
                    {
                        responseClassName = "EmptyResponse";
                        allApiResponse.Add(new ApiResponse(response.Code.Value, responseClassName, sourceCode: null, DataType.Json));
                        continue;
                    }

                    try
                    {
                        List<ClassType>? _ = null;
                        var type = allResponses.Count > 1
                            ? (HttpStatusCode) response.Code + Consts.Response
                            : Consts.Response;
                        ProcessItem(jsonClassGenerator, json, uniqueName, type, ref responseClassName,
                            ref responseSourceCode, ref _);
                    }
                    catch (JsonException)
                    {
                        responseClassName = "EmptyResponse";
                        allApiResponse.Add(new ApiResponse(response.Code.Value, responseClassName, sourceCode: null, DataType.Json));
                        continue;
                    }
                    catch (NoClassesGeneratedException)
                    {
                        responseClassName = "EmptyResponse";
                        allApiResponse.Add(new ApiResponse(response.Code.Value, responseClassName, sourceCode: null, DataType.Json));
                        continue;
#if DEBUG
                        Console.WriteLine($@"Response no classes generated. {requestItem.Name}");
#endif
                    }
                    catch (DuplicateRootException ex)
                    {
                        AddDuplicateRootUsage(ex.OriginalRootName, ex.OriginalIsRoot, responseClassName, uniqueName, GeneratedClassType.Response);

                        responseClassName = ex.OriginalRootName;
                        allApiResponse.Add(new ApiResponse(response.Code.Value, responseClassName, sourceCode: null, DataType.Json));
                        continue;
                    }
                    catch (Exception ex)
                    {
                        responseClassName = "EmptyResponse";
                        allApiResponse.Add(new ApiResponse(response.Code.Value, responseClassName, sourceCode: null, DataType.Json));
                        continue;
#if DEBUG
                        Console.WriteLine(ex);
                        throw;
#endif
                    }
                    allApiResponse.Add(new ApiResponse(response.Code.Value, responseClassName, responseSourceCode, DataType.Json));
                    continue;
                }

                allApiResponse.Add(new ApiResponse(response.Code.Value, null, null, DataType.Binary));
            }
            // If there is no success response, add one
            if (!allApiResponse.Any(x => x.IsSuccessCode))
            {
                allApiResponse.Add(new ApiResponse(200, null, null, DataType.Binary));
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
                    ProcessItem(jsonClassGenerator, queryParametersAsJson, uniqueName, Consts.Parameters, ref queryParametersClassName, ref queryParametersSourceCode, ref queryParameterTypes, descriptionDict);
                }
                catch (NoClassesGeneratedException)
                {
#if DEBUG
                    Console.WriteLine($@"Query parameters no classes generated. {requestItem.Name}");
#endif
                    queryParametersClassName = null;
                    queryParametersSourceCode = null;
                }
                catch (DuplicateRootException ex)
                {
                    AddDuplicateRootUsage(ex.OriginalRootName, ex.OriginalIsRoot, queryParametersClassName, uniqueName, GeneratedClassType.QueryParameters);
                    queryParametersClassName = ex.OriginalRootName;
                    queryParametersSourceCode = null;
                }
                catch (Exception ex)
                {
                    queryParametersClassName = null;
                    queryParametersSourceCode = null;
#if DEBUG
                    Console.WriteLine(ex);
                    throw;
#endif
                }
            }

            var uniqueHeaders = requestItem.Request.Header.Except(commonHeaders).ToList() ?? new ();

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
                AllResponses = allApiResponse,
                QueryParameterClassName = queryParametersClassName,
                QueryParameterSourceCode = queryParametersSourceCode,
                FormDataClassName = formClassName,
                FormDataSourceCode = formClassSourceCode,
                UniqueHeaders = uniqueHeaders,
                RequestTypes = requestTypes,
                QueryParameterTypes = queryParameterTypes,
            });

            _processedRequests += 1;
            if (_processedRequests > TotalRequest) 
                throw new Exception("Something went wrong");
            await RaiseProgressCallback((float) _processedRequests / TotalRequest);
        }
        return (httpCalls, totalClassesGenerated, duplicateRoots);
        
        void ProcessItem(JsonClassGenerator classGenerator, string json, string itemName, string itemType, ref string? className, ref string? sourceCode, 
            ref List<ClassType>? types, Dictionary<string, string?>? descriptionDict = null)
        {
            className = GenerateUniqueName(itemName + itemType, uniqueNames);
            var writeComments = Options.ApiClientOptions.XmlCommentTypes.Contains(XmlCommentTypes.QueryParameters);
            classGenerator.SetRootName(className);
            if (itemType == Consts.Parameters)
            {
                classGenerator.CodeWriter = ParametersConfig(className, nameSpace, writeComments);
                classGenerator.SetCurrentRootIsQueryParameters(true);
            }
            else
            {
                var codeWriterClone = Options.CSharpCodeWriterConfig.Clone();
                codeWriterClone.Namespace = nameSpace;
                codeWriterClone.RootClassName = className;
                classGenerator.CodeWriter = new CSharpCodeWriter(codeWriterClone, writeComments);
                classGenerator.SetCurrentRootIsQueryParameters(false);
            }
            classGenerator.SetDescriptionDict(descriptionDict);
            (sourceCode, var classCount) = GenerateClasses(classGenerator, json, ref types, className);
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

            CSharpCodeWriter ParametersConfig(string name, string nameSpacee, bool writeDescriptions)
            {
                var config = new CSharpCodeWriterConfig(name);
                config.AttributeUsage = JsonPropertyAttributeUsage.Never;
                config.Namespace = nameSpacee;
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

    private static (string SourceCode, int ClassCount) GenerateClasses(JsonClassGenerator jsonClassGenerator, string json, ref List<ClassType>? types, string rootClassName)
    {
        var sb = jsonClassGenerator.GenerateClasses(json, out var errorMessage);
        types = jsonClassGenerator.Types?.Select(x => new ClassType()
        {
            AssignedName = x.NewAssignedName,
            Fields = x.Fields.Select(y => new Field()
            {
                MemberName = y.MemberName,
                JsonMemberName = y.JsonMemberName
            }).ToList()
        }).ToList();
        var consolidated = CodeAnalysisUtils.ConsolidateNamespaces(sb.ToString(), rootClassName);
        var reordered = CodeAnalysisUtils.ReorderClasses(consolidated, rootClassName, out var classCount);
        return (reordered, classCount);
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
        var usings = $@"
using System;
using System.Collections.Generic;
using System.Net.Http;
";

        var namespaceDeclaration = $"namespace {nameSpace}";

        SyntaxTree tree = CSharpSyntaxTree.ParseText(usings + $@"
{namespaceDeclaration}
{{
    public class {formClassName} : {formDataInterface}
    {{
    }}
}}");
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