using System.Collections.Generic;
using System.Linq;
using System.Text;
using Postman2CSharp.Core.Infrastructure;
using Postman2CSharp.Core.Models;
using Xamasoft.JsonClassGenerator.Models;

namespace Postman2CSharp.Core.Serialization;

public static class InterfaceSerializer
{
    public static string CreateInterface(List<HttpCall> httpsCalls, string nameSpace, string apiClientName, bool handleMultipleResponses, MultipleResponseHandling multipleResponseHandling, bool useCancellationTokens, OutputCollectionType outputCollectionType)
    {
        var sb = new StringBuilder();
        if (handleMultipleResponses && multipleResponseHandling == MultipleResponseHandling.OneOf && httpsCalls.Any(x => x.AllResponses.GroupBy(x => x.ClassName ?? "Stream").Count() > 1))
        {
            sb.AppendLine("using OneOf;");
        }
        sb.AppendLine("using System.IO;");
        sb.AppendLine("using System.Threading.Tasks;");
        sb.AppendLine($"namespace {nameSpace}");
        sb.AppendLine("{");
        var indent = Consts.Indent(1);
        sb.AppendLine(indent + $"public interface I{apiClientName}");
        sb.AppendLine(indent + "{");

        indent = Consts.Indent(2);
        foreach (var httpCall in httpsCalls)
        {
            var methodParameters = httpCall.MethodParameters(outputCollectionType);
            if (useCancellationTokens)
            {
                methodParameters.Add(HttpCallMethodParameter.CancellationToken);
            }
            sb.FunctionSignature(httpCall, indent, outputCollectionType, methodParameters, handleMultipleResponses, multipleResponseHandling, true);
        }
        indent = Consts.Indent(1);
        sb.AppendLine(indent + "}");
        sb.AppendLine("}");
        return sb.ToString();
    }
}