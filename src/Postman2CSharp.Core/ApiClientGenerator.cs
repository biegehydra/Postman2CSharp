using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis;
using Newtonsoft.Json.Linq;
using Postman2CSharp.Core.Core;
using Postman2CSharp.Core.Models;
using Postman2CSharp.Core.Models.PostmanCollection;
using Postman2CSharp.Core.Models.PostmanCollection.Http;
using Postman2CSharp.Core.Models.PostmanCollection.Http.Request;
using Postman2CSharp.Core.Models.PostmanCollection.Http.Response;
using Xamasoft.JsonClassGenerator;
using Xamasoft.JsonClassGenerator.CodeWriters;
using JsonSerializer = System.Text.Json.JsonSerializer;
using System.Text.RegularExpressions;
using Xamasoft.JsonClassGenerator.CodeWriterConfiguration;
using Xamasoft.JsonClassGenerator.Models;
using Parameter = Postman2CSharp.Core.Models.PostmanCollection.Http.Parameter;

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
            var variableUsages = VariableExtractor.ExtractVariableUsages(rootItem, PostmanCollection.Variable ?? new());
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
        var commonBase = Helpers.GetCommonBase(allCallNames);
        commonBase ??= Helpers.GetLongestSubstring(allCallNames);

        var name = Helpers.NormalizeToCsharpPropertyName(commonBase ?? Options.CSharpCodeWriterConfig.Namespace) + "ApiClient";
        var nameSpace = Helpers.NormalizeToCsharpPropertyName(commonBase ?? Options.CSharpCodeWriterConfig.Namespace);
        var leastPossibleUri = rootItem.FindLeastPossibleUri();
        var commonHeaders = rootItem.GetCommonHeaders();
        var httpCalls = await GetHttpCalls(rootItem, commonHeaders, nameSpace);
        var auth = rootItem.Auth ?? PostmanCollection.Auth;

        var apiClient = new ApiClient(name, rootItem.Description, nameSpace, leastPossibleUri, httpCalls, commonHeaders, auth, variableUsages,
            Options.ApiClientOptions.EnsureResponseIsSuccessStatusCode, Options.ApiClientOptions.XmlCommentTypes, Options.ApiClientOptions.CatchExceptionTypes,
            Options.ApiClientOptions.ErrorHandlingSinks, Options.ApiClientOptions.ErrorHandlingStrategy, Options.ApiClientOptions.LogLevel, Options.CSharpCodeWriterConfig.AttributeLibrary,
            Options.ApiClientOptions.UseCancellationTokens);
        // This is generated here and not in the constructor because it allows my wasm app to lazy load a couple large dlls
        // that are used in the generation process. GenerateSourceCode was being called when I deserialized api clients from local storage
        apiClient.GenerateSourceCode();
        return apiClient;
    }

    private async Task<List<HttpCall>> GetHttpCalls(CollectionItem item, List<Header> commonHeaders, string nameSpace)
    {
        List<HttpCall> httpCalls = new ();
        var requestItems = item.RequestItems();
        var writeFormDataComments = Options.ApiClientOptions.XmlCommentTypes.Contains(XmlCommentTypes.FormData);

        if(requestItems == null) return httpCalls;

        var jsonClassGenerator = ClassGenerator();
        foreach (var requestItem in requestItems)
        {
            var requestDataType = GetRequestDataType(requestItem.Request!);
            var normalizedName = Helpers.NormalizeToCsharpPropertyName(requestItem.Name);

            string? requestSourceCode = null;
            string? requestClassName = null;
            List<ClassType>? requestTypes = null;
            if (requestDataType is DataType.Json)
            {
                var json = requestItem.Request!.Body!.Raw ?? "";
                if (string.IsNullOrWhiteSpace(json))
                {
                    requestClassName = "EmptyRequest";
                }
                else
                {
                    try
                    {
                        ProcessItem(jsonClassGenerator, json, normalizedName, Consts.Request, ref requestClassName,
                            ref requestSourceCode, ref requestTypes);
                    }
                    catch (JsonException)
                    {
                        requestClassName = "EmptyRequest";
                        requestSourceCode = null;
                        requestTypes = null;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                    }
                }
            }

            string? formClassName = null;
            string? formClassSourceCode = null;
            if (requestDataType is DataType.ComplexFormData or DataType.SimpleFormData)
            {
                formClassName = normalizedName + (requestDataType is DataType.ComplexFormData ? Consts.Classes.MultipartFormData : Consts.Classes.FormData);
                Dictionary<string, string?> descriptionDict;
                if (requestItem.Request!.Body!.Formdata != null)
                {
                    descriptionDict = requestItem.Request!.Body!.Formdata!
                        .GroupBy(x => x.Key)
                        .ToDictionary(
                            group => group.Key,
                            group => group.First().Description?.HtmlToPlainText()
                        );
                    formClassSourceCode = GenerateFormDataClass(formClassName, requestItem.Request!.Body!.Formdata, requestDataType, nameSpace, descriptionDict, writeFormDataComments);
                }
                else if (requestItem.Request!.Body!.Urlencoded != null)
                {
                    descriptionDict = requestItem.Request!.Body!.Urlencoded!
                        .GroupBy(x => x.Key)
                        .ToDictionary(
                            group => group.Key,
                            group => group.First().Description?.HtmlToPlainText()
                        );
                    formClassSourceCode = GenerateFormDataClass(formClassName, requestItem.Request!.Body!.Urlencoded, nameSpace, descriptionDict, writeFormDataComments);
                }
            }

            var successResponse = requestItem.Response?.FirstOrDefault(x => x.Code == 200);
            var responseDataType = GetResponseDataType(successResponse);
            string? responseSourceCode = null;
            string? responseClassName = null;
            List<ClassType>? responseTypes = null;
            if (responseDataType == DataType.Json)
            {   
                var json = successResponse!.Body ?? string.Empty;
                if (string.IsNullOrWhiteSpace(json))
                {
                    responseClassName = "EmptyResponse";
                }
                else
                {
                    try
                    {
                        ProcessItem(jsonClassGenerator, json, normalizedName, Consts.Response, ref responseClassName, ref responseSourceCode, ref responseTypes);
                    }
                    catch (JsonException)
                    {
                        responseClassName = "EmptyResponse";
                        responseSourceCode = null;
                        responseTypes = null;
                    }
                }
            }

            string? queryParametersSourceCode = null;
            string? queryParametersClassName = null;
            List<ClassType>? queryParameterTypes = null;
            if (requestItem.Request!.Url.Query is {Count: > 0})
            {
                var jObject = new JObject();

                foreach (var queryParameter in requestItem.Request.Url.Query)
                {
                    //if (queryParameter.Disabled.HasValue && queryParameter.Disabled.Value)
                    //{
                    //    continue; // TODO Option to skip disabled parameters
                    //}

                    jObject[queryParameter.Key] = "test";
                }

                var descriptionDict = requestItem.Request.Url.Query
                    .GroupBy(x => x.Key)
                    .ToDictionary(
                        group => group.Key,
                        group => group.First().Description?.HtmlToPlainText()
                    );
                var queryParametersAsJson = jObject.ToString();
                ProcessItem(jsonClassGenerator ,queryParametersAsJson, normalizedName, Consts.Parameters, ref queryParametersClassName, ref queryParametersSourceCode, ref queryParameterTypes, descriptionDict);
            }

            var uniqueHeaders = requestItem.Request.Header.Except(commonHeaders).ToList() ?? new ();

            var httpClientFunction = Helpers.HttpClientCall(requestItem.Request.Method, requestDataType, responseDataType, Options.CSharpCodeWriterConfig.AttributeLibrary);


            httpCalls.Add(new ()
            {
                Name = normalizedName,
                HttpClientFunction = httpClientFunction,
                Request = requestItem.Request!,
                RequestDataType = requestDataType,
                RequestClassName = requestClassName,
                RequestSourceCode = requestSourceCode,
                SuccessResponse = successResponse,
                ResponseDataType = responseDataType,
                ResponseClassName = responseClassName,
                ResponseSourceCode = responseSourceCode,
                QueryParameterClassName = queryParametersClassName,
                QueryParameterSourceCode = queryParametersSourceCode,
                FormDataClassName = formClassName,
                FormDataSourceCode = formClassSourceCode,
                UniqueHeaders = uniqueHeaders,
                RequestTypes = requestTypes,
                ResponseTypes = responseTypes,
                QueryParameterTypes = queryParameterTypes,
            });

            _processedRequests += 1;
            if (_processedRequests > TotalRequest) 
                throw new Exception("Something went wrong");
            await RaiseProgressCallback((float) _processedRequests / TotalRequest);
        }
        return httpCalls;

        void ProcessItem(JsonClassGenerator jsonClassGenerator, string json, string itemName, string itemType, ref string? className, ref string? sourceCode, ref List<ClassType>? types, Dictionary<string, string?>? descriptionDict = null)
        {
            className = itemName + itemType;
            var writeComments = Options.ApiClientOptions.XmlCommentTypes.Contains(XmlCommentTypes.QueryParameters);
            jsonClassGenerator.SetRootName(className);
            if (itemType == Consts.Parameters)
            {
                jsonClassGenerator.CodeWriter = ParametersConfig(className, nameSpace, writeComments);
            }
            else
            {
                var codeWriterClone = Options.CSharpCodeWriterConfig.Clone();
                codeWriterClone.Namespace = nameSpace;
                codeWriterClone.RootClassName = className;
                jsonClassGenerator.CodeWriter = new CSharpCodeWriter(codeWriterClone, writeComments);
            }
            jsonClassGenerator.SetDescriptionDict(descriptionDict);
            sourceCode = GenerateClasses(jsonClassGenerator, json, ref types, className);
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

        static DataType GetRequestDataType(Request request)
        {
            if (request.Body is {Mode: "raw", Options.Raw.Language: "json"})
            {
                return DataType.Json;
            }
            else if (request.Body is { Mode: "raw", Options: null })
            {
                return DataType.Json;
            }
            if (request.Body is { Mode: "raw", Options.Raw.Language: "xml" })
            {
                return DataType.Xml;
            }
            if (request.Body is { Mode: "raw", Options.Raw.Language: "html" })
            {
                return DataType.Html;
            }
            if (request.Body is { Mode: "raw", Options.Raw.Language: "text" })
            {
                return DataType.Text;
            }
            if (request.Body is { Mode: "urlencoded", Urlencoded.Count: > 0 })
            {
                return DataType.SimpleFormData;
            }
            if (request.Body is {Mode: "formdata", Formdata.Count: > 0})
            {
                return request.Body.Formdata.Any(x => x.FormDataType == FormDataType.File || x.Src != null) ? DataType.ComplexFormData : DataType.SimpleFormData;
            }
            else if (request.Body is { Mode: "formdata", Formdata.Count: 0 })
            {
                return DataType.QueryOnly;
            }
            if (request.Body == null)
            {
                return DataType.QueryOnly;
            }
            if (request.Body.Mode == "file")
            {
                return DataType.Binary;
            }

            if (request.Body.Mode == "graphql")
            {
                return DataType.GraphQl;
            }
            else
            {
                if (request.Body.Mode == null)
                {
                    throw new ArgumentException(nameof(request.Body.Mode), $"Request mode not supported for request {request.Url.Raw}: null");
                }
                else
                {
                    throw new ArgumentException(nameof(request.Body.Mode), $"Request mode not supported for request {request.Url.Raw}: {request.Body.Mode}");
                }
            }
        }

        static DataType GetResponseDataType(Response? response)
        {
            if (response?.Header?.Any(x => x.Key.ToLower() == "content-type" && PossibleJsonContentTypes.Any(type => x.Value.ToLower().Contains(type))) ?? false)
            {
                return DataType.Json;
            }

            return DataType.Null;
        }
    }

    private static readonly List<string> PossibleJsonContentTypes = new () { "application/json", "text" };

    private JsonClassGenerator ClassGenerator()
    {
        return new JsonClassGenerator(CSharpCodeWriter(), Options.ApiClientOptions.RemoveDuplicateClasses);
    }

    private CSharpCodeWriter CSharpCodeWriter()
    {
        var writeComments = Options.ApiClientOptions.XmlCommentTypes.Contains(XmlCommentTypes.QueryParameters);
        return new CSharpCodeWriter(Options.CSharpCodeWriterConfig, writeComments);
    }

    private static string GenerateClasses(JsonClassGenerator jsonClassGenerator, string json, ref List<ClassType>? types, string rootClassName)
    {
        var sb = jsonClassGenerator.GenerateClasses(json, out var errorMessage);
        types = jsonClassGenerator.Types?.Select(x => new ClassType()
        {
            AssignedName = x.AssignedName,
            Fields = x.Fields.Select(y => new Field()
            {
                MemberName = y.MemberName,
                JsonMemberName = y.JsonMemberName
            }).ToList()
        }).ToList();
        var consolidated = Helpers.ConsolidateNamespaces(sb.ToString());
        return Helpers.ReorderClasses(consolidated, rootClassName);
    }

    private static string? GenerateFormDataClass(string? formClassName, List<Parameter>? parameters, string nameSpace, Dictionary<string, string?> descriptionDict, bool writeFormDataComments)
    {
        if (formClassName == null || parameters == null) return null;

        var usings = @$"
using System;
using System.Collections.Generic;
using System.Net.Http;
";

        var namespaceDeclaration = $"namespace {nameSpace}";

        SyntaxTree tree = CSharpSyntaxTree.ParseText(usings + $@"
{namespaceDeclaration}
{{
    public class {formClassName} : IFormData
    {{
    }}
}}");

        var root = (CompilationUnitSyntax)tree.GetRoot();

        var newRoot = root.ReplaceNodes(root.DescendantNodes().OfType<ClassDeclarationSyntax>(),
            (node, _) =>
            {
                if (node.Identifier.Text == formClassName)
                {
                    // Adding properties
                    foreach (var parameter in parameters)
                    {
                        string comment = "";

                        if (writeFormDataComments && descriptionDict.TryGetValue(parameter.Key, out var value))
                        {
                            if (!string.IsNullOrWhiteSpace(value))
                            {
                                comment = XmlCommentHelpers.ToXmlComment(value, Consts.Indent(3));
                            }
                        }

                        var prop = SyntaxFactory.PropertyDeclaration(SyntaxFactory.ParseTypeName("string"), parameter.CsPropertyName)
                            .AddModifiers(SyntaxFactory.Token(SyntaxKind.PublicKeyword))
                            .AddAccessorListAccessors(
                                SyntaxFactory.AccessorDeclaration(SyntaxKind.GetAccessorDeclaration).WithSemicolonToken(SyntaxFactory.Token(SyntaxKind.SemicolonToken)),
                                SyntaxFactory.AccessorDeclaration(SyntaxKind.SetAccessorDeclaration).WithSemicolonToken(SyntaxFactory.Token(SyntaxKind.SemicolonToken)))
                            .WithLeadingTrivia(SyntaxFactory.ParseLeadingTrivia(comment));

                        node = node.AddMembers(prop);
                    }

                    // Create method
                    var toFormDataMethod = SyntaxFactory.MethodDeclaration(SyntaxFactory.ParseTypeName("FormUrlEncodedContent"), "ToFormData")
                        .AddModifiers(SyntaxFactory.Token(SyntaxKind.PublicKeyword))
                        .WithBody(SyntaxFactory.Block(SyntaxFactory.ParseStatement(
                            $@"return new FormUrlEncodedContent(new KeyValuePair<string, string>[]
                        {{
                            {string.Join(",\n", parameters.Select(x => $"new (\"{x.Key}\", {x.CsPropertyName})"))}
                        }});"
                        )));

                    // Add the form data method
                    node = node.AddMembers(toFormDataMethod);
                }

                return node;
            });
        var indent = Consts.Indent(2);
        return newRoot.NormalizeWhitespace().ToFullString().Replace(indent + "/// <summary>", Environment.NewLine + indent + "/// <summary>"); ;
    }

    private static string? GenerateFormDataClass (string? formClassName, List<Formdata>? formdatas, DataType? dataType, string nameSpace, Dictionary<string, string?> descriptionDict, bool writeFormDataComments)
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

        var root = (CompilationUnitSyntax)tree.GetRoot();

        var newRoot = root.ReplaceNodes(root.DescendantNodes().OfType<ClassDeclarationSyntax>(),
            (node, _) =>
            {
                if (node.Identifier.Text == formClassName)
                {
                    int files = 1;
                    // Adding properties
                    foreach (var formdata in formdatas)
                    {
                        string comment = "";

                        if (writeFormDataComments && descriptionDict.TryGetValue(formdata.Key, out var value))
                        {
                            if (!string.IsNullOrWhiteSpace(value))
                            {
                                comment = XmlCommentHelpers.ToXmlComment(value, Consts.Indent(3));
                            }
                        }
                        if (formdata.FormDataType == FormDataType.Text)
                        {
                            var prop = SyntaxFactory.PropertyDeclaration(SyntaxFactory.ParseTypeName("string"), formdata.CsPropertyName)
                                .AddModifiers(SyntaxFactory.Token(SyntaxKind.PublicKeyword))
                                .AddAccessorListAccessors(
                                    SyntaxFactory.AccessorDeclaration(SyntaxKind.GetAccessorDeclaration).WithSemicolonToken(SyntaxFactory.Token(SyntaxKind.SemicolonToken)),
                                    SyntaxFactory.AccessorDeclaration(SyntaxKind.SetAccessorDeclaration).WithSemicolonToken(SyntaxFactory.Token(SyntaxKind.SemicolonToken)))
                                .WithLeadingTrivia(SyntaxFactory.ParseLeadingTrivia(comment));

                            node = node.AddMembers(prop);
                        }

                        if (formdata.FormDataType == FormDataType.File)
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
                    if (formDataInterface == "IMultipartFormData")
                    {
                        files = 0;
                        string formDataContent = string.Join(",\n",
                            formdatas.Select((x, i) =>
                            {
                                if (x.FormDataType == FormDataType.File) files++;
                                var fileNumberText = files > 1 ? files.ToString() : null;
                                return x.FormDataType == FormDataType.Text
                                    ? $"{{ new StringContent({x.CsPropertyName}), \"{x.Key}\" }}"
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
                            formdatas.Select(x => $"new (\"{x.Key}\", {x.CsPropertyName} )"));

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
    }
}