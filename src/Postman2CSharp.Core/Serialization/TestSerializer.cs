using System;
using System.Collections.Generic;
using System.Text;
using ObjectInitializerGenerator;
using Postman2CSharp.Core.Infrastructure;
using Postman2CSharp.Core.Models;
using Postman2CSharp.Core.Utilities;

namespace Postman2CSharp.Core.Serialization
{
    public class TestSerializer
    {
        public static string SerializeTestClass(ApiClient apiclient)
        {
            var sb = new StringBuilder();
            sb.AppendLine("using System;");
            sb.AppendLine($"using {apiclient.Name};");
            sb.AppendLine($"namespace {apiclient.Name + ".Tests"}");
            sb.AppendLine("{");
            var indent = Consts.Indent(1);
            sb.AppendLine(indent + "[TestClass]");
            sb.AppendLine(indent + $"public class {apiclient.TestClassName}");
            sb.AppendLine(indent + "{");
            indent = Consts.Indent(2);
            var _apiClient = Utils.NormalizeToCsharpPropertyName(apiclient.Name, CsharpPropertyType.Private);
            sb.AppendLine(indent + $"private readonly {apiclient.InterfaceName} {_apiClient};");
            sb.AppendLine(indent + "[ClassInitialize]");
            sb.AppendLine(indent + "public void Setup()");
            sb.AppendLine(indent + "{");
            indent = Consts.Indent(3);
            sb.AppendLine(indent + "// TODO: Setup");
            indent = Consts.Indent(2);
            sb.AppendLine(indent + "}");
            foreach (var httpCall in apiclient.HttpCalls)
            {
                sb.AppendLine();
                sb.AppendLine(indent + "[TestMethod]");
                sb.AppendLine(indent + $"public async Task {httpCall.Name + "_ReturnsValidResult"}()");
                sb.AppendLine(indent + "{");
                indent = Consts.Indent(3);
                List<string> parameterNames = new List<string>();
                foreach (var httpCallMethodParameter in httpCall.MethodParameters())
                {
                    switch (httpCallMethodParameter.Type)
                    {
                        case HttpCallMethodParameterType.QueryParameters:
                            AppendGeneratedClass(sb, httpCall.QueryParameterClassName, httpCall.QueryParameterSourceCode);
                            parameterNames.Add(httpCall.QueryParameterClassName!.ToLower());
                            break;
                        case HttpCallMethodParameterType.Request:
                            AppendGeneratedClass(sb, httpCall.RequestClassName, httpCall.RequestSourceCode);
                            parameterNames.Add(httpCall.RequestClassName!.ToLower());
                            break;
                        case HttpCallMethodParameterType.FormData:
                            AppendGeneratedClass(sb, httpCall.FormDataClassName, httpCall.FormDataSourceCode);
                            parameterNames.Add(httpCall.FormDataClassName!.ToLower());
                            break;
                        case HttpCallMethodParameterType.RawText:
                            sb.AppendLine(indent + $"var {httpCallMethodParameter.ParameterName} = \"Test Data\";");
                            parameterNames.Add(httpCallMethodParameter.ParameterName);
                            break;
                        case HttpCallMethodParameterType.Path:
                            sb.AppendLine(indent + $"var {httpCallMethodParameter.ParameterName} = \"Test Data\";");
                            parameterNames.Add(httpCallMethodParameter.ParameterName);
                            break;
                        case HttpCallMethodParameterType.Stream:
                            sb.AppendLine(indent + "var stream = new Stream();");
                            parameterNames.Add("stream");
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                }
                sb.AppendLine(indent + $"var response = await {_apiClient}.{httpCall.Name}({string.Join(", ", parameterNames)})");
                sb.AppendLine(indent + "Assert.IsNotNull(response);");
                indent = Consts.Indent(2);
                sb.AppendLine(indent + "}");
            }
            indent = Consts.Indent(1);
            sb.AppendLine(indent + "}");
            sb.AppendLine("}");
            return sb.ToString();
        }

        private static void AppendGeneratedClass(StringBuilder sb, string? className, string? sourceCode)
        {
            if (string.IsNullOrWhiteSpace(className) || string.IsNullOrWhiteSpace(sourceCode)) return;
            var generator = new Generator(new CSharpWriter());
            generator.Analyse(sourceCode);
            var queryParameterInitializer = generator.Write(className);
            sb.AppendLine(Utils.PadLeft(queryParameterInitializer, 3));
        }
    }
}
