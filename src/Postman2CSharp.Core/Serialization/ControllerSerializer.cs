using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Postman2CSharp.Core.Infrastructure;
using Postman2CSharp.Core.Models;
using Postman2CSharp.Core.Models.PostmanCollection.Authorization;
using Postman2CSharp.Core.Models.PostmanCollection.Http;
using Postman2CSharp.Core.Models.PostmanCollection.Http.Request;
using Postman2CSharp.Core.Utilities;
using Xamasoft.JsonClassGenerator.Models;

namespace Postman2CSharp.Core.Serialization
{
    public static class ControllerSerializer
    {
        public static string SerializeController(ApiClient apiClient)
        {
            var uri = new Uri(apiClient.BaseUrl?.ReplaceBrackets() ?? "");
            var uriSegments = uri.AbsolutePath.Split('/', StringSplitOptions.RemoveEmptyEntries).ToList();

            var sb = new StringBuilder();
            sb.AppendLine("[ApiController]");
            var absolutePath = string.IsNullOrWhiteSpace(uri.AbsolutePath.AddBackBrackets()) 
                ? string.Empty : $"(\"{uri.AbsolutePath.AddBackBrackets()}\")";

            sb.AppendLine($"[Route{absolutePath}]");
            sb.AppendLine($"public class {apiClient.ControllerClassName} : ControllerBase");
            sb.AppendLine("{");
            var last = apiClient.HttpCalls.Last();
            foreach (var httpCall in apiClient.HttpCalls)   
            {
                SerializeEndpoint(sb, apiClient, httpCall, uriSegments, apiClient.BaseUrl, 1, apiClient.CollectionType);
                if (httpCall != last)
                {
                    sb.AppendLine();
                }
            }
            sb.AppendLine("}");
            return sb.ToString();
        }
        private static void SerializeEndpoint(StringBuilder sb, ApiClient apiClient, HttpCall httpCall, List<string> baseUriSegments, string? baseUrl, int intIndent, OutputCollectionType outputCollectionType)
        {
            var indent = Consts.Indent(intIndent);
            var attribute = Attribute(httpCall);
            var relativePath = Utils.ExtractRelativePath(baseUrl ?? "", httpCall.Request.Url.Raw);
            relativePath = string.IsNullOrWhiteSpace(relativePath) ? string.Empty : $"(\"{relativePath}\")";

            XmlComment(sb, apiClient.Options.XmlCommentTypes, httpCall.RequestClassName, httpCall.Request.Description,
                httpCall.Request.Url.Path, httpCall.Request.Url.Query, httpCall.Request.Url.Variable, httpCall.Request.Header, indent);
            foreach (var response in httpCall.AllResponses.OrderBy(x => x.Code))
            {
                sb.Append(indent + $"[ProducesResponseType(StatusCodes.{response.StatusCode()}");
                sb.Append($", Type = typeof({Common.SignatureClassName(response.ClassName, response.RootWasArray, outputCollectionType)})");
                sb.AppendLine(")]");
            }
            sb.AppendLine(indent + $"[{attribute}{relativePath}]");
            var responseType = Common.SignatureClassName(httpCall.SuccessResponse?.ClassName, httpCall.SuccessResponse?.RootWasArray ?? false, outputCollectionType);
            sb.AppendLine(indent + $"public IActionResult<{responseType}> {httpCall.Name}({ControllerParameters(httpCall, apiClient.CommonHeaders, baseUriSegments, outputCollectionType)})");
            sb.AppendLine(indent + "{");
            indent = Consts.Indent(intIndent + 1);
            sb.AppendLine(indent + "throw new NotImplementedException();");
            indent = Consts.Indent(intIndent);
            sb.AppendLine(indent + "}");
        }

