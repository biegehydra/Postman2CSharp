using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Newtonsoft.Json.Linq;
using Postman2CSharp.Core.Infrastructure;
using Postman2CSharp.Core.Models.PostmanCollection.Http.Request;
using Postman2CSharp.Core.Models.PostmanCollection.Http.Response;
using Xamasoft.JsonClassGenerator.Models;

namespace Postman2CSharp.Core.Utilities;

public enum CsharpPropertyType
{
    Public,
    Local,
    Private
}

public interface ICsProperty
{
    public string Key { get; }
}
public interface IDirtyName
{
    public string Name { get; }
}
public static class Utils
{
    public static string CsPropertyName(this ICsProperty property, CsharpPropertyType type)
    {
        return NormalizeToCsharpPropertyName(property.Key, type);
    }
    public static string NormalizedName(this IDirtyName dirtyName)
    {
        return NormalizeToCsharpPropertyName(dirtyName.Name);
    }
    public static string NormalizeToCsharpPropertyName(string? input, CsharpPropertyType propertyType = CsharpPropertyType.Public)
    {
        if (input == null)
        {
            return string.Empty;
        }
        var words = input.Split('_', ' ', '-')
            .Select(x => Regex.Replace(x, @"\W|_", ""))
            .Where(word => !string.IsNullOrEmpty(word))
            .Select(word => char.ToUpperInvariant(word[0]) + word[1..]);

        var result = string.Join(string.Empty, words);

        if (propertyType != CsharpPropertyType.Public && !string.IsNullOrEmpty(result))
        {
            result = char.ToLowerInvariant(result[0]) + result[1..];
            if (propertyType == CsharpPropertyType.Private)
            {
                result = "_" + result;
            }
        }
        if (result.Length > 50)
        {
            result = result[^50..];
        }
        return result;
    }

    public static string HttpClientCall(string httpMethod, DataType requestDataType, DataType responseDataType, JsonLibrary jsonLibrary, bool multipleResponses)
    {
        string function;
        var suffix = jsonLibrary == JsonLibrary.SystemTextJson ? "Json" : "NewtonsoftJson";
        if (requestDataType == DataType.Json)
        {
            if (responseDataType == DataType.Json && !multipleResponses)
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
            if (responseDataType == DataType.Json && !multipleResponses)
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
        if (string.IsNullOrWhiteSpace(baseUrl) && !string.IsNullOrWhiteSpace(fullUrl))
        {
            fullUrl = fullUrl.ReplaceBrackets();
            if (fullUrl.StartsWith("http://"))
            {
                fullUrl = "https://" + fullUrl[7..];
            }
            var _fullUri = new UriBuilder(fullUrl) { Scheme = Uri.UriSchemeHttps }.Uri;
            return $"https://{_fullUri.Authority}{_fullUri.AbsolutePath}";
        }
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

        var fullUri = new UriBuilder(fullUrl) { Scheme = Uri.UriSchemeHttps }.Uri;
        var baseUri = new UriBuilder(baseUrl) { Scheme = Uri.UriSchemeHttps }.Uri;
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

        // Replace any character with '-' in front of it with its uppercase version
        str = Regex.Replace(str, @"(?<=\{[^}]*?)-([a-z])(?=[^}]*?\})", m =>
        {
            var letter = m.Value.TrimStart('-').ToUpperInvariant();
            return letter;
        });
        return str;
    }

    // The regex is to ensure variables in curly brackets don't become lower cased.
    public static string ReplaceBrackets(this string str)
    {
        // Find uppercase letters between brackets and add '-' before them
        str = Regex.Replace(str, @"(?<=\{[^}]*)[A-Z](?=[^}]*\})", m =>
        {
            var letter = m.Value;
            return "-" + letter.ToLowerInvariant();
        });

        str = str.Replace("{", "leftcurly");
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

    public static string? HtmlToPlainText(this string? html)
    {
        if (string.IsNullOrWhiteSpace(html)) return null;
        const string tagWhiteSpace = @"(>|$)(\W|\n|\r)+<"; // Matches one or more (white space or line breaks) between '>' and '<'
        const string stripFormatting = @"<[^>]*(>|$)"; // Matches any character between '<' and '>', even when end tag is missing
        const string lineBreak = @"<(br|BR)\s{0,1}\/{0,1}>"; // Matches: <br>,<br/>,<br />,<BR>,<BR/>,<BR />

        var lineBreakRegex = new Regex(lineBreak, RegexOptions.Multiline);
        var stripFormattingRegex = new Regex(stripFormatting, RegexOptions.Multiline);
        var tagWhiteSpaceRegex = new Regex(tagWhiteSpace, RegexOptions.Multiline);

        // Decode HTML specific characters
        var text = System.Net.WebUtility.HtmlDecode(html);

        // Remove tag whitespace/line breaks
        text = tagWhiteSpaceRegex.Replace(text, "><");

        // Replace <br /> with line breaks
        text = lineBreakRegex.Replace(text, Environment.NewLine);

        // Strip formatting
        text = stripFormattingRegex.Replace(text, string.Empty);

        // Insert new lines every 70 characters
        text = InsertNewLinesEvery100CharactersEfficient(text);

        return text;
    }

    private static string InsertNewLinesEvery100CharactersEfficient(string text)
    {
        var sb = new StringBuilder();
        var i = 0;
        foreach (ReadOnlySpan<char> splitLine in text.SplitLines())
        {
            foreach (var character in splitLine)
            {
                i++;
                sb.Append(character);
                if (i >= 100 && character == ' ')
                {
                    sb.AppendLine();
                    i = 0;
                }
            }
        }

        return sb.ToString();
    }

    public static string FixXmlCommentsAfterCodeAnalysis(this string sourceCode, int indent)
    {
        return ReplaceExceptFirst(sourceCode, "/// <summary>", Environment.NewLine + Consts.Indent(indent) + "/// <summary>");
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

    public static string NormalizeWhitespace(string sourceCode)
    {
        sourceCode = sourceCode.TrimEnd();

        sourceCode = sourceCode.Replace("\r\n", "\n").Replace("\r", "\n").Replace("\n", "\r\n");
        return sourceCode;
    }

    public static DataType GetRequestDataType(Request request)
    {
        if (request.Body is { Mode: "raw", Options.Raw.Language: "json" })
        {
            return DataType.Json;
        }
        else if (request.Body is { Mode: "raw", Options: null })
        {
            return DataType.Json;
        }
        if (request.Body is { Mode: "raw", Options.Raw.Language: "xml" })
        {
            return DataType.Xml;
        }
        if (request.Body is { Mode: "raw", Options.Raw.Language: "html" })
        {
            return DataType.Html;
        }
        if (request.Body is { Mode: "raw", Options.Raw.Language: "text" })
        {
            return DataType.Text;
        }
        if (request.Body is { Mode: "urlencoded", Urlencoded.Count: > 0 })
        {
            return DataType.SimpleFormData;
        }
        if (request.Body is { Mode: "formdata", Formdata.Count: > 0 })
        {
            return request.Body.Formdata.Any(x => x.FormDataType == FormDataType.File || x.Src != null) ? DataType.ComplexFormData : DataType.SimpleFormData;
        }
        else if (request.Body is { Mode: "formdata", Formdata.Count: 0 })
        {
            return DataType.QueryOnly;
        }
        if (request.Body == null)
        {
            return DataType.QueryOnly;
        }
        if (request.Body.Mode == "file")
        {
            return DataType.Binary;
        }

        if (request.Body.Mode == "graphql")
        {
            return DataType.GraphQl;
        }
        else
        {
            if (request.Body.Mode == null)
            {
                throw new ArgumentException(nameof(request.Body.Mode), $"Request mode not supported for request {request.Url.Raw}: null");
            }
            else
            {
                throw new ArgumentException(nameof(request.Body.Mode), $"Request mode not supported for request {request.Url.Raw}: {request.Body.Mode}");
            }
        }
    }

    private static readonly List<string> PossibleJsonContentTypes = new() { "json", "text" };
    public static DataType GetResponseDataType(Response? response)
    {
        if (response?.Header?.Any(x => x.Key.ToLower() == "content-type" && PossibleJsonContentTypes.Any(type => x.Value.ToLower().Contains(type))) ?? false)
        {
            return DataType.Json;
        }

        if (response?.Code == null)
        {
            return DataType.Null;
        }
        return DataType.Binary;
    }

    public static string IncrementString(string input)
    {
        // regular expression pattern for optional digits at the end of the string
        string pattern = @"^(.*?)(\d{1,2})?$";
        Regex regex = new(pattern);
        Match match = regex.Match(input);

        if (match.Success)
        {
            string prefix = match.Groups[1].Value; // the part of the string before the optional digits
            string numberStr = match.Groups[2].Value; // the optional digits at the end of the string

            int number;
            if (string.IsNullOrEmpty(numberStr))
            {
                // if there was no number at the end of the string, we add 2 to it
                number = 2;
            }
            else
            {
                // if there was a number at the end of the string, we increment it by 1
                number = int.Parse(numberStr) + 1;
            }

            // return the new string
            return prefix + number.ToString();
        }
        else
        {
            // if the string did not match the pattern, just return it unchanged
            return input;
        }
    }

    public static string GenerateUniqueName(string name, List<string> existingNames)
    {
        // If the base name doesn't exist in the list, return it as is
        if (!existingNames.Contains(name))
        {
            existingNames.Add(name);
            return name;
        }

        if (TryParseStringNumberEnding(name, out var nameBase, out var numberPart))
        {
            int counter = numberPart;
            while (existingNames.Contains(nameBase + counter))
            {
                counter++;
            }
            existingNames.Add(nameBase + counter);
            return nameBase + counter;
        }
        else
        {
            int counter = 2;
            while (existingNames.Contains(name + counter))
            {
                counter++;
            }
            existingNames.Add(name + counter);
            return name + counter;
        }
    }

    private static bool TryParseStringNumberEnding(string input, out string basePart, out int numberPart)
    {
        var match = Regex.Match(input, @"(.*?)(\d+)$");

        if (!match.Success)
        {
            basePart = string.Empty;
            numberPart = -1;
            return false;
        }

        basePart = match.Groups[1].Value;
        numberPart = int.Parse(match.Groups[2].Value);

        return true;
    }
}