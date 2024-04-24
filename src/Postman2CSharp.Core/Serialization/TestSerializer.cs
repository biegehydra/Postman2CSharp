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
            sb.AppendLineIndented(indent, "[TestClass]");
            sb.AppendLineIndented(indent, $"public class {apiclient.TestClassName}");
            sb.AppendLineIndented(indent, "{");
            indent = Consts.Indent(2);
            var _apiClient = Utils.NormalizeToCsharpPropertyName(apiclient.Name, CsharpPropertyType.Private);
            sb.AppendLineIndented(indent, $"private readonly {apiclient.InterfaceName} {_apiClient};");
            sb.AppendLineIndented(indent, "[ClassInitialize]");
            sb.AppendLineIndented(indent, "public void Setup()");
            sb.AppendLineIndented(indent, "{");
            indent = Consts.Indent(3);
            sb.AppendLineIndented(indent, "// TODO: Setup");
            indent = Consts.Indent(2);
            sb.AppendLineIndented(indent, "}");
            foreach (var httpCall in apiclient.HttpCalls)
            {
                sb.AppendLine();
                sb.AppendLineIndented(indent, "[TestMethod]");
                sb.AppendLineIndented(indent, $"public async Task {httpCall.Name + "_ReturnsValidResult"}()");
                sb.AppendLineIndented(indent, "{");
                indent = Consts.Indent(3);
                List<string> parameterNames = new List<string>();
                foreach (var httpCallMethodParameter in httpCall.MethodParameters(apiclient.Options.OutputCollectionType))
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
                        case HttpCallMethodParameterType.GraphQlVariables:
                            AppendGeneratedClass(sb, httpCall.GraphQlVariablesClassName, httpCall.GraphQlVariablesSourceCode);
                            parameterNames.Add(httpCall.GraphQlVariablesClassName!.ToLower());
                            break;
                        case HttpCallMethodParameterType.RawText:
                            sb.AppendLineIndented(indent, $"var {httpCallMethodParameter.ParameterName} = \"Test Data\";");
                            parameterNames.Add(httpCallMethodParameter.ParameterName);
                            break;
                        case HttpCallMethodParameterType.Path:
                            sb.AppendLineIndented(indent, $"var {httpCallMethodParameter.ParameterName} = \"Test Data\";");
                            parameterNames.Add(httpCallMethodParameter.ParameterName);
                            break;
                        case HttpCallMethodParameterType.Stream:
                            sb.AppendLineIndented(indent, "var stream = new Stream();");
                            parameterNames.Add("stream");
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                }
                sb.AppendLineIndented(indent, $"var response = await {_apiClient}.{httpCall.Name}({string.Join(", ", parameterNames)})");
                sb.AppendLineIndented(indent, "Assert.IsNotNull(response);");
                indent = Consts.Indent(2);
                sb.AppendLineIndented(indent, "}");
            }
            indent = Consts.Indent(1);
            sb.AppendLineIndented(indent, "}");
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