        private static void XmlComment(StringBuilder sb, List<XmlCommentTypes> commentTypes, string? requestClassName, string? requestDescription, List<Path>? paths, List<QueryParameter>? queryParameters, List<KeyValueTypeDescription>? variables, List<Header> headers, string indent)
        {
            if (commentTypes.Contains(XmlCommentTypes.Request) && !string.IsNullOrWhiteSpace(requestDescription) && !string.IsNullOrWhiteSpace(requestClassName))
            {
                var xmlParam = XmlCommentHelpers.ToXmlParam(requestDescription, requestClassName, indent);
                sb.Append(xmlParam);
            }

            if (commentTypes.Contains(XmlCommentTypes.PathVariables) && paths != null && variables != null)
            {
                foreach (var path in paths)
                {
                    if (variables.FirstOrDefault(x => !string.IsNullOrWhiteSpace(x.Key) && x.Key == path.Value?.Replace(":", string.Empty)) is
                        { } keyValueTypeDescription)
                    {
                        if (!string.IsNullOrWhiteSpace(keyValueTypeDescription.Description))
                        {
                            var xmlParam = XmlCommentHelpers.ToXmlParam(keyValueTypeDescription.Description,
                                path.CsPropertyName(CsharpPropertyType.Local), indent);
                            sb.Append(xmlParam);
                        }
                    }
                }
            }
            if (commentTypes.Contains(XmlCommentTypes.QueryParameters) && queryParameters != null)
            {
                foreach (var parameter in queryParameters.Where(parameter => !string.IsNullOrWhiteSpace(parameter.Description)))
                {
                    var xmlParam = XmlCommentHelpers.ToXmlParam(parameter.Description,
                        parameter.CsPropertyName(CsharpPropertyType.Local), indent);
                    sb.Append(xmlParam);
                }
            }

            if (commentTypes.Contains(XmlCommentTypes.Header))
            {
                foreach (var headerGroup in headers.GroupBy(x => x.Key).Where(header => header.Any(x => !string.IsNullOrWhiteSpace(x.Description))))
                {
                    var header = headerGroup.First();
                    sb.AppendLine(indent + $"/// <param name=\"{header.Key}\">{header.Description}</param>");
                }
            }
        }

        private static string ControllerParameters(HttpCall httpCall, List<Header> sharedHeaders, List<string> baseUriSegments, OutputCollectionType outputCollectionType)
        {
            var allHeaders = new List<Header>(sharedHeaders);
            allHeaders.AddRange(httpCall.UniqueHeaders);

            var parameters = new List<string>();
            if (httpCall.RequestClassName != null)
            {
                parameters.Add($"[FromBody] {Common.SignatureClassName(httpCall.RequestClassName, httpCall.RequestRootWasArray, outputCollectionType)} {Lower(httpCall.RequestClassName)}");
            }
            if (httpCall.FormDataClassName != null)
            {
                parameters.Add($"[FromForm] {httpCall.FormDataClassName} {Lower(httpCall.FormDataClassName)}");
            }
            foreach (var parameter in httpCall.Request.Url.Query ?? new ())
            {
                parameters.Add($"[FromQuery] string {parameter.CsPropertyName(CsharpPropertyType.Local)}");
            }
            foreach (var path in httpCall.Request.Url.Path?.Where(x => x.IsVariable()) ?? new List<Path>())
            {
                if (!baseUriSegments.Contains(path))
                {
                    parameters.Add($"[FromRoute] string " + path.CsPropertyName(CsharpPropertyType.Local));
                }
            }
            foreach (var header in allHeaders.GroupBy(x => x.Key))
            {
                parameters.Add($"[FromHeader(Name = \"{header.Key}\")] string {header.First().CsPropertyName(CsharpPropertyType.Local)}");
            }
            return string.Join(", ", parameters.ToArray());
        }

        private static string Lower(string value)
        {
            return char.ToLower(value[0]) + value[1..];
        }

        private static string Attribute(HttpCall httpCall)
        {
            return httpCall.Request.Method.ToUpper() switch
            {
                "GET" => $"HttpGet",
                "PUT" => $"HttpPut",
                "POST" => $"HttpPost",
                "PATCH" => $"HttpPatch",
                "DELETE" => $"HttpDelete",
                _ => throw new NotImplementedException(),
            };
        }
    }
}
