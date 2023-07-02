using System.Collections.Generic;
using System.Text;
using Postman2CSharp.Core.Core;
using Postman2CSharp.Core.Models;

namespace Postman2CSharp.Core.Serialization;

public static class InterfaceSerializer
{
    public static string CreateInterface(List<HttpCall> httpsCalls, string nameSpace, string apiClientName, bool useCancellationTokens)
    {
        var sb = new StringBuilder();
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
            var methodParameters = httpCall.MethodParameters();
            if (useCancellationTokens)
            {
                methodParameters.Add(HttpCallMethodParameter.CancellationToken);
            }
            sb.FunctionSignature(httpCall, indent, methodParameters, true);
        }
        indent = Consts.Indent(1);
        sb.AppendLine(indent + "}");
        sb.AppendLine("}");
        return sb.ToString();
    }
}