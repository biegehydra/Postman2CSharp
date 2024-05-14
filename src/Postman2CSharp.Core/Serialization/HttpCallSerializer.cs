using Postman2CSharp.Core.Infrastructure;
using Postman2CSharp.Core.Models;
using Postman2CSharp.Core.Models.PostmanCollection.Authorization;
using Postman2CSharp.Core.Models.PostmanCollection.Http;
using Postman2CSharp.Core.Utilities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using Xamasoft.JsonClassGenerator.Models;

namespace Postman2CSharp.Core.Serialization;

public static class HttpCallSerializer
{
    public static void SerializeHttpCall(StringBuilder sb, string? baseUrl, HttpCall call, bool constructorHasAuthHeader,
        ApiClientOptions options, string graphQlQueriesClassName)
    {
        string relativePath;
        try
        {
            relativePath = Utils.ExtractRelativePath(baseUrl, call.Request.Url.Raw);
        }
        catch (ArgumentException)
        {
            throw new ArgumentException(nameof(call.Request.Url.Raw), $"Invalid URL for {call.Name}: {call.Request.Url.Raw}");
        }
        var methodParameters = call.MethodParameters(options.OutputCollectionType);
        if (options.UseCancellationTokens)
        {
            methodParameters.Add(HttpCallMethodParameter.CancellationToken);
        }

        var indent = Consts.Indent(1);
        XmlComment(sb, options.XmlCommentTypes, call.Request.Description, call.Request.Url.Path, call.Request.Url.Variable, indent);
        sb.FunctionSignature(call, indent, methodParameters, options);
        sb.AppendLineIndented(indent, "{");

        indent = Consts.Indent(2);

        if (options.ErrorHandlingStrategy == ErrorHandlingStrategy.None)
        {
            HttpCallBody(sb, call, constructorHasAuthHeader, 2, relativePath, options, graphQlQueriesClassName);
        }
        else
        {
            if (options.CatchExceptionTypes.Count == 0)
            {
                options.CatchExceptionTypes = new List<CatchExceptionTypes> { CatchExceptionTypes.HttpRequestException };
            }
            sb.AppendLineIndented(indent, "try");
            sb.AppendLineIndented(indent, "{");

            HttpCallBody(sb, call, constructorHasAuthHeader, 3, relativePath, options, graphQlQueriesClassName);

            indent = Consts.Indent(2);
            sb.AppendLineIndented(indent, "}");
            foreach (var catchExceptionType in options.CatchExceptionTypes)
            {
                Catch(sb, catchExceptionType, options.ErrorHandlingSinks, options.ErrorHandlingStrategy, options.LogLevel, indent);
            }
        }

        indent = Consts.Indent(1);
        sb.AppendIndented(indent, "}");
    }


    private static void XmlComment(StringBuilder sb, List<XmlCommentTypes> commentTypes, string? description, List<Path>? paths, List<KeyValueTypeDescription>? variables, string indent)
    {
        if (commentTypes.Contains(XmlCommentTypes.Request) && !string.IsNullOrWhiteSpace(description))
        {
            var xmlComment = XmlCommentHelpers.ToXmlSummary(description, indent);
            sb.Append(xmlComment);
        }

        if (variables == null || paths == null || !commentTypes.Contains(XmlCommentTypes.PathVariables)) return;
        foreach (var path in paths)
        {
            if (variables.FirstOrDefault(x => !string.IsNullOrWhiteSpace(x.Key) && x.Key == path.Value?.Replace(":", string.Empty)) is
                { } keyValueTypeDescription)
            {
                if (!string.IsNullOrWhiteSpace(keyValueTypeDescription.Description))
                {
                    var xmlParam = XmlCommentHelpers.ToXmlParam(keyValueTypeDescription.Description, path.CsPropertyName(CsharpPropertyType.Local), indent);
                    sb.Append(xmlParam);
                }
            }
        }
    }


    private static void GraphQlString(StringBuilder sb, GraphQl? graphQl, bool queriesInSeparateFile, string? graphQlQueriesClassName, string requestName, string? graphQlVariablesClassName, int intIndent)
    {
        if (graphQl == null)
        {
            return;
        }

        string indent = Consts.Indent(intIndent);

        if (queriesInSeparateFile)
        {
            sb.AppendLineIndented(indent, $"string query = {graphQlQueriesClassName}.{requestName};");
        }
        else
        {
            sb.AppendLineIndented(indent, $"var query = @\"\n{HttpUtility.JavaScriptStringEncode(graphQl.Query).Replace(@"\r\n", "\n").Replace(@"\n", "\n").Replace("\\\"", "\"\"")}\";");
        }

        List<string> parameters = ["query"];
        if (!string.IsNullOrWhiteSpace(graphQlVariablesClassName))
        {
            parameters.Add("graphQlVariables");
        }
        else if (!string.IsNullOrWhiteSpace(graphQl.Variables))
        {
            sb.AppendLineIndented(indent, $"var graphQlVariables = @\"\n{HttpUtility.JavaScriptStringEncode(graphQl.Variables).Replace(@"\r\n", "\n").Replace(@"\n", "\n").Replace("\\\"", "\"\"")}\";");
            parameters.Add("graphQlVariables");
        }
        sb.AppendLineIndented(indent, $"var graphQlRequest = new GraphQLRequest({string.Join(", ", parameters)});");
    }

    private static void Catch(StringBuilder sb, CatchExceptionTypes catchExceptionType, List<ErrorHandlingSinks> errorHandlingSinks, ErrorHandlingStrategy errorHandlingStrategy,
        LogLevel logLevel, string indent)
    {
        sb.AppendLineIndented(indent, $"catch ({catchExceptionType} ex)");
        sb.AppendLineIndented(indent, "{");
        indent = Consts.Indent(3);

        sb.ErrorHandlingSinks(errorHandlingSinks, logLevel, indent);

        sb.ErrorHandlingStrategy("throw;", errorHandlingStrategy, indent);

        indent = Consts.Indent(2);
        sb.AppendLineIndented(indent, "}");
    }

    private static void HttpCallBody(StringBuilder sb, HttpCall call, bool constructorHasHeader,
        int intIndent, string relativePath, ApiClientOptions options, string graphQlQueriesClassName)
    {
        if (call.AllResponses.Count == 0) throw new UnreachableException("No success responses found. Should never happen.");
        if (call.AllResponses.Count > 1 && options.HandleMultipleResponses)
        {
            HttpCallMultipleResponseTypesBody(sb, call, constructorHasHeader, intIndent, relativePath, options, graphQlQueriesClassName);
        }
        else
        {
            HttpCallSingleResponseTypeBody(sb, call, constructorHasHeader, intIndent, relativePath, options, graphQlQueriesClassName);
        }
    }

    private static void HttpCallSingleResponseTypeBody(StringBuilder sb, HttpCall call,
        bool constructorHasHeader, int intIndent, string relativePath, ApiClientOptions options, string graphQlQueriesClassName)
    {
        var indent = Consts.Indent(intIndent);
        if (!constructorHasHeader)
        {
            sb.SetAuthenticationHeader(call.Request.Auth, indent);
        }
        UniqueHeaders(sb, call, intIndent, out var hasUniqueHeaders);

        var requestHasQueryString = QueryParameters(sb, call.Request.Auth, call, indent, relativePath);

        if (call.RequestDataType is DataType.Html or DataType.Text or DataType.Xml)
        {
            var parameterName = call.RequestDataType switch
            {
                DataType.Html => "html",
                DataType.Xml => "xml",
                DataType.Text => "text",
                _ => throw new UnreachableException()
            };
            var contentType = call.RequestDataType switch
            {
                DataType.Html => "text/html",
                DataType.Xml => "text/xml",
                DataType.Text => "text/plan",
                _ => throw new UnreachableException()
            };
            sb.AppendLineIndented(indent, $"var httpContent = new StringContent({parameterName}, Encoding.UTF8, \"{contentType}\")");
        }
        else if (call.RequestDataType is DataType.Binary)
        {
            sb.AppendLineIndented(indent, $"var httpContent = new StreamContent(stream);");
        }
        else if (call.RequestDataType is DataType.GraphQl)
        {
            GraphQlString(sb, call.Request.Body!.Graphql, options.GraphQLQueriesInSeperateFile, graphQlQueriesClassName, call.Name, call.GraphQlVariablesClassName, intIndent);
        }

        // We need to use the HttpRequestMessage

        var httpClientCallReturnsResponse = call.SuccessResponse?.ClassName != null &&
                                            call.HttpClientFunction.Contains("Json") &&
                                            !(call.HttpClientFunction.Contains("AsJson") ||
                                              call.HttpClientFunction.Contains("AsNewtonsoftJson"));

        if (call.Request.Auth?.EnumType() == PostmanAuthType.oauth2 && call.Request.Auth.TryGetAuth2Config(OAuth2Config.AddTokenTo, out var addTokenToValue))
        {
            if (Enum.TryParse<AddTokenTo>(addTokenToValue, true, out var addTokenTo))
            {
                if (addTokenTo == AddTokenTo.Header)
                {
                    sb.AppendLineIndented(indent, $"await {OAuth2Functions.AddAccessTokenToRequest}();");
                }
            }
        }

        ReturnOrVarResponse(sb, httpClientCallReturnsResponse, indent);

        HttpClientRequest(sb, call, httpClientCallReturnsResponse, requestHasQueryString, relativePath, hasUniqueHeaders, options);

        if (!httpClientCallReturnsResponse)
        {
            ReturnIfRequestDidNotReturnEarlier(sb, call.SuccessResponse, indent, options);
        }
    }

    private static void HttpCallMultipleResponseTypesBody(StringBuilder sb, HttpCall call,
        bool constructorHasHeader, int intIndent, string relativePath, ApiClientOptions options, string graphQlQueriesClassName)
    {
        var indent = Consts.Indent(intIndent);
        if (!constructorHasHeader)
        {
            sb.SetAuthenticationHeader(call.Request.Auth, indent);
        }
        UniqueHeaders(sb, call, intIndent, out var hasUniqueHeaders);

        var requestHasQueryString = QueryParameters(sb, call.Request.Auth, call, indent, relativePath);

        if (call.RequestDataType is DataType.Html or DataType.Text or DataType.Xml)
        {
            var parameterName = call.RequestDataType switch
            {
                DataType.Html => "html",
                DataType.Xml => "xml",
                DataType.Text => "text",
                _ => throw new UnreachableException()
            };
            var contentType = call.RequestDataType switch
            {
                DataType.Html => "text/html",
                DataType.Xml => "text/xml",
                DataType.Text => "text/plan",
                _ => throw new UnreachableException()
            };
            sb.AppendLine(
                indent + $"var httpContent = new StringContent({parameterName}, Encoding.UTF8, \"{contentType}\")");
        }
        else if (call.RequestDataType is DataType.Binary)
        {
            sb.AppendLineIndented(indent, $"var httpContent = new StreamContent(stream);");
        }
        else if (call.RequestDataType is DataType.GraphQl)
        {
            GraphQlString(sb, call.Request.Body!.Graphql, options.GraphQLQueriesInSeperateFile, graphQlQueriesClassName, call.Name, call.GraphQlVariablesClassName, intIndent);
        }

        if (call.Request.Auth?.EnumType() == PostmanAuthType.oauth2 && call.Request.Auth.TryGetAuth2Config(OAuth2Config.AddTokenTo, out var addTokenToValue))
        {
            if (Enum.TryParse<AddTokenTo>(addTokenToValue, true, out var addTokenTo))
            {
                if (addTokenTo == AddTokenTo.Header)
                {
                    sb.AppendLineIndented(indent, $"await {OAuth2Functions.AddAccessTokenToRequest}();");
                }
            }
        }

        ReturnOrVarResponse(sb, false, indent);

        HttpClientRequest(sb, call, false, requestHasQueryString, relativePath, hasUniqueHeaders, options);

        int i = 0;
        foreach (var group in call.AllResponses.GroupBy(x => x.ClassName))
        {
            if (group.Count() == 1)
            {
                sb.AppendLineIndented(indent, $"if (response.StatusCode == HttpStatusCode.{(HttpStatusCode)group.First().Code})");
            }
            else
            {
                var statusCodes = string.Join(" or ", group.Select(x => $"HttpStatusCode.{(HttpStatusCode)x.Code}"));
                sb.AppendLineIndented(indent, $"if (response.StatusCode is {statusCodes})");
            }
            sb.AppendLineIndented(indent, "{");
            indent = Consts.Indent(intIndent + 1);

            ReturnIfRequestDidNotReturnEarlier(sb, group.First(), indent, options);

            indent = Consts.Indent(intIndent);
            sb.AppendLineIndented(indent, "}");
            i++;
        }

        if (options.UnexpectedStatusCodeHandling == UnexpectedStatusCodeHandling.ThrowException)
        {
            var errorMessage = $"throw new Exception($\"{call.Name}: Unexpected response. Status Code: {{response.StatusCode}}. Content: {{await response.Content.ReadAsStringAsync()}}\");";
            sb.ErrorHandlingStrategy(errorMessage, options.ErrorHandlingStrategy, indent);
        }
        else
        {
            sb.AppendLineIndented(indent, "return new UnexpectedStatusCodeResponse(response);");
        }
    }

    private static void ReturnIfRequestDidNotReturnEarlier(StringBuilder sb, ApiResponse? apiResponse, string indent, ApiClientOptions options)
    {
        sb.AppendIndented(indent, "return ");
        if (apiResponse?.ClassName != null)
        {
            if (options.JsonLibrary == JsonLibrary.SystemTextJson)
            {
                sb.AppendLine($"await response.ReadJsonAsync<{apiResponse.ClassName}>();");
            }
            else
            {
                sb.AppendLine($"await response.ReadNewtonsoftJsonAsync<{apiResponse.ClassName}>();");
            }
        }
        else
        {
            if (options.UseCancellationTokens)
            {
                sb.AppendLine("await response.Content.ReadAsStreamAsync(cancellationToken);");
            }
            else
            {
                sb.AppendLine("await response.Content.ReadAsStreamAsync();");
            }
        }
    }

    private static void HttpClientRequest(StringBuilder sb, HttpCall call, bool httpClientCallReturnsResponse,
        bool requestHasQueryString, string relativePath, bool hasHeaders, ApiClientOptions options)
    {
        if (options.ExecuteWithRetry)
        {
            sb.Append($"await ExecuteWithRetry(() => _httpClient.{call.HttpClientFunction}");
        }
        else
        {
            sb.Append($"await _httpClient.{call.HttpClientFunction}");
        }
        if (httpClientCallReturnsResponse)
        {
            sb.Append($"<{Common.SignatureClassName(call.SuccessResponse!.ClassName, call.SuccessResponse.RootWasArray, options.OutputCollectionType)}>");
        }

        sb.Append('(');

        var relativePathParameter = requestHasQueryString ? "queryString" : $@"$""{relativePath}""";
        var httpClientParameters = HttpClientParameters();
        sb.Append(string.Join(", ", httpClientParameters));
        if (options.ExecuteWithRetry)
        {
            sb.AppendLine("));");
        }
        else
        {
            sb.AppendLine(");");
        }

        List<string> HttpClientParameters()
        {
            var list = new List<string>() { relativePathParameter };
            if (call.RequestClassName != null)
            {
                list.Add("request");
            }
            else if (call.FormDataClassName != null)
            {
                var func = "To" + (call.FormDataClassName.Contains(Consts.Classes.MultipartFormData) ? Consts.Classes.MultipartFormData : Consts.Classes.FormData);
                list.Add($"formData.{func}()");
            }
            else if (call.RequestDataType is DataType.Html or DataType.Text or DataType.Xml or DataType.Binary)
            {
                list.Add("httpContent");
            }
            else if (call.RequestDataType is DataType.GraphQl)
            {
                list.Add("graphQlRequest");
            }

            if (hasHeaders)
            {
                list.Add("headers");
            }
            if (options.UseCancellationTokens)
            {
                if (hasHeaders)
                {
                    list.Add("cancellationToken");
                }
                else
                {
                    list.Add("cancellationToken: cancellationToken");
                }
            }
            return list;
        }
    }

    private static void ReturnOrVarResponse(StringBuilder sb, bool httpClientCallReturnsResponse, string indent)
    {
        if (httpClientCallReturnsResponse)
        {
            sb.AppendIndented(indent, "return ");
        }
        else
        {
            sb.AppendIndented(indent, "var response = ");
        }
    }

    private static bool QueryParameters(StringBuilder sb, AuthSettings? auth, HttpCall call, string indent, string relativePath)
    {
        var requestHasOAuthQueryParameters = false;
        if (auth?.TryGetAuth2Config(OAuth2Config.AddTokenTo, out var addTokenToValue) ?? false)
        {
            if (Enum.TryParse<AddTokenTo>(addTokenToValue, true, out var addTokenTo))
            {
                if (addTokenTo == AddTokenTo.QueryParams)
                {
                    sb.AppendLineIndented(indent, "var oauthQueryParameters = new OAuth2QueryParameters();");
                    sb.AppendLineIndented(indent, "oauthQueryParameters.AccessToken = await GetAccessToken();");
                    requestHasOAuthQueryParameters = true;
                }
            }
        }

        var requestHasQueryString = false;
        if (call.QueryParameterClassName != null && requestHasOAuthQueryParameters)
        {
            sb.AppendLineIndented(indent, "var parametersDict = queryParameters.ToDictionary().Unique(oauthQueryParameters.ToDictionary());");
            requestHasQueryString = true;
        }
        else if (requestHasOAuthQueryParameters)
        {
            sb.AppendLineIndented(indent, $"var parametersDict = oauthQueryParameters.ToDictionary();");
            requestHasQueryString = true;
        }
        else if (call.QueryParameterClassName != null)
        {
            sb.AppendLineIndented(indent, $"var parametersDict = queryParameters.ToDictionary();");
            requestHasQueryString = true;
        }

        if (auth?.EnumType() == PostmanAuthType.apikey)
        {
            if (auth.TryGetApiKeyConfig(ApiKeyConfig.In, out var value))
            {
                if (Enum.TryParse<In>(value, true, out var enumValue) && enumValue == In.Query)
                {
                    var keyValue = auth.TryGetApiKeyConfig(ApiKeyConfig.Key, out var key) ? key : "api_key";
                    if (requestHasQueryString)
                    {
                        sb.AppendLineIndented(indent, $@"parametersDict.Add($""{keyValue}"", {Consts._apiKey});");
                    }
                    else
                    {
                        sb.AppendLineIndented(indent, $@"var parametersDict = new Dictionary<string, string>() {{ {{ ""{keyValue}"", {Consts._apiKey} }} }};");
                        requestHasQueryString = true;
                    }
                }
            }
        }

        if (requestHasQueryString)
        {
            sb.AppendLineIndented(indent, $"var queryString = QueryHelpers.AddQueryString($\"{relativePath}\", parametersDict);");
        }

        return requestHasQueryString;
    }

    private static void UniqueHeaders(StringBuilder sb, HttpCall call, int intIndent, out bool hasUniqueHeaders)
    {
        if (!call.UniqueHeaders.Any(Header.IsImportant))
        {
            hasUniqueHeaders = false;
            return;
        }
        hasUniqueHeaders = true;
        var indent = Consts.Indent(intIndent);
        sb.AppendLineIndented(indent, "var headers = new Dictionary<string, string>()");
        sb.AppendLineIndented(indent, "{");
        indent = Consts.Indent(intIndent + 1);
        var uniqueHeaders = call.UniqueHeaders.Where(Header.IsImportant).ToList();
        var last = uniqueHeaders.Last();
        foreach (var uniqueHeader in call.UniqueHeaders.Where(Header.IsImportant))
        {
            hasUniqueHeaders = true;
            sb.AppendIndented(indent, $"{{ $\"{uniqueHeader.Key}\", $\"{uniqueHeader.Value}\" }}");
            if (uniqueHeader != last)
            {
                sb.Append(',');
            }
            sb.AppendLine();
        }
        indent = Consts.Indent(intIndent);
        sb.AppendLineIndented(indent, "};");
        sb.AppendLine();
    }
}