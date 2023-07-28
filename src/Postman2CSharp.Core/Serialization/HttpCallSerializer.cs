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
using Xamasoft.JsonClassGenerator.Models;

namespace Postman2CSharp.Core.Serialization;

public static class HttpCallSerializer
{
    public static void SerializeHttpCall(StringBuilder sb, AuthSettings? auth, string? baseUrl, HttpCall call, bool constructorHasAuthHeader,
        ApiClientOptions options, OutputCollectionType outputCollectionType)
    {
        string relativePath;
        try
        {
            relativePath = Utils.ExtractRelativePath(baseUrl ?? "", call.Request.Url.Raw);
        }
        catch (ArgumentException)
        {
            throw new ArgumentException(nameof(call.Request.Url.Raw), $"Invalid URL: {call.Request.Url.Raw}");
        }
        var methodParameters = call.MethodParameters(outputCollectionType);
        if (options.UseCancellationTokens)
        {
            methodParameters.Add(HttpCallMethodParameter.CancellationToken);
        }

        var indent = Consts.Indent(1);
        XmlComment(sb, options.XmlCommentTypes, call.Request.Description, call.Request.Url.Path, call.Request.Url.Variable, indent);
        sb.FunctionSignature(call, indent, outputCollectionType, methodParameters, options.HandleMultipleResponses, options.MultipleResponseHandling);
        sb.AppendLine(indent + "{");

        indent = Consts.Indent(2);

        if (options.ErrorHandlingStrategy == ErrorHandlingStrategy.None)
        {
            HttpCallBody(sb, auth, call, constructorHasAuthHeader, 2, relativePath, options.EnsureResponseIsSuccessStatusCode, options.JsonLibrary,
                options.UseCancellationTokens, options.ErrorHandlingStrategy, outputCollectionType, options.HandleMultipleResponses, options.ExecuteWithRetry);
        }
        else
        {
            if (options.CatchExceptionTypes.Count == 0)
            {
                options.CatchExceptionTypes = new List<CatchExceptionTypes> { CatchExceptionTypes.HttpRequestException };
            }
            sb.AppendLine(indent + "try");
            sb.AppendLine(indent + "{");

            HttpCallBody(sb, auth, call, constructorHasAuthHeader, 3, relativePath, options.EnsureResponseIsSuccessStatusCode, options.JsonLibrary,
                options.UseCancellationTokens, options.ErrorHandlingStrategy, outputCollectionType, options.HandleMultipleResponses, options.ExecuteWithRetry);

            indent = Consts.Indent(2);
            sb.AppendLine(indent + "}");
            foreach (var catchExceptionType in options.CatchExceptionTypes)
            {
                Catch(sb, catchExceptionType, options.ErrorHandlingSinks, options.ErrorHandlingStrategy, options.LogLevel, indent);
            }
        }

        indent = Consts.Indent(1);
        sb.Append(indent + "}");
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
                    sb.AppendLine(indent + $"/// <param name=\"{path.CsPropertyName(CsharpPropertyType.Local)}\">{keyValueTypeDescription.Description}</param>");
                }
            }
        }
    }


    private static void GraphQlString(StringBuilder sb, GraphQl? graphQl, string indent)
    {
        if (graphQl == null)
        {
            return;
        }
        sb.AppendLine(indent + "var graphQlQuery = @\"");

        sb.AppendLine(indent + "{");

        sb.Append(indent + "\t\\\"query\\\": \\\"");
        sb.Append(graphQl.Query.Replace("\"", "\\\"").Replace("\r\n", "\\n").Replace("\n", "\\n"));
        sb.AppendLine("\\\",");

        sb.Append(indent + "\t\\\"variables\\\": ");
        sb.AppendLine(graphQl.Variables.Replace("\"", "\\\"").Replace("\r\n", "\\n").Replace("\n", "\\n"));
        sb.AppendLine(indent + "}\";");

        sb.AppendLine(indent + "var httpContent = new StringContent(graphQlQuery, Encoding.UTF8, \"application/json\");");
    }

    private static void Catch(StringBuilder sb, CatchExceptionTypes catchExceptionType, List<ErrorHandlingSinks> errorHandlingSinks, ErrorHandlingStrategy errorHandlingStrategy,
        LogLevel logLevel, string indent)
    {
        sb.AppendLine(indent + $"catch ({catchExceptionType} ex)");
        sb.AppendLine(indent + "{");
        indent = Consts.Indent(3);

        sb.ErrorHandlingSinks(errorHandlingSinks, logLevel, indent);

        sb.ErrorHandlingStrategy("throw;", errorHandlingStrategy, indent);

        indent = Consts.Indent(2);
        sb.AppendLine(indent + "}");
    }

    private static void HttpCallBody(StringBuilder sb, AuthSettings? auth, HttpCall call, bool authHasHeader,
        int intIndent, string relativePath, bool ensureSuccessStatusCode, JsonLibrary jsonLibrary, bool useCancellationTokens,
        ErrorHandlingStrategy errorHandlingStrategy, OutputCollectionType outputCollectionType, bool handleMultipleResponse, bool executeWithRetry)
    {
        if (call.AllResponses.Count == 0) throw new UnreachableException("No success responses found. Should never happen.");
        if (call.AllResponses.Count > 1 && handleMultipleResponse)
        {
            HttpCallMultipleResponseTypesBody(sb, auth, call, authHasHeader, intIndent, relativePath, ensureSuccessStatusCode, jsonLibrary, errorHandlingStrategy, useCancellationTokens, outputCollectionType, executeWithRetry);
        }
        else
        {
            HttpCallSingleResponseTypeBody(sb, auth, call, authHasHeader, intIndent, relativePath, ensureSuccessStatusCode, jsonLibrary, useCancellationTokens, outputCollectionType, executeWithRetry);
        }
    }

    private static void HttpCallSingleResponseTypeBody(StringBuilder sb, AuthSettings? auth, HttpCall call,
        bool authHasHeader, int intIndent, string relativePath, bool ensureSuccessStatusCode, JsonLibrary jsonLibrary,
        bool useCancellationTokens, OutputCollectionType outputCollectionType, bool executeWithRetry)
    {
        var indent = Consts.Indent(intIndent);
        sb.AddAuthorizationHeader(call.Request.Auth, indent, authHasHeader);
        UniqueHeaders(sb, call, intIndent, out var hasUniqueHeaders);

        if (call.UniqueHeaders.Where(Header.IsImportant).Any())
        {
            sb.AppendLine();
        }

        var requestHasQueryString = QueryParameters(sb, call.Request.Auth ?? auth, call, indent, relativePath);

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
            sb.AppendLine(indent + $"var httpContent = new StringContent({parameterName}, Encoding.UTF8, \"{contentType}\")");
        }
        else if (call.RequestDataType is DataType.Binary)
        {
            sb.AppendLine(indent + $"var httpContent = new StreamContent(stream);");
        }
        else if (call.RequestDataType is DataType.GraphQl)
        {
            GraphQlString(sb, call.Request.Body!.Graphql, indent);
        }

        // We need to use the HttpRequestMessage

        var httpClientCallReturnsResponse = call.SuccessResponse?.ClassName != null &&
                                            call.HttpClientFunction.Contains("Json") &&
                                            !(call.HttpClientFunction.Contains("AsJson") ||
                                              call.HttpClientFunction.Contains("AsNewtonsoftJson"));

        ReturnOrVarResponse(sb, httpClientCallReturnsResponse, indent);

        HttpClientRequest(sb, call, httpClientCallReturnsResponse, requestHasQueryString, relativePath, hasUniqueHeaders,
            useCancellationTokens, outputCollectionType, executeWithRetry);

        if (!httpClientCallReturnsResponse && ensureSuccessStatusCode)
        {
            sb.AppendLine(indent + "response.EnsureSuccessStatusCode();");
        }

        if (!httpClientCallReturnsResponse)
        {
            ReturnIfRequestDidNotReturnEarlier(sb, call, call.SuccessResponse, jsonLibrary, indent, useCancellationTokens);
        }
    }

    private static void HttpCallMultipleResponseTypesBody(StringBuilder sb, AuthSettings? auth, HttpCall call,
        bool authHasHeader, int intIndent, string relativePath, bool ensureSuccessStatusCode, JsonLibrary jsonLibrary, ErrorHandlingStrategy
         errorHandlingStrategy, bool useCancellationTokens, OutputCollectionType outputCollectionType, bool executeWithRetry)
    {
        var indent = Consts.Indent(intIndent);
        sb.AddAuthorizationHeader(call.Request.Auth, indent, authHasHeader);
        UniqueHeaders(sb, call, intIndent, out var hasUniqueHeaders);

        if (call.UniqueHeaders.Where(Header.IsImportant).Any())
        {
            sb.AppendLine();
        }

        var requestHasQueryString = QueryParameters(sb, call.Request.Auth ?? auth, call, indent, relativePath);

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
            sb.AppendLine(indent + $"var httpContent = new StreamContent(stream);");
        }
        else if (call.RequestDataType is DataType.GraphQl)
        {
            GraphQlString(sb, call.Request.Body!.Graphql, indent);
        }

        

        ReturnOrVarResponse(sb, false, indent);

        HttpClientRequest(sb, call, false, requestHasQueryString, relativePath, hasUniqueHeaders,
            useCancellationTokens, outputCollectionType, executeWithRetry);

        int i = 0;
        foreach (var response in call.AllResponses)
        {
            var ifStatement = i == 0 ? "if" : "else if";
            sb.AppendLine(indent + $"{ifStatement} (response.StatusCode == HttpStatusCode.{(HttpStatusCode) response.Code})");
            sb.AppendLine(indent + "{");
            indent = Consts.Indent(intIndent + 1);

            ReturnIfRequestDidNotReturnEarlier(sb, call, response, jsonLibrary, indent, useCancellationTokens);

            indent = Consts.Indent(intIndent);
            sb.AppendLine(indent + "}");
            i++;
        }
        sb.AppendLine(indent + "else");
        sb.AppendLine(indent + "{");
        indent = Consts.Indent(intIndent + 1);
        
        var errorMessage = $"throw new Exception($\"{call.Name}: Unexpected response. Status Code: {{response.StatusCode}}. Content: {{await response.Content.ReadAsStringAsync()}}\");";
        sb.ErrorHandlingStrategy(errorMessage, errorHandlingStrategy, indent);

        indent = Consts.Indent(intIndent);
        sb.AppendLine(indent + "}");
    }

    private static void ReturnIfRequestDidNotReturnEarlier(StringBuilder sb, HttpCall call, ApiResponse? apiResponse, JsonLibrary jsonLibrary, string indent, bool useCancellationTokens)
    {
        sb.Append(indent + "return ");
        if (apiResponse?.ClassName != null)
        {
            if (jsonLibrary == JsonLibrary.SystemTextJson)
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
            if (useCancellationTokens)
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
        bool requestHasQueryString, string relativePath, bool hasHeaders, bool useCancellationToken, OutputCollectionType outputCollectionType, bool executeWithRetry)
    {
        if (executeWithRetry)
        {
            sb.Append($"await ExecuteWithRetry(() => _httpClient.{call.HttpClientFunction}");
        }
        else
        {
            sb.Append($"await _httpClient.{call.HttpClientFunction}");
        }
        if (httpClientCallReturnsResponse)
        {
            sb.Append($"<{Common.SignatureClassName(call.SuccessResponse!.ClassName, call.SuccessResponse.RootWasArray, outputCollectionType)}>");
        }

        sb.Append('(');

        var relativePathParameter = requestHasQueryString ? "queryString" : $@"$""{relativePath}""";
        var httpClientParameters = HttpClientParameters();
        sb.Append(string.Join(", ", httpClientParameters));
        if (executeWithRetry)
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
            if (call.RequestDataType is DataType.Html or DataType.Text or DataType.Xml or DataType.Binary or DataType.GraphQl)
                list.Add("httpContent");
            if (hasHeaders)
            {
                list.Add("headers");
            }
            if (useCancellationToken)
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
            sb.Append(indent + "return ");
        }
        else
        {
            sb.Append(indent + "var response = ");
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
                    sb.AppendLine(indent + "var oauthQueryParameters = new OAuth2QueryParameters();");
                    sb.AppendLine(indent + "oauthQueryParameters.AccessToken = await GetAccessToken();");
                    requestHasOAuthQueryParameters = true;
                }
            }
        }

        var requestHasQueryString = false;
        if (call.QueryParameterClassName != null && requestHasOAuthQueryParameters)
        {
            sb.AppendLine(indent + "var parametersDict = queryParameters.ToDictionary().Unique(oauthQueryParameters.ToDictionary());");
            requestHasQueryString = true;
        }
        else if (requestHasOAuthQueryParameters)
        {
            sb.AppendLine(indent + $"var parametersDict = oauthQueryParameters.ToDictionary();");
            requestHasQueryString = true;
        }
        else if (call.QueryParameterClassName != null)
        {
            sb.AppendLine(indent + $"var parametersDict = queryParameters.ToDictionary();");
            requestHasQueryString = true;
        }

        if (auth?.TryGetApiKeyConfig(ApiKeyConfig.In, out var value) ?? false)
        {
            if (Enum.TryParse<In>(value, true, out var enumValue))
            {
                if (enumValue == In.Query)
                {
                    var keyValue = auth.TryGetApiKeyConfig(ApiKeyConfig.Key, out var key) ? key : "api_key";
                    if (requestHasQueryString)
                    {
                        sb.AppendLine(indent + $@"parametersDict.Add($""{keyValue}"", {Consts._apiKey});");
                    }
                    else
                    {
                        sb.AppendLine(indent + $@"var parametersDict = new Dictionary<string, string>() {{ {{ ""{keyValue}"", {Consts._apiKey} }} }};");
                        requestHasQueryString = true;
                    }
                }
            }
        }

        if (requestHasQueryString)
        {
            sb.AppendLine(indent + $"var queryString = QueryHelpers.AddQueryString($\"{relativePath}\", parametersDict);");
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
        sb.AppendLine(indent + "var headers = new Dictionary<string, string>()");
        sb.AppendLine(indent + "{");
        indent = Consts.Indent(intIndent + 1);
        var uniqueHeaders = call.UniqueHeaders.Where(Header.IsImportant).ToList();
        var last = uniqueHeaders.Last();
        foreach (var uniqueHeader in call.UniqueHeaders.Where(Header.IsImportant))
        {
            hasUniqueHeaders = true;
            sb.Append(indent + $"{{ $\"{uniqueHeader.Key}\", $\"{uniqueHeader.Value}\" }}");
            if (uniqueHeader != last)
            {
                sb.Append(",");
            }
            sb.AppendLine();
        }
        indent = Consts.Indent(intIndent);
        sb.AppendLine(indent + "};");
    }
}