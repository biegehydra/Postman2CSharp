using System.Web;
using Microsoft.AspNetCore.Components;

namespace Postman2CSharp.UI.Extensions
{
    //https://chrissainty.com/working-with-query-strings-in-blazor/
    public static class NavigationManagerExtensions
    {
        public static bool TryGetQueryParameter(this NavigationManager navManager, string key, out string? value)
        {
            var uri = new Uri(navManager.Uri);
            value = HttpUtility.ParseQueryString(uri.Query).Get(key);
            return value == null;
        }
        public static bool TryGetIdFromPath(this NavigationManager navManager, string key, out string? value)
        {
            var uri = new Uri(navManager.Uri);

            return uri.TryGetIdFromPath(key, out value);
        }

        public static bool TryGetIdFromPath(this string? path, string key, out string? value)
        {
            if (path == null)
            {
                value = default;
                return false;
            }

            var uri = new Uri(path);
            return uri.TryGetIdFromPath(key, out value);
        }
        public static bool TryGetIdFromPath(this Uri uri, string key, out string? value)
        {
            for (var i = 0; i < uri.Segments.Length - 1; i++)
            {
                if (i + 1 == uri.Segments.Length)
                {
                    value = default;
                    return false;
                }
                if (uri.Segments[i].Trim('/') == key && Guid.TryParse(uri.Segments[i + 1], out var _))
                {
                    value = uri.Segments[i + 1];
                    return true;
                }
            }

            value = default;
            return false;
        }

        public static string GetAllQueryParameters(this NavigationManager navManager, string keyToRemove = "")
        {
            var uri = new Uri(navManager.Uri);
            var parsedQuery = HttpUtility.ParseQueryString(uri.Query);

#pragma warning disable CS8714, CS8621
            var queryParams = parsedQuery.AllKeys.Where(k => k != keyToRemove)
                .ToDictionary(k => k, k => parsedQuery[k]);
#pragma warning restore CS8714, CS8621
            var queryStr = string.Join("&", queryParams.Select(kvp => $"{HttpUtility.UrlEncode(kvp.Key)}={HttpUtility.UrlEncode(kvp.Value)}"));

            return queryStr;
        }
    }
}
