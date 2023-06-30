using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Newtonsoft.Json.Linq;
using Postman2CSharp.Core.Core;
using Xamasoft.JsonClassGenerator.Models;

namespace Postman2CSharp.Core;

public enum CsharpPropertyType
{
    Public,
    Local,
    Private
}
public static class Helpers
{
    public static string ConsolidateNamespaces(string sourceCode)
    {
        var tree = CSharpSyntaxTree.ParseText(sourceCode);
        var root = tree.GetCompilationUnitRoot();

        var namespaces = root.DescendantNodes().OfType<NamespaceDeclarationSyntax>().GroupBy(n => n.Name.ToString());

        // Create a new root without any namespace
        var newRoot = SyntaxFactory.CompilationUnit()
            .WithExterns(root.Externs)
            .WithUsings(root.Usings)
            .WithAttributeLists(root.AttributeLists)
            .WithLeadingTrivia(root.GetLeadingTrivia())
            .WithTrailingTrivia(root.GetTrailingTrivia())
            .WithMembers(new SyntaxList<MemberDeclarationSyntax>());

        foreach (var namespaceGroup in namespaces)
        {
            var consolidatedNamespace = namespaceGroup.First();

            // Add all members from duplicate namespaces to the first one
            foreach (var duplicateNamespace in namespaceGroup.Skip(1))
            {
                consolidatedNamespace = consolidatedNamespace.AddMembers(duplicateNamespace.Members.ToArray());
            }

            // Add consolidated namespace to the new root
            newRoot = newRoot.AddMembers(consolidatedNamespace);
        }

        return newRoot.NormalizeWhitespace().ToFullString().FixXmlCommentsAfterCodeAnalysis();
    }

    public static string NormalizeToCsharpPropertyName(string? input, CsharpPropertyType propertyType = CsharpPropertyType.Public)
    {
        if (input == null)
        {
            return string.Empty;
        }
        var words = input.Split('_')
            .Select(x => Regex.Replace(x, @"\W|_", ""))
            .Where(word => !string.IsNullOrEmpty(word))
            .Select(word => char.ToUpperInvariant(word[0]) + word[1..]);

        var result = string.Join(string.Empty, words);

        if (propertyType != CsharpPropertyType.Public  && !string.IsNullOrEmpty(result))
        {
            result = char.ToLowerInvariant(result[0]) + result[1..];
            if (propertyType == CsharpPropertyType.Private)
            {
                result = "_" + result;
            }
        }

        return result;
    }

    public static string HttpClientCall(string httpMethod, DataType requestDataType, DataType responseDataType, JsonLibrary jsonLibrary)
    {
        string function;
        var suffix = jsonLibrary == JsonLibrary.SystemTextJson ? "Json" : "NewtonsoftJson";
        if (requestDataType == DataType.Json)
        {
            if (responseDataType == DataType.Json)
            {
                function = httpMethod.ToUpper() switch
                {
                    "GET" => $"Get{suffix}Async",
                    "PUT" => $"Put{suffix}Async",
                    "POST" => $"Post{suffix}Async",
                    "PATCH" => $"Patch{suffix}Async",
                    "DELETE" => $"Delete{suffix}Async",
                    _ => throw new NotImplementedException(),
                };
            }
            else
            {
                function = httpMethod.ToUpper() switch
                {
                    "GET" => $"GetAs{suffix}Async",
                    "PUT" => $"PutAs{suffix}Async",
                    "POST" => $"PostAs{suffix}Async",
                    "PATCH" => $"PatchAs{suffix}Async",
                    "DELETE" => $"DeleteAs{suffix}Async",
                    _ => throw new NotImplementedException(),
                };
            }
        }
        else
        {
            if (responseDataType == DataType.Json)
            {
                function = httpMethod.ToUpper() switch
                {
                    "GET" => $"GetFrom{suffix}Async",
                    "PUT" => $"PutFrom{suffix}Async",
                    "POST" => $"PostFrom{suffix}Async",
                    "PATCH" => $"PatchFrom{suffix}Async",
                    "DELETE" => $"DeleteFrom{suffix}Async",
                    _ => throw new NotImplementedException(),
                };
            }
            else
            {
                function = httpMethod.ToUpper() switch
                {
                    "GET" => "GetAsync",
                    "PUT" => "PutAsync",
                    "POST" => "PostAsync",
                    "PATCH" => "PatchAsync",
                    "DELETE" => "DeleteAsync",
                    _ => throw new NotImplementedException(),
                };
            }
        }

        return function;
    }

    public static string ExtractRelativePath(string? baseUrl, string? fullUrl, bool safe = true)
    {
        if (baseUrl is null || fullUrl is null)
        {
            return string.Empty;
        }
        baseUrl = baseUrl.ReplaceBrackets();
        fullUrl = fullUrl.ReplaceBrackets();
        if (baseUrl.StartsWith("http://"))
        {
            baseUrl = "https://" + baseUrl[7..];
        }
        if (fullUrl.StartsWith("http://"))
        {
            fullUrl = "https://" + fullUrl[7..];
        }
        var fullUriBuilder = new UriBuilder(fullUrl) { Scheme = Uri.UriSchemeHttps };
        var baseUriBuilder = new UriBuilder(baseUrl) { Scheme = Uri.UriSchemeHttps };

        var fullUri = fullUriBuilder.Uri;
        var baseUri = baseUriBuilder.Uri;
        if (!fullUri.AbsoluteUri.StartsWith(baseUri.AbsoluteUri))
        {
            if (safe)
            {
                throw new ArgumentException("The full URI must start with the base URI.");
            }
            else return string.Empty;
        }


        var relativePath = fullUri.AbsolutePath[baseUri.AbsolutePath.Length..].Trim('/');
        relativePath = relativePath.AddBackBrackets();
        return relativePath;
    }

    public static string AddBackBrackets(this string str)
    {
        str = str.Replace("leftcurly", "{"); // better way to do this?
        str = str.Replace("rightcurly", "}");
        str = str.Replace("%7B", "{"); // Add brackets back in if they got replaced in the process
        str = str.Replace("%7D", "}");
        return str;
    }
    public static string ReplaceBrackets(this string str)
    {
        str = str.Replace("{", "leftcurly"); // better way to do this?
        str = str.Replace("}", "rightcurly");
        return str;
    }


    /// <summary>
    /// Gets the largest common base from a list of strings
    /// </summary>
    /// <param name="strings"></param>
    /// <returns></returns>
    public static string? GetCommonBase(List<string> strings)
    {
        if (!strings.Any() || strings.Any(string.IsNullOrWhiteSpace))
            return null;

        var commonBase = strings[0];

        for (var i = 1; i < strings.Count; i++)
        {
            commonBase = string.Concat(commonBase.TakeWhile((c, index) => index < strings[i].Length && c == strings[i][index]));
        }

        return commonBase.Length > 4 ? commonBase : null;
    }

    public static string? GetLongestSubstring(List<string> strings)
    {
        if (!strings.Any() || strings.Any(string.IsNullOrWhiteSpace))
            return null;

        string? longestCommonSubstring = null;
        var minLengthStr = strings.OrderBy(s => s.Length).First();

        for (var len = minLengthStr.Length; len > 0; len--)
        {
            for (var strIndex = 0; strIndex <= minLengthStr.Length - len; strIndex++)
            {
                var currentSubstring = minLengthStr.Substring(strIndex, len);

                if (char.IsUpper(currentSubstring[0]) && strings.All(str => str.Contains(currentSubstring)))
                {
                    if (longestCommonSubstring == null || currentSubstring.Length > longestCommonSubstring.Length)
                        longestCommonSubstring = currentSubstring;
                }
            }
        }

        return longestCommonSubstring?.Length > 4 ? longestCommonSubstring : null;
    }

    public enum NamingPolicy
    {
        Unknown,
        CamelCase,
        PascalCase,
        SnakeCase
    }

    public static NamingPolicy DetectNamingPolicy(string jsonString)
    {
        var json = JObject.Parse(jsonString);

        foreach (var property in json.Properties())
        {
            string name = property.Name;

            if (name.All(c => char.IsLower(c) || c == '_')) // snake_case check
            {
                if (name.Contains('_'))
                {
                    return NamingPolicy.SnakeCase;
                }
                else
                {
                    return NamingPolicy.Unknown;
                }
            }

            if (char.IsUpper(name[0])) // PascalCase check
            {
                return NamingPolicy.PascalCase;
            }
            else if (char.IsLower(name[0])) // camelCase check
            {
                return NamingPolicy.CamelCase;
            }
        }

        return NamingPolicy.Unknown;
    }

    public static string PadLeft(string sourceCode, int indentCount = 1)
    {
        using StringReader reader = new(sourceCode);
        var paddedParagraph = string.Empty;
        // Read each line, add padding, and join them back
        while (reader.ReadLine() is { } line)
        {
            var paddedLine = line.PadLeft(line.Length + 4 * indentCount);
            paddedParagraph += paddedLine + Environment.NewLine;
        }
        return paddedParagraph;
    }

    public static string HtmlToPlainText(this string html)
    {
        const string tagWhiteSpace = @"(>|$)(\W|\n|\r)+<";//matches one or more (white space or line breaks) between '>' and '<'
        const string stripFormatting = @"<[^>]*(>|$)";//match any character between '<' and '>', even when end tag is missing
        const string lineBreak = @"<(br|BR)\s{0,1}\/{0,1}>";//matches: <br>,<br/>,<br />,<BR>,<BR/>,<BR />
        var lineBreakRegex = new Regex(lineBreak, RegexOptions.Multiline);
        var stripFormattingRegex = new Regex(stripFormatting, RegexOptions.Multiline);
        var tagWhiteSpaceRegex = new Regex(tagWhiteSpace, RegexOptions.Multiline);

        var text = html;
        //Decode html specific characters
        text = System.Net.WebUtility.HtmlDecode(text);
        //Remove tag whitespace/line breaks
        text = tagWhiteSpaceRegex.Replace(text, "><");
        //Replace <br /> with line breaks
        text = lineBreakRegex.Replace(text, Environment.NewLine);
        //Strip formatting
        text = stripFormattingRegex.Replace(text, string.Empty);

        return text;
    }
     
    public static string FixXmlCommentsAfterCodeAnalysis(this string sourceCode)
    {
        return ReplaceExceptFirst(sourceCode, "/// <summary>", Environment.NewLine + Consts.Indent(2) + "/// <summary>");
    }

    private static string ReplaceExceptFirst(string sourceCode, string oldValue, string newValue)
    {
        var pos = sourceCode.IndexOf(oldValue, StringComparison.Ordinal);
        if (pos == -1)
        {
            return sourceCode;
        }

        var nextPos = sourceCode.IndexOf(oldValue, pos + oldValue.Length, StringComparison.Ordinal);

        if (nextPos == -1)
        {
            return sourceCode;
        }

        return sourceCode[..nextPos] + sourceCode[nextPos..].Replace(oldValue, newValue);
    }
}