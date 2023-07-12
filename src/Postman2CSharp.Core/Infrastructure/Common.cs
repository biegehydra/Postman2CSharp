using System;
using Postman2CSharp.Core.Models;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamasoft.JsonClassGenerator.Models;

namespace Postman2CSharp.Core.Infrastructure
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

        public static void FunctionSignature(this StringBuilder sb, HttpCall call, string indent, OutputCollectionType outputCollectionType, List<HttpCallMethodParameter> methodParameters, bool handleMultipleResponse, MultipleResponseHandling multipleResponseHandling, bool isInterface = false)
        {
            if (isInterface)
            {
                sb.Append(indent + "Task");
            }
            else
            {
                sb.Append(indent + "public async Task");
            }
            if (!handleMultipleResponse || call.AllResponses.GroupBy(x => SignatureClassName(x.ClassName, x.RootWasArray, outputCollectionType)).Count() == 1)
            {
                if (call.SuccessResponse == null)
                {
#if DEBUG
                    throw new Exception("SuccessResponse null. Not supposed to happen.");
#endif
                }
                sb.Append($"<{SignatureClassName(call.SuccessResponse?.ClassName, call.SuccessResponse?.RootWasArray ?? false, outputCollectionType)}>");
            }
            else
            {
                if (multipleResponseHandling == MultipleResponseHandling.OneOf)
                {
                    sb.Append("<OneOf<");
                    var groups = call.AllResponses.GroupBy(x => SignatureClassName(x.ClassName, x.RootWasArray, outputCollectionType));
                    sb.Append(string.Join(", ", groups.Select(x => x.Key)));
                    sb.Append(">>");
                }
                else if (multipleResponseHandling == MultipleResponseHandling.Object)
                {
                    sb.Append("<object>");
                }
            }
            sb.Append($" {call.Name}(");
            sb.Append(string.Join(", ", methodParameters.Select(x => x.LocalParameterDeclaration)));
            sb.AppendLine(isInterface ? ");" : ")");
        }

        public static string SignatureClassName(string? className, bool rootWasArray,  OutputCollectionType outputCollectionType)
        {
            if (className == null)
            {
                return "Stream";
            }
            if (rootWasArray)
            {
                return outputCollectionType switch
                {
                    OutputCollectionType.Array or OutputCollectionType.ImmutableArray => $"{className}[]",
                    OutputCollectionType.MutableList or OutputCollectionType.IReadOnlyList => $"List<{className}>",
                    _ => throw new ArgumentOutOfRangeException(nameof(outputCollectionType), outputCollectionType, null)
                };
            }
            return className;
        }
    }
}
