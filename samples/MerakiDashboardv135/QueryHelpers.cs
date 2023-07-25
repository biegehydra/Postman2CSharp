using System.Text;
using System.Text.Encodings.Web;
using System;
using System.Collections.Generic;

namespace MerakiDashboard
{
    public static class QueryHelpers
    {
        public static string AddQueryString(
            string uri,
            IEnumerable<KeyValuePair<string, string?>> queryString)
        {
            ArgumentNullException.ThrowIfNull(uri);
            ArgumentNullException.ThrowIfNull(queryString);
    
            var anchorIndex = uri.IndexOf('#');
            var uriToBeAppended = uri.AsSpan();
            var anchorText = ReadOnlySpan<char>.Empty;
            // If there is an anchor, then the query string must be inserted before its first occurrence.
            if (anchorIndex != -1)
            {
                anchorText = uriToBeAppended.Slice(anchorIndex);
                uriToBeAppended = uriToBeAppended.Slice(0, anchorIndex);
            }
    
            var queryIndex = uriToBeAppended.IndexOf('?');
            var hasQuery = queryIndex != -1;
    
            var sb = new StringBuilder();
            sb.Append(uriToBeAppended);
            foreach (var parameter in queryString)
            {
                if (parameter.Value == null)
                {
                    continue;
                }
    
                sb.Append(hasQuery ? '&' : '?');
                sb.Append(UrlEncoder.Default.Encode(parameter.Key));
                sb.Append('=');
                sb.Append(UrlEncoder.Default.Encode(parameter.Value));
                hasQuery = true;
            }
    
            sb.Append(anchorText);
            return sb.ToString();
        }
    }
}