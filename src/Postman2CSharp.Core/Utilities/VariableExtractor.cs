using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Postman2CSharp.Core.Models.PostmanCollection;
using Postman2CSharp.Core.Models.PostmanCollection.Authorization;
using Postman2CSharp.Core.Models.PostmanCollection.Http;

namespace Postman2CSharp.Core.Utilities
{
    public static class VariableExtractor
    {
        // We purposefully don't check URL usages here
        public static List<VariableUsage> ExtractAndReplaceVariableUsagesWithPrivateVariables(CollectionItem rootItem, List<CollectionVariable> collectionVariables)
        {
            var collectionVariableUsages = new List<VariableUsage>();

            if (rootItem.RequestItems() == null || !rootItem.RequestItems()!.Any())
            {
                return collectionVariableUsages;
            }

            // Initialize the dictionary with collection variables
            foreach (var collectionVariable in collectionVariables)
            {
                collectionVariableUsages.Add(new(collectionVariable.Key, collectionVariable.Value, CsharpPropertyType.Private));
            }

            var usedVariables = new List<(string Original, CsharpPropertyType CsharpPropertyType)>();

            foreach (var requestItem in rootItem.RequestItems()!)
            {
                if (requestItem.Request is not { } request) continue;

                if (request.Url is { } url)
                {
                    // Check URL properties for variables
                    var result = ExtractAndReplaceVariables(url.Raw, CsharpPropertyType.Private);
                    url.Raw = result.StringWithCSharpInterpolation!;
                    usedVariables.AddRange(result.Variables);

                    result = ExtractAndReplaceVariables(url.Protocol, CsharpPropertyType.Private);
                    url.Protocol = result.StringWithCSharpInterpolation!;
                    usedVariables.AddRange(result.Variables);

                    result = ExtractAndReplaceVariables(url.Port, CsharpPropertyType.Private);
                    url.Port = result.StringWithCSharpInterpolation!;
                    usedVariables.AddRange(result.Variables);

                    if (url.Host is { Count: > 0 })
                    {
                        for (var i = 0; i < url.Host.Count; i++)
                        {
                            result = ExtractAndReplaceVariables(url.Host[i], CsharpPropertyType.Private);
                            url.Host[i] = result.StringWithCSharpInterpolation!;
                            usedVariables.AddRange(result.Variables);
                        }
                    }

                    if (url.Path is { Count: > 0 })
                    {
                        for (var i = 0; i < url.Path.Count; i++)
                        {
                            result = ExtractAndReplaceVariables(url.Path[i], CsharpPropertyType.Private);
                            url.Path[i] = result.StringWithCSharpInterpolation!;
                            usedVariables.AddRange(result.Variables);
                        }
                    }
                }

                if (request.Body is { } body)
                {
                    // Check Body properties for variables
                    var result = ExtractAndReplaceVariables(body.Mode, CsharpPropertyType.Private);
                    body.Mode = result.StringWithCSharpInterpolation!;
                    usedVariables.AddRange(result.Variables);

                    result = ExtractAndReplaceVariables(body.Raw, CsharpPropertyType.Private);
                    body.Raw = result.StringWithCSharpInterpolation!;
                    usedVariables.AddRange(result.Variables);
                }

                if (request.Header is { Count: > 0 })
                {
                    // Check Header properties for variables
                    foreach (var header in request.Header)
                    {
                        var result = ExtractAndReplaceVariables(header.Key, CsharpPropertyType.Private);
                        header.Key = result.StringWithCSharpInterpolation!;
                        usedVariables.AddRange(result.Variables);

                        result = ExtractAndReplaceVariables(header.Value, CsharpPropertyType.Private);
                        header.Value = result.StringWithCSharpInterpolation!;
                        usedVariables.AddRange(result.Variables);
                    }
                }
            }

            // Add any additional used variables to the dictionary
            foreach (var (original, csPropertyType) in usedVariables.Distinct())
            {
                if (collectionVariableUsages.Any(x => x.Original == original))
                {
                    continue;
                }
                collectionVariableUsages.Add(new(original, null, csPropertyType));
            }

            // Remove any unused variables from the dictionary. This is necessary for collection variables that aren't used / we don't care about
            collectionVariableUsages.RemoveAll(collectionVariableUsage =>
                usedVariables.All(x => x.Original != collectionVariableUsage.Original));

            return collectionVariableUsages;
        }

        public static void ReplaceAllVariableUsageInAuthWithValues(AuthSettings authSettings, List<CollectionVariable> variableUsages)
        {
            if (authSettings.TryGetAuth2Config(OAuth2Config.AccessTokenUrl, out var accessTokenUrl) && accessTokenUrl is { })
            {
                accessTokenUrl = ReplaceVariablesWithValues(accessTokenUrl, variableUsages, CsharpPropertyType.Private)!;
                authSettings.SetAuth2ConfigValue(OAuth2Config.AccessTokenUrl, accessTokenUrl);
            }

            if (authSettings.TryGetAuth2Config(OAuth2Config.AuthUrl, out var authUrl) && authUrl is { })
            {
                authUrl = ReplaceVariablesWithValues(authUrl, variableUsages, CsharpPropertyType.Private)!;
                authSettings.SetAuth2ConfigValue(OAuth2Config.AuthUrl, authUrl);
            }

            if (authSettings.TryGetAuth2Config(OAuth2Config.RefreshTokenUrl, out var refreshTokenUrl) && refreshTokenUrl is { })
            {
                refreshTokenUrl = ReplaceVariablesWithValues(refreshTokenUrl, variableUsages, CsharpPropertyType.Private)!;
                authSettings.SetAuth2ConfigValue(OAuth2Config.RefreshTokenUrl, refreshTokenUrl);
            }

            if (authSettings.TryGetAuth2Config(OAuth2Config.RedirectUri, out var redirectUri) && redirectUri is { })
            {
                redirectUri = ReplaceVariablesWithValues(redirectUri, variableUsages, CsharpPropertyType.Private)!;
                authSettings.SetAuth2ConfigValue(OAuth2Config.RedirectUri, redirectUri);
            }
        }

        /// <summary>
        /// Sometimes there are variables usages within variables values. These can be used in urls which creates
        /// invalid urls so these must be replaced. https://{{authority}}.com will not be parsed by new Uri(str);
        /// </summary>
        /// <param name="variableUsages"></param>
        /// <param name="collectionVariables"></param>
        public static void ReplaceVariableUsagesInVariableUsages(List<CollectionVariable> collectionVariables)
        {
            foreach (var collectionVariable in collectionVariables)
            {
                collectionVariable.Value = ReplaceVariablesWithValues(collectionVariable.Value, collectionVariables, CsharpPropertyType.Private)!;
            }
        }

        public static string? ReplaceVariablesWithValues(string? input, List<CollectionVariable> collectionVariables, CsharpPropertyType type)
        {
            if (input == null) return null;
            var pattern = "{{(.*?)}}";

            var matches = Regex.Matches(input, pattern);

            foreach (Match match in matches)
            {
                var variable = match.Groups[1].Value;
                if (collectionVariables.FirstOrDefault(x => x.Key == variable) is { } variableUsage)
                {
                    var value = variableUsage.Value;
                    if (string.IsNullOrEmpty(variableUsage.Value))
                    {
                        value = Utils.NormalizeToCsharpPropertyName(variable, type);
                    }
                    input = input.Replace($"{{{{{variable}}}}}", value);
                }
                else
                {
                    var value = Utils.NormalizeToCsharpPropertyName(variable, type);
                    input = input.Replace($"{{{{{variable}}}}}", value);
                }
            }

            return input;
        }

        public static (string? StringWithCSharpInterpolation, List<(string Original, CsharpPropertyType CsharpPropertyType)> Variables) ExtractAndReplaceVariables(string? input, CsharpPropertyType csharpPropertyType)
        {
            var variables = new List<(string Original, CsharpPropertyType CsharpPropertyType)>();
            if (input == null) return (null, variables);
            var pattern = "{{(.*?)}}";

            var matches = Regex.Matches(input, pattern);

            foreach (Match match in matches)
            {
                var variable = match.Groups[1].Value; // Groups[1] corresponds to the content between {{ and }}
                var indexOfQuestionMark = input.IndexOf('?');
                var indexOfVariable = input.IndexOf(variable, StringComparison.Ordinal);
                if (indexOfVariable > indexOfQuestionMark && indexOfQuestionMark != -1)
                {
                    // This is a query parameter, so we don't return it. If we did, this would get added to the list
                    // of api client member variables and we don't want that
                    continue;
                }
                var csPropertyUsage = Utils.NormalizeToCsharpPropertyName(variable, csharpPropertyType);

                // Replace the variable with the C# interpolated usage
                input = input.Replace($"{{{{{variable}}}}}", $"{{{csPropertyUsage}}}");

                variables.Add(new(variable, csharpPropertyType));
            }

            return (input, variables);
        }
    }
}
