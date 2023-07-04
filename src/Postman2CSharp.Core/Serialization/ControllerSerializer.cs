using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Postman2CSharp.Core.Infrastructure;
using Postman2CSharp.Core.Models;
using Postman2CSharp.Core.Models.PostmanCollection.Http;
using Postman2CSharp.Core.Utilities;

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
                SerializeEndpoint(sb, apiClient.CommonHeaders, httpCall, uriSegments, apiClient.BaseUrl, 1);
                if (httpCall != last)
                {
                    sb.AppendLine();
                }
            }
            sb.AppendLine("}");
            return sb.ToString();
        }
        private static void SerializeEndpoint(StringBuilder sb, List<Header> sharedHeaders, HttpCall httpCall, List<string> baseUriSegments, string? baseUrl, int intIndent)
        {
            var indent = Consts.Indent(intIndent);
            var attribute = Attribute(httpCall);
            var relativePath = Utils.ExtractRelativePath(baseUrl ?? "", httpCall.Request.Url.Raw);
            relativePath = string.IsNullOrWhiteSpace(relativePath) ? string.Empty : $"(\"{relativePath}\")";
            sb.AppendLine(indent + $"[{attribute}{relativePath})]");
            var responseType = httpCall.ResponseClassName ?? "Stream";
            sb.AppendLine(indent + $"public IActionResult<{responseType}> {httpCall.Name}({ControllerParameters(httpCall, sharedHeaders, baseUriSegments)})");
            sb.AppendLine(indent + "{");
            indent = Consts.Indent(intIndent + 1);
            sb.AppendLine(indent + "throw new NotImplementedException();");
            indent = Consts.Indent(intIndent);
            sb.AppendLine(indent + "}");
        }

        private static string ControllerParameters(HttpCall httpCall, List<Header> sharedHeaders, List<string> baseUriSegments)
        {
            var allHeaders = new List<Header>(sharedHeaders);
            allHeaders.AddRange(httpCall.UniqueHeaders);

            var parameters = new List<string>();
            if (httpCall.RequestClassName != null)
            {
                parameters.Add($"[FromBody] {httpCall.RequestClassName} {Lower(httpCall.RequestClassName)}");
            }
            if (httpCall.FormDataClassName != null)
            {
                parameters.Add($"[FromForm] {httpCall.FormDataClassName} {Lower(httpCall.FormDataClassName)}");
            }
            foreach (var parameter in httpCall.Request.Url.Query ?? new ())
            {
                parameters.Add($"[FromQuery] string {Utils.NormalizeToCsharpPropertyName(parameter.Key, CsharpPropertyType.Local)}");
            }
            foreach (var path in httpCall.Request.Url.Path?.Where(x => x.IsVariable()) ?? new List<Path>())
            {
                if (!baseUriSegments.Contains(path))
                {
                    parameters.Add($"[FromRoute] string " + path.LocalVariableName);
                }
            }
            foreach (var header in allHeaders.GroupBy(x => x.Key))
            {
                parameters.Add($"[FromHeader(Name = \"{header.Key}\")] string {Utils.NormalizeToCsharpPropertyName(header.Key, CsharpPropertyType.Local)}");
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
