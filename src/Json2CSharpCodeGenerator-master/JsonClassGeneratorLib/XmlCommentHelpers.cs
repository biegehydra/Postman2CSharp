using System;
using System.Linq;
using System.Text;

namespace Postman2CSharp.Core;

public static class XmlCommentHelpers
{
    public static string ToXmlSummary(string str, string indent)
    {
        if (string.IsNullOrWhiteSpace(str)) return "";
        var sb = new StringBuilder();
        var lines = str.Split(new[] {"\r\n", "\r", "\n\n", "\n" }, StringSplitOptions.None);
        sb.AppendLine(indent + "/// <summary>");
        foreach (var line in lines)
        {
            if (line == lines.Last() && string.IsNullOrWhiteSpace(line)) continue;
            sb.AppendLine(indent + "/// " + line.Replace("\n", string.Empty).Replace("\r", string.Empty));
        }

        sb.AppendLine(indent + "/// </summary>");
        return sb.ToString();
    }

    public static string ToXmlParam(string str, string name, string indent)
    {
        if (string.IsNullOrWhiteSpace(str)) return "";
        var sb = new StringBuilder();
        var lines = str.Split(new[] {"\r\n", "\r", "\n\n", "\n"}, StringSplitOptions.None);
        sb.AppendLine(indent + $"/// <param name=\"{name}\">");
        foreach (var line in lines)
        {
            if (line == lines.Last() && string.IsNullOrWhiteSpace(line)) continue;
            sb.AppendLine(indent + "/// " + line.Replace("\n", string.Empty).Replace("\r", string.Empty));
        }

        sb.AppendLine(indent + "/// </param>");
        return sb.ToString();
    }
}