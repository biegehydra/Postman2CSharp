﻿using Postman2CSharp.Core.Models.PostmanCollection.Http;
using Postman2CSharp.Core.Models.PostmanCollection;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using System;
using System.Text;
using Postman2CSharp.Core.Models.PostmanCollection.Authorization;
using System.Text.RegularExpressions;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Postman2CSharp.Core.Infrastructure;

namespace Postman2CSharp.Core.Utilities
{
    public static partial class RootItemHelpers
    {
        /// <summary>
        /// Normalizes the all the request items names in the root
        /// </summary>
        /// <param name="rootItem"></param>
        public static void NormalizeRequestItemNames(CollectionItem rootItem)
        {
            foreach (var requestItem in rootItem.RequestItems() ?? new List<CollectionItem>())
            {
                requestItem.Name = requestItem.NormalizedName();
            }
        }

        /// <summary>
        /// Finds headers present in all of the request items
        /// </summary>
        /// <param name="rootItem"></param>
        /// <returns></returns>
        public static List<Header> GetCommonHeaders(this CollectionItem rootItem)
        {
            var requestItemsCount = rootItem.RequestItems()!.Count();
            var firstHeaders = rootItem.RequestItems()!.FirstOrDefault()?.Request?.Header;
            if (firstHeaders == null)
                return new List<Header>();
            var commonHeaders = firstHeaders
                .Where(header =>
                {
                    var percent = (decimal)rootItem.RequestItems()!.Count(r =>
                                      r.Request?.Header.Any(h => h.Key == header.Key && h.Value == header.Value) == true) /
                                  requestItemsCount;

                    return percent == 1m || (percent > 0.90m && rootItem.RequestItems()!.All(r => r.Request?.Header.Any(h => h.Key == header.Key) == true));
                })
                .ToList();
            return commonHeaders;
        }

        /// <summary>
        /// This groups the requests by authority (localhost:62099) so that we can generate a client for each authority
        /// </summary>
        /// <param name="rootItem"></param>
        /// <returns></returns>
        /// <exception cref="UnreachableException"></exception>
        public static Dictionary<string, List<CollectionItem>> GroupRequestsByAuthority(CollectionItem rootItem)
        {
            var requestItems = rootItem.Item?.Where(x => x.IsRequestItem).ToList() ?? throw new UnreachableException();
            var groups = requestItems
                .GroupBy(
                    requestItem =>
                    {
                        var uri = new Uri(requestItem.Request!.Url.Raw.ReplaceBrackets());
                        return uri.FullAuthority();
                    },
                    requestItem => requestItem)
                .ToDictionary(g => g.Key, g => g.ToList());
            return groups;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="rootItem"></param>
        /// <returns></returns>
        /// <exception cref="UnreachableException"></exception>
        public static bool HasCommonAuthority(this CollectionItem rootItem)
        {
            List<string> uris = new();
            var requestItems = rootItem.RequestItems()?.ToList() ?? throw new UnreachableException("Request items should not be null at this point.");
            foreach (var requestItem in requestItems)
            {
                if (requestItem.Request?.Url != null)
                {
                    uris.Add(requestItem.Request.Url.Raw.ReplaceBrackets());
                }
            }

            var uriData = uris.Select(u => new Uri(u.ReplaceBrackets())).ToList();

            var commonUserInfo = uriData[0].UserInfo;
            var commonAuthority = uriData[0].Authority;
            return uriData.All(uri => commonUserInfo == uri.UserInfo && uri.Authority == commonAuthority);
        }

        public static string? FindLeastPossibleUri(this CollectionItem rootItem, List<VariableUsage> variableUsages)
        {
            List<string?> uris = new();
            var requestItems = rootItem.RequestItems()?.ToList() ?? throw new UnreachableException();
            foreach (var requestItem in requestItems)
            {
                if (requestItem.Request?.Url != null)
                {
                    uris.Add(requestItem.Request.Url.Raw);
                }
            }
            return FindLeastPossibleUri(uris, variableUsages);
        }

        private const string BracketsPattern = @"\{([^}]*)\}";
        private static readonly Regex BracketsRegex = new Regex(BracketsPattern, RegexOptions.Compiled);

        /// <summary>
        /// Finds the least possible url from a list of urls with a common authority
        /// The return value will never include a path variable. This is because a path variable
        /// ending up in a base url is bad because generally those need to be known on a per request basis.
        /// </summary>
        /// <param name="uris"></param>
        /// <returns></returns>
        private static string? FindLeastPossibleUri(List<string?> uris, List<VariableUsage> variableUsages)
        {
            if (uris.Count == 0)
            {
                return null;
            }

            var uriData = uris.Where(x => x != null).Select(u => new Uri(u!.ReplaceBrackets())).ToList();
            var commonUserInfo = uriData[0].UserInfo;
            var commonAuthority = uriData[0].Authority;
            if (uriData.Any(uri => commonUserInfo != uri.UserInfo || uri.Authority != commonAuthority))
            {
                return null;
            }

            var uriSegments = uriData.Select(u => u.AbsolutePath.Split('/', StringSplitOptions.RemoveEmptyEntries)).ToList();
            var leastPossibleUriSegments = new List<string>();

            for (int i = 0; i < uriSegments[0].Length; i++)
            {
                var currentSegment = uriSegments[0][i];
                var withBrackets = currentSegment.AddBackBrackets();
                if (uriSegments.All(us => us.Length > i && us[i] == currentSegment) && !currentSegment.StartsWith(":")) // Don't want the least possible uri to include path variables.
                {
                    if (withBrackets.Count(x => x == '{') == 1 && withBrackets.Count(x => x == '}') == 1)
                    {
                        var match = BracketsRegex.Match(withBrackets);
                        if (match is { Success: true, Groups.Count: > 1 }) // Checking if there is a match and a capturing group
                        {
                            var capturedValue = match.Groups[1].Value;
                            if (variableUsages.Any(x =>
                                    x.ApiClientUsage == CsharpPropertyType.Private &&
                                    Utils.NormalizeToCsharpPropertyName(x.Original, CsharpPropertyType.Private) ==
                                    capturedValue))
                            {
                                leastPossibleUriSegments.Add(withBrackets);
                            }
                            else
                            {
                                break;
                            }
                        }
                        else
                        {
                            break;
                        }
                    }
                    else
                    {
                        leastPossibleUriSegments.Add(withBrackets);
                    }
                }
                else
                {
                    break;
                }
            }
            return $"https://{uriData[0].FullAuthority()}/{string.Join('/', leastPossibleUriSegments)}";
        }

        /// <summary>
        /// Adds http:// or https:// to urls that are missing it
        /// </summary>
        /// <param name="rootItem"></param>
        /// <exception cref="NotImplementedException"></exception>
        public static void FixUrlsMissingScheme(CollectionItem rootItem, List<VariableUsage>? variableUsages)
        {
            var requestItems = rootItem.RequestItems();
            if (requestItems == null) return;
            foreach (var requestItem in requestItems)
            {
                var url = requestItem.Request!.Url;
                url.Raw = url.Raw.Trim();
                if (url.Raw.StartsWith('/'))
                {
                    url.Raw = "https://{_unknownBaseUrl}" + url.Raw;
                }
                if (url.Protocol == null && !(url.Raw.StartsWith("https://") || url.Raw.StartsWith("http://")))
                {
                    var firstVariable = CaptureFirstVariable(url.Raw);
                    if (!string.IsNullOrWhiteSpace(firstVariable) &&
                        variableUsages?.FirstOrDefault(x => x.CSPropertyUsage(CsharpPropertyType.Private) == firstVariable) is { } variable)
                    {
                        variable.Value = variable.Value?
                            .Replace("https://", "", StringComparison.CurrentCultureIgnoreCase)
                            .Replace("http://", "", StringComparison.CurrentCultureIgnoreCase);
                    }
                    url.Raw = $"https://{url.Raw}";
                }
                url.Raw = url.Raw.Trim();
            }

            string? CaptureFirstVariable(string? str)
            {
                if (string.IsNullOrWhiteSpace(str)) return null;
                if (!str.StartsWith("{"))
                {
                    return null;
                }
                StringBuilder sb = new ();
                int i = 1;
                while (i < str.Length && str[i] != '}')
                {
                    sb.Append(str[i]);
                    i++;
                }
                if (str[i] != '}' || sb.Length == 0)
                {
                    return null;
                }
                return sb.ToString();
            }
        }

        /// <summary>
        /// Uses the settings to optionally remove disabled headers or query params
        /// </summary>
        /// <param name="rootItem"></param>
        /// <param name="options"></param>
        public static void RemoveUnusedParameters(List<CollectionItem> rootItems, ApiClientOptions options)
        {

            foreach (var rootItem in rootItems)
            {
                if (rootItem.RequestItems() == null) return;
                foreach (var requestItem in rootItem.RequestItems()!)
                {
                    if (options.RemoveDisabled.Contains(RemoveDisabled.Headers))
                    {
                        requestItem.Request!.Header.RemoveAll(x => x.Disabled == true);
                    }

                    if (options.RemoveDisabled.Contains(RemoveDisabled.QueryParameters))
                    {
                        requestItem.Request!.Url.Query?.RemoveAll(x => x.Disabled == true);
                    }

                    if (options.RemoveDisabled.Contains(RemoveDisabled.FormData))
                    {
                        requestItem.Request!.Body?.Formdata?.RemoveAll(x => x.Disabled);
                    }

                    if (options.RemoveDisabled.Contains(RemoveDisabled.FormUrlEncoded))
                    {
                        requestItem.Request!.Body?.Urlencoded?.RemoveAll(x => x.Disabled);
                    }
                }
            }
        }

        /// <summary>
        /// Removes all urls in request items where a uri cannot be created or the scheme is not http or https
        /// </summary>
        /// <param name="rootItem"></param>
        public static void RemoveRequestsWithInvalidUrls(CollectionItem rootItem)
        {
            rootItem.Item?.RemoveAll(x => x.IsRequestItem && (!Uri.TryCreate(x.Request?.Url.Raw.ReplaceBrackets(), UriKind.Absolute, out var uri) || !(uri.Scheme == Uri.UriSchemeHttp || uri.Scheme == Uri.UriSchemeHttps)));
        }

        public static void ReplacePathVariablesWithInterpolatedVariableUsages(CollectionItem rootItem)
        {
            foreach (var requestItem in rootItem.RequestItems() ?? new List<CollectionItem>())
            {
                var url = requestItem.Request!.Url;
                foreach (var pathVariableUsage in url.Path?.Where(x => x.IsVariable()) ?? new List<Path>())
                {
                    url.Raw = url.Raw.Replace(pathVariableUsage, $"{{{pathVariableUsage.CsPropertyName(CsharpPropertyType.Local)}}}");
                }
            }
        }

        public static void ReplaceAllVariableUsagesInAllAuths(CollectionItem rootItem, AuthSettings? auth, List<CollectionVariable> variables)
        {
            if (auth != null)
            {
                VariableExtractor.ReplaceAllVariableUsageInAuthWithValues(auth, variables);
                foreach (var requestItem in rootItem.RequestItems() ?? new List<CollectionItem>())
                {
                    if (requestItem.Request!.Auth != null)
                    {
                        VariableExtractor.ReplaceAllVariableUsageInAuthWithValues(requestItem.Request.Auth,
                            variables);
                    }
                }
            }
        }

        public static List<CollectionItem> ResortByAuthority(List<CollectionItem> rootItems, string collectionName)
        {
            var allRequests = rootItems.SelectMany(x => x.RequestItems() ?? new List<CollectionItem>()).ToList();
            var dictionary = new Dictionary<string, List<CollectionItem>>();
            foreach (var request in allRequests)
            {
                var uri = new Uri(request.Request!.Url.Raw.ReplaceBrackets());
                var authority = uri.FullAuthority();
                if (!dictionary.TryGetValue(authority, out _))
                {
                    dictionary[authority] = new ();
                }
                dictionary[authority].Add(request);
            }
            var newRoots = new List<CollectionItem>();
            foreach (var (key, value) in dictionary)
            {
                var name = dictionary.Count == 1 ? Utils.NormalizeToCsharpPropertyName(collectionName) : Utils.NormalizeToCsharpPropertyName(key);
                var newRootItem = new CollectionItem()
                {
                    Name = name,
                    Item = value
                };
                newRoots.Add(newRootItem);
            }
            return newRoots;
        }

        // lang=regex
        private const string InterpolatedVariableRegex = @"(?<!\{){_[a-zA-Z0-9]*}(?!\})";
        public static void MakePathCollectionVariablesFunctionParameters(CollectionItem rootItem, List<VariableUsage> variableUsages)
        {
            foreach (var url in rootItem.RequestItems()?.Select(x => x.Request!.Url) ?? new List<Url>())
            {
                if (url.Path == null) continue;
                foreach (var path in url.Path)
                {
                    foreach (Match match in VariableUsageRegex().Matches(path))
                    {
                        var matched = match.Value;
                        if (variableUsages.FirstOrDefault(x => matched.Contains(x.CSPropertyUsage(CsharpPropertyType.Private))) is { } variableUsage)
                        {
                            variableUsages.Remove(variableUsage);
                        }
                        var asPathVariable = ":" + matched.Replace("{_", "").Replace("}", "");
                        path.Value = path.Value!.Replace(matched, asPathVariable);
                        url.Raw = url.Raw.Replace(matched, asPathVariable);
                    }
                }
            }
        }

        [GeneratedRegex("(?<!\\{){_[a-zA-Z0-9]*}(?!\\})")]
        private static partial Regex VariableUsageRegex();
    }
}
