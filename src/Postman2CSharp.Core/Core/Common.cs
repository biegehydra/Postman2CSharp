using Postman2CSharp.Core.Models;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Postman2CSharp.Core.Core
{
    public static class Common
    {
        public static void AddDefaultRequestHeader(this StringBuilder sb, string indent, string key, string value)
        {
            sb.AppendLine(indent + $@"_httpClient.DefaultRequestHeaders.Add({key}, {value});");
        }
        public static void AddDefaultAuthorizationHeader(this StringBuilder sb, string indent, string key, string value)
        {
            sb.AppendLine(indent + $@"_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue({key}, {value});");
        }

        public static void AssignVariableToConfig(this StringBuilder sb, string indent, string variable, string path)
        {
            sb.AppendLine(indent + $@"{variable} = config[""Path:To{path}""];");
        }

        public static void AddPrivateReadonlyString(this StringBuilder sb, string indent, string variableName)
        {
            sb.AppendLine(indent + $"private readonly string {variableName};");
        }

        public static void FunctionSignature(this StringBuilder sb, HttpCall call, string indent, List<HttpCallMethodParameter> methodParameters, bool isInterface = false)
        {
            if (isInterface)
            {
                sb.Append(indent + "Task");
            }
            else
            {
                sb.Append(indent + "public async Task");
            }
            sb.Append($"<{call.ResponseClassName ?? "Stream"}>");
            sb.Append($" {call.Name}(");
            sb.Append(string.Join(", ", methodParameters.Select(x => x.LocalParameterDeclaration)));
            sb.AppendLine(isInterface ? ");" : ")");
        }
    }
}
