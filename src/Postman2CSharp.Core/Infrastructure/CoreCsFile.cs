using System.Collections.Generic;

namespace Postman2CSharp.Core.Infrastructure
{
    public static class CoreCsFile
    {
        public static readonly Dictionary<string, string> Dict = new()
        {
            {nameof(QueryHelpers), QueryHelpers},
            {nameof(HelperExtensions), HelperExtensions},
            {nameof(OAuth2QueryParameters), OAuth2QueryParameters},
            {nameof(Interfaces), Interfaces},
            {nameof(HttpJsonExtensions), HttpJsonExtensions},
            {nameof(NewtonsoftHttpJsonExtensions), NewtonsoftHttpJsonExtensions}
        };

        // no pad
        public const string QueryHelpers = @"public static class QueryHelpers
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
}";

        // needs pad
        public const string HelperExtensions = @"public static class HelperExtensions
{
    public static Dictionary<string, string?> Unique(this Dictionary<string, string?> dict1,
        Dictionary<string, string?> dict2)
    {
        Dictionary<string, string?> result = new (dict1);
        foreach (var kvp in dict2)
        {
            result.TryAdd(kvp.Key, kvp.Value);
        }
        return result;
    }
}";

        // needs pad
        public const string OAuth2QueryParameters = @"public class OAuth2QueryParameters : IQueryParameters
    {
        public string? AccessToken { get; set; }
        public string? RefreshToken { get; set; }
        public string? ResponseType { get; set; }
        public string? RedirectUrl { get; set; }
        public string? ClientId { get; set; }
        public string? ClientSecret { get; set; }
        public string? Code { get; set; }
        public string? GrantType { get; set; }
        public string? Scope { get; set; }
        public string? State { get; set; }
        public Dictionary<string, string?> ToDictionary()
        {
            var dictionary = new Dictionary<string, string?>();
            AddIfNotNull(""access_token"", AccessToken);
            AddIfNotNull(""refresh_token"", RefreshToken);
            AddIfNotNull(""response_type"", ResponseType);
            AddIfNotNull(""redirect_url"", RedirectUrl);
            AddIfNotNull(""access_token"", AccessToken);
            AddIfNotNull(""grant_type"", GrantType);
            AddIfNotNull(""client_id"", ClientId);
            AddIfNotNull(""client_secret"", ClientSecret);
            AddIfNotNull(""code"", Code);
            AddIfNotNull(""scope"", Scope);
            AddIfNotNull(""state"", State);
            return dictionary;

            void AddIfNotNull(string key, string? value)
            {
                if (value != null)
                {
                    dictionary.Add(key, value);
                }
            }
        }
    }";

        // no pad
        public const string Interfaces = @"    public interface IQueryParameters
    {
        Dictionary<string, string?> ToDictionary();
    }

    public interface IFormData
    {
        FormUrlEncodedContent ToFormData();
    }

    public interface IMultipartFormData
    {
        MultipartFormDataContent ToMultipartFormData();
    }";

        // needs pad
        public const string HttpJsonExtensions = @"/// <summary>
/// Extension methods for working with JSON APIs.
/// </summary>
public static class HttpClientJsonExtensions
{
    private static readonly JsonSerializerOptions JsonSerializerOptions = new(JsonSerializerDefaults.Web);
    /// <summary>
    /// Sends a GET request to the specified URI, and parses the JSON response body
    /// to create an object of the generic type.
    /// </summary>
    /// <typeparam name=""T"">A type into which the response body can be JSON-deserialized.</typeparam>
    /// <param name=""httpClient"">The <see cref=""HttpClient""/>.</param>
    /// <param name=""requestUri"">The URI that the request will be sent to.</param>
    /// <returns>The response parsed as an object of the generic type.</returns>
    public static Task<T> GetJsonAsync<T>(this HttpClient httpClient, string requestUri, object content, Dictionary<string, string>? headers = null, CancellationToken cancellationToken = default)
        => httpClient.SendJsonAsync<T>(HttpMethod.Get, requestUri, content, headers, cancellationToken);


    /// <summary>
    /// Sends a POST request to the specified URI, including the specified <paramref name=""content""/>
    /// in JSON-encoded format, and parses the JSON response body to create an object of the generic type.
    /// </summary>
    /// <typeparam name=""T"">A type into which the response body can be JSON-deserialized.</typeparam>
    /// <param name=""httpClient"">The <see cref=""HttpClient""/>.</param>
    /// <param name=""requestUri"">The URI that the request will be sent to.</param>
    /// <param name=""content"">Content for the request body. This will be JSON-encoded and sent as a string.</param>
    /// <returns>The response parsed as an object of the generic type.</returns>
    public static Task<T> PatchJsonAsync<T>(this HttpClient httpClient, string requestUri, object content, Dictionary<string, string>? headers = null, CancellationToken cancellationToken = default)
        => httpClient.SendJsonAsync<T>(HttpMethod.Patch, requestUri, content, headers, cancellationToken);

    /// <summary>
    /// Sends a POST request to the specified URI, including the specified <paramref name=""content""/>
    /// in JSON-encoded format, and parses the JSON response body to create an object of the generic type.
    /// </summary>
    /// <typeparam name=""T"">A type into which the response body can be JSON-deserialized.</typeparam>
    /// <param name=""httpClient"">The <see cref=""HttpClient""/>.</param>
    /// <param name=""requestUri"">The URI that the request will be sent to.</param>
    /// <param name=""content"">Content for the request body. This will be JSON-encoded and sent as a string.</param>
    /// <returns>The response parsed as an object of the generic type.</returns>
    public static Task<T> DeleteJsonAsync<T>(this HttpClient httpClient, string requestUri, object content, Dictionary<string, string>? headers = null, CancellationToken cancellationToken = default)
        => httpClient.SendJsonAsync<T>(HttpMethod.Delete, requestUri, content, headers, cancellationToken);

    /// <summary>
    /// Sends a POST request to the specified URI, including the specified <paramref name=""content""/>
    /// in JSON-encoded format, and parses the JSON response body to create an object of the generic type.
    /// </summary>
    /// <typeparam name=""T"">A type into which the response body can be JSON-deserialized.</typeparam>
    /// <param name=""httpClient"">The <see cref=""HttpClient""/>.</param>
    /// <param name=""requestUri"">The URI that the request will be sent to.</param>
    /// <param name=""content"">Content for the request body. This will be JSON-encoded and sent as a string.</param>
    /// <returns>The response parsed as an object of the generic type.</returns>
    public static Task<T> PostJsonAsync<T>(this HttpClient httpClient, string requestUri, object content, Dictionary<string, string>? headers = null, CancellationToken cancellationToken = default)
        => httpClient.SendJsonAsync<T>(HttpMethod.Post, requestUri, content, headers, cancellationToken);

    /// <summary>
    /// Sends a PUT request to the specified URI, including the specified <paramref name=""content""/>
    /// in JSON-encoded format, and parses the JSON response body to create an object of the generic type.
    /// </summary>
    /// <typeparam name=""T"">A type into which the response body can be JSON-deserialized.</typeparam>
    /// <param name=""httpClient"">The <see cref=""HttpClient""/>.</param>
    /// <param name=""requestUri"">The URI that the request will be sent to.</param>
    /// <param name=""content"">Content for the request body. This will be JSON-encoded and sent as a string.</param>
    /// <returns>The response parsed as an object of the generic type.</returns>
    public static Task<T> PutJsonAsync<T>(this HttpClient httpClient, string requestUri, object content, Dictionary<string, string>? headers = null, CancellationToken cancellationToken = default)
        => httpClient.SendJsonAsync<T>(HttpMethod.Put, requestUri, content, headers, cancellationToken);

    /// <summary>
    /// Sends an HTTP request to the specified URI, including the specified <paramref name=""content""/>
    /// in JSON-encoded format, and parses the JSON response body to create an object of the generic type.
    /// </summary>
    /// <typeparam name=""T"">A type into which the response body can be JSON-deserialized.</typeparam>
    /// <param name=""httpClient"">The <see cref=""HttpClient""/>.</param>
    /// <param name=""method"">The HTTP method.</param>
    /// <param name=""requestUri"">The URI that the request will be sent to.</param>
    /// <param name=""content"">Content for the request body. This will be JSON-encoded and sent as a string.</param>
    /// <returns>The response parsed as an object of the generic type.</returns>
    public static async Task<T> SendJsonAsync<T>(this HttpClient httpClient, HttpMethod method, string requestUri, object content, Dictionary<string, string>? headers = null, CancellationToken cancellationToken = default)
    {
        var requestJson = JsonSerializer.Serialize(content, JsonSerializerOptions)!;
        var request = new HttpRequestMessage(method, requestUri)
        {
            Content = new StringContent(requestJson, Encoding.UTF8, ""application/json"")
        };
        AddHeadersToRequest(request, headers);
        var response = await httpClient.SendAsync(request, cancellationToken);
        response.EnsureSuccessStatusCode();
        if (typeof(T) == typeof(IgnoreResponse))
        {
            return default!;
        }
        else
        {
            return await response.ReadJsonAsync<T>();
        }
    }


    public static Task<HttpResponseMessage> GetAsJsonAsync(this HttpClient httpClient, string requestUri,
        object content, Dictionary<string, string>? headers = null, CancellationToken cancellationToken = default)
        => httpClient.SendAsJsonAsync(HttpMethod.Get, requestUri, content, headers, cancellationToken);

    public static Task<HttpResponseMessage> DeleteAsJsonAsync(this HttpClient httpClient, string requestUri,
        object content, Dictionary<string, string>? headers = null, CancellationToken cancellationToken = default)
        => httpClient.SendAsJsonAsync(HttpMethod.Delete, requestUri, content, headers, cancellationToken);

    public static Task<HttpResponseMessage> PostAsJsonAsync(this HttpClient httpClient, string requestUri,
        object content, Dictionary<string, string>? headers = null, CancellationToken cancellationToken = default)
        => httpClient.SendAsJsonAsync(HttpMethod.Post, requestUri, content, headers, cancellationToken);

    public static Task<HttpResponseMessage> PutAsJsonAsync(this HttpClient httpClient, string requestUri,
        object content, Dictionary<string, string>? headers = null, CancellationToken cancellationToken = default)
        => httpClient.SendAsJsonAsync(HttpMethod.Put, requestUri, content, headers, cancellationToken);

    public static Task<HttpResponseMessage> PatchAsJsonAsync(this HttpClient httpClient, string requestUri,
        object content, Dictionary<string, string>? headers = null, CancellationToken cancellationToken = default)
        => httpClient.SendAsJsonAsync(HttpMethod.Patch, requestUri, content, headers, cancellationToken);

    private static async Task<HttpResponseMessage> SendAsJsonAsync(this HttpClient httpClient, HttpMethod method, string requestUri, object content, Dictionary<string, string>? headers, CancellationToken cancellationToken)
    {
        var requestJson = JsonSerializer.Serialize(content, JsonSerializerOptions)!;
        var request = new HttpRequestMessage(method, requestUri)
        {
            Content = new StringContent(requestJson, Encoding.UTF8, ""application/json"")
        };
        AddHeadersToRequest(request, headers);
        var response = await httpClient.SendAsync(request, cancellationToken);
        response.EnsureSuccessStatusCode();
        return response;
    }


    public static Task<T> GetFromJsonAsync<T>(this HttpClient httpClient, string requestUri, HttpContent httpContent, Dictionary<string, string>? headers = null, CancellationToken cancellationToken = default)
    => httpClient.SendFromJsonAsync<T>(HttpMethod.Get, requestUri, httpContent, headers, cancellationToken);

    public static Task<T> DeleteFromJsonAsync<T>(this HttpClient httpClient, string requestUri, HttpContent httpContent, Dictionary<string, string>? headers = null, CancellationToken cancellationToken = default)
        => httpClient.SendFromJsonAsync<T>(HttpMethod.Delete, requestUri, httpContent, headers, cancellationToken);

    public static Task<T> PostFromJsonAsync<T>(this HttpClient httpClient, string requestUri, HttpContent httpContent, Dictionary<string, string>? headers = null, CancellationToken cancellationToken = default)
        => httpClient.SendFromJsonAsync<T>(HttpMethod.Post, requestUri, httpContent, headers, cancellationToken);

    public static Task<T> PutFromJsonAsync<T>(this HttpClient httpClient, string requestUri, HttpContent httpContent, Dictionary<string, string>? headers = null, CancellationToken cancellationToken = default)
        => httpClient.SendFromJsonAsync<T>(HttpMethod.Put, requestUri, httpContent, headers, cancellationToken);

    public static Task<T> PatchFromJsonAsync<T>(this HttpClient httpClient, string requestUri, HttpContent httpContent, Dictionary<string, string>? headers = null, CancellationToken cancellationToken = default)
        => httpClient.SendFromJsonAsync<T>(HttpMethod.Patch, requestUri, httpContent, headers, cancellationToken);

    private static async Task<T> SendFromJsonAsync<T>(this HttpClient httpClient, HttpMethod method, string requestUri, HttpContent content, Dictionary<string, string>? headers = null, CancellationToken cancellationToken = default)
    {
        var request = new HttpRequestMessage(method, requestUri) { Content = content };
        AddHeadersToRequest(request, headers);
        var response = await httpClient.SendAsync(request, cancellationToken);
        response.EnsureSuccessStatusCode();
        return await response.ReadJsonAsync<T>();
    }


    public static Task<T> GetFromJsonAsync<T>(this HttpClient httpClient, string requestUri, Dictionary<string, string>? headers = null, CancellationToken cancellationToken = default)
        => httpClient.SendFromJsonAsync<T>(HttpMethod.Get, requestUri, headers, cancellationToken);

    public static Task<T> DeleteFromJsonAsync<T>(this HttpClient httpClient, string requestUri, Dictionary<string, string>? headers = null, CancellationToken cancellationToken = default)
        => httpClient.SendFromJsonAsync<T>(HttpMethod.Delete, requestUri, headers, cancellationToken);

    public static Task<T> PostFromJsonAsync<T>(this HttpClient httpClient, string requestUri, Dictionary<string, string>? headers = null, CancellationToken cancellationToken = default)
        => httpClient.SendFromJsonAsync<T>(HttpMethod.Post, requestUri, headers, cancellationToken);

    public static Task<T> PutFromJsonAsync<T>(this HttpClient httpClient, string requestUri, Dictionary<string, string>? headers = null, CancellationToken cancellationToken = default)
        => httpClient.SendFromJsonAsync<T>(HttpMethod.Put, requestUri, headers, cancellationToken);

    public static Task<T> PatchFromJsonAsync<T>(this HttpClient httpClient, string requestUri, Dictionary<string, string>? headers = null, CancellationToken cancellationToken = default)
        => httpClient.SendFromJsonAsync<T>(HttpMethod.Patch, requestUri, headers, cancellationToken);

    private static async Task<T> SendFromJsonAsync<T>(this HttpClient httpClient, HttpMethod method, string requestUri, Dictionary<string, string>? headers = null, CancellationToken cancellationToken = default)
    {
        var request = new HttpRequestMessage(method, requestUri);
        AddHeadersToRequest(request, headers);
        var response = await httpClient.SendAsync(request, cancellationToken);
        response.EnsureSuccessStatusCode();
        return await response.ReadJsonAsync<T>();
    }


    public static Task<HttpResponseMessage> GetAsync(this HttpClient httpClient, string requestUri, HttpContent httpContent, Dictionary<string, string>? headers = null, CancellationToken cancellationToken = default)
        => httpClient.SendAsync(HttpMethod.Get, requestUri, httpContent, headers, cancellationToken);

    public static Task<HttpResponseMessage> PostAsync(this HttpClient httpClient, string requestUri, HttpContent httpContent, Dictionary<string, string>? headers = null, CancellationToken cancellationToken = default)
        => httpClient.SendAsync(HttpMethod.Post, requestUri, httpContent, headers, cancellationToken);

    public static Task<HttpResponseMessage> PutAsync(this HttpClient httpClient, string requestUri, HttpContent httpContent, Dictionary<string, string>? headers = null, CancellationToken cancellationToken = default)
        => httpClient.SendAsync(HttpMethod.Put, requestUri, httpContent, headers, cancellationToken);

    public static Task<HttpResponseMessage> PatchAsync(this HttpClient httpClient, string requestUri, HttpContent httpContent, Dictionary<string, string>? headers = null, CancellationToken cancellationToken = default)
        => httpClient.SendAsync(HttpMethod.Patch, requestUri, httpContent, headers, cancellationToken);

    public static Task<HttpResponseMessage> DeleteAsync(this HttpClient httpClient, string requestUri, HttpContent httpContent, Dictionary<string, string>? headers = null, CancellationToken cancellationToken = default)
        => httpClient.SendAsync(HttpMethod.Delete, requestUri, httpContent, headers, cancellationToken);

    private static async Task<HttpResponseMessage> SendAsync(this HttpClient httpClient, HttpMethod method, string requestUri, HttpContent httpContent, Dictionary<string, string>? headers, CancellationToken cancellationToken)
    {
        var request = new HttpRequestMessage(method, requestUri)
        {
            Content = httpContent
        };
        AddHeadersToRequest(request, headers);
        var response = await httpClient.SendAsync(request, cancellationToken);
        response.EnsureSuccessStatusCode();
        return response;
    }


    public static Task<HttpResponseMessage> GetAsync(this HttpClient httpClient, string requestUri, Dictionary<string, string>? headers = null, CancellationToken cancellationToken = default)
        => httpClient.SendAsync(HttpMethod.Get, requestUri, headers, cancellationToken);

    public static Task<HttpResponseMessage> PostAsync(this HttpClient httpClient, string requestUri, Dictionary<string, string>? headers = null, CancellationToken cancellationToken = default)
        => httpClient.SendAsync(HttpMethod.Post, requestUri, headers, cancellationToken);

    public static Task<HttpResponseMessage> PutAsync(this HttpClient httpClient, string requestUri, Dictionary<string, string>? headers = null, CancellationToken cancellationToken = default)
        => httpClient.SendAsync(HttpMethod.Put, requestUri, headers, cancellationToken);

    public static Task<HttpResponseMessage> PatchAsync(this HttpClient httpClient, string requestUri, Dictionary<string, string>? headers = null, CancellationToken cancellationToken = default)
        => httpClient.SendAsync(HttpMethod.Patch, requestUri, headers, cancellationToken);

    public static Task<HttpResponseMessage> DeleteAsync(this HttpClient httpClient, string requestUri, Dictionary<string, string>? headers = null, CancellationToken cancellationToken = default)
        => httpClient.SendAsync(HttpMethod.Delete, requestUri, headers, cancellationToken);

    private static async Task<HttpResponseMessage> SendAsync(this HttpClient httpClient, HttpMethod method, string requestUri, Dictionary<string, string>? headers, CancellationToken cancellationToken)
    {
        var request = new HttpRequestMessage(method, requestUri);
        AddHeadersToRequest(request, headers);
        var response = await httpClient.SendAsync(request, cancellationToken);

        response.EnsureSuccessStatusCode();
        return response;
    }

    public static async Task<T> ReadJsonAsync<T>(this HttpResponseMessage response)
    {
        var stringContent = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<T>(stringContent, JsonSerializerOptions)!;
    }

    private static void AddHeadersToRequest(HttpRequestMessage request, Dictionary<string, string>? headers)
    {
        if (headers != null)
        {
            foreach (var (key, value) in headers)
            {
                request.Headers.TryAddWithoutValidation(key, value);
            }
        }
    }

    class IgnoreResponse { }
}
public class EmptyRequest { }
public class EmptyResponse { }";

        public const string NewtonsoftHttpJsonExtensions = @"/// <summary>
/// Extension methods for working with JSON APIs.
/// </summary>
public static class NewtonsoftHttpClientJsonExtensions
{
    private static readonly JsonSerializerSettings JsonSerializerSettings = new JsonSerializerSettings();
    /// <summary>
    /// Sends a GET request to the specified URI, and parses the JSON response body
    /// to create an object of the generic type.
    /// </summary>
    /// <typeparam name=""T"">A type into which the response body can be JSON-deserialized.</typeparam>
    /// <param name=""httpClient"">The <see cref=""HttpClient""/>.</param>
    /// <param name=""requestUri"">The URI that the request will be sent to.</param>
    /// <returns>The response parsed as an object of the generic type.</returns>
    public static Task<T> GetNewtonsoftJsonAsync<T>(this HttpClient httpClient, string requestUri, object content, Dictionary<string, string>? headers = null, CancellationToken cancellationToken = default)
        => httpClient.SendNewtonsoftJsonAsync<T>(HttpMethod.Get, requestUri, content, headers, cancellationToken);


    /// <summary>
    /// Sends a POST request to the specified URI, including the specified <paramref name=""content""/>
    /// in JSON-encoded format, and parses the JSON response body to create an object of the generic type.
    /// </summary>
    /// <typeparam name=""T"">A type into which the response body can be JSON-deserialized.</typeparam>
    /// <param name=""httpClient"">The <see cref=""HttpClient""/>.</param>
    /// <param name=""requestUri"">The URI that the request will be sent to.</param>
    /// <param name=""content"">Content for the request body. This will be JSON-encoded and sent as a string.</param>
    /// <returns>The response parsed as an object of the generic type.</returns>
    public static Task<T> PatchNewtonsoftJsonAsync<T>(this HttpClient httpClient, string requestUri, object content, Dictionary<string, string>? headers = null, CancellationToken cancellationToken = default)
        => httpClient.SendNewtonsoftJsonAsync<T>(HttpMethod.Patch, requestUri, content, headers, cancellationToken);

    /// <summary>
    /// Sends a POST request to the specified URI, including the specified <paramref name=""content""/>
    /// in JSON-encoded format, and parses the JSON response body to create an object of the generic type.
    /// </summary>
    /// <typeparam name=""T"">A type into which the response body can be JSON-deserialized.</typeparam>
    /// <param name=""httpClient"">The <see cref=""HttpClient""/>.</param>
    /// <param name=""requestUri"">The URI that the request will be sent to.</param>
    /// <param name=""content"">Content for the request body. This will be JSON-encoded and sent as a string.</param>
    /// <returns>The response parsed as an object of the generic type.</returns>
    public static Task<T> DeleteNewtonsoftJsonAsync<T>(this HttpClient httpClient, string requestUri, object content, Dictionary<string, string>? headers = null, CancellationToken cancellationToken = default)
        => httpClient.SendNewtonsoftJsonAsync<T>(HttpMethod.Delete, requestUri, content, headers, cancellationToken);

    /// <summary>
    /// Sends a POST request to the specified URI, including the specified <paramref name=""content""/>
    /// in JSON-encoded format, and parses the JSON response body to create an object of the generic type.
    /// </summary>
    /// <typeparam name=""T"">A type into which the response body can be JSON-deserialized.</typeparam>
    /// <param name=""httpClient"">The <see cref=""HttpClient""/>.</param>
    /// <param name=""requestUri"">The URI that the request will be sent to.</param>
    /// <param name=""content"">Content for the request body. This will be JSON-encoded and sent as a string.</param>
    /// <returns>The response parsed as an object of the generic type.</returns>
    public static Task<T> PostNewtonsoftJsonAsync<T>(this HttpClient httpClient, string requestUri, object content, Dictionary<string, string>? headers = null, CancellationToken cancellationToken = default)
        => httpClient.SendNewtonsoftJsonAsync<T>(HttpMethod.Post, requestUri, content, headers, cancellationToken);

    /// <summary>
    /// Sends a PUT request to the specified URI, including the specified <paramref name=""content""/>
    /// in JSON-encoded format, and parses the JSON response body to create an object of the generic type.
    /// </summary>
    /// <typeparam name=""T"">A type into which the response body can be JSON-deserialized.</typeparam>
    /// <param name=""httpClient"">The <see cref=""HttpClient""/>.</param>
    /// <param name=""requestUri"">The URI that the request will be sent to.</param>
    /// <param name=""content"">Content for the request body. This will be JSON-encoded and sent as a string.</param>
    /// <returns>The response parsed as an object of the generic type.</returns>
    public static Task<T> PutJsonNewtonsoftAsync<T>(this HttpClient httpClient, string requestUri, object content, Dictionary<string, string>? headers = null, CancellationToken cancellationToken = default)
        => httpClient.SendNewtonsoftJsonAsync<T>(HttpMethod.Put, requestUri, content, headers, cancellationToken);

    /// <summary>
    /// Sends an HTTP request to the specified URI, including the specified <paramref name=""content""/>
    /// in JSON-encoded format, and parses the JSON response body to create an object of the generic type.
    /// </summary>
    /// <typeparam name=""T"">A type into which the response body can be JSON-deserialized.</typeparam>
    /// <param name=""httpClient"">The <see cref=""HttpClient""/>.</param>
    /// <param name=""method"">The HTTP method.</param>
    /// <param name=""requestUri"">The URI that the request will be sent to.</param>
    /// <param name=""content"">Content for the request body. This will be JSON-encoded and sent as a string.</param>
    /// <returns>The response parsed as an object of the generic type.</returns>
    public static async Task<T> SendNewtonsoftJsonAsync<T>(this HttpClient httpClient, HttpMethod method, string requestUri, object content, Dictionary<string, string>? headers, CancellationToken cancellationToken)
    {
        var requestJson = JsonConvert.SerializeObject(content, JsonSerializerSettings);
        var request = new HttpRequestMessage(method, requestUri)
        {
            Content = new StringContent(requestJson, Encoding.UTF8, ""application/json"")
        };
        AddHeadersToRequest(request, headers);
        var response = await httpClient.SendAsync(request, cancellationToken);

        response.EnsureSuccessStatusCode();

        if (typeof(T) == typeof(IgnoreResponse))
        {
            return default!;
        }
        else
        {
            return await response.ReadNewtonsoftJsonAsync<T>();
        }
    }


    public static Task<HttpResponseMessage> GetAsNewtonsoftJsonAsync(this HttpClient httpClient, string requestUri, 
        object content, Dictionary<string, string>? headers = null, CancellationToken cancellationToken = default)
        => httpClient.SendAsNewtonsoftJsonAsync(HttpMethod.Get, requestUri, content, headers, cancellationToken);

    public static Task<HttpResponseMessage> DeleteAsNewtonsoftJsonAsync(this HttpClient httpClient, string requestUri,
        object content, Dictionary<string, string>? headers = null, CancellationToken cancellationToken = default)
        => httpClient.SendAsNewtonsoftJsonAsync(HttpMethod.Delete, requestUri, content, headers, cancellationToken);

    public static Task<HttpResponseMessage> PostAsNewtonsoftJsonAsync(this HttpClient httpClient, string requestUri,
        object content, Dictionary<string, string>? headers = null, CancellationToken cancellationToken = default)
        => httpClient.SendAsNewtonsoftJsonAsync(HttpMethod.Post, requestUri, content, headers, cancellationToken);

    public static Task<HttpResponseMessage> PutAsNewtonsoftJsonAsync(this HttpClient httpClient, string requestUri,
        object content, Dictionary<string, string>? headers = null, CancellationToken cancellationToken = default)
        => httpClient.SendAsNewtonsoftJsonAsync(HttpMethod.Put, requestUri, content, headers, cancellationToken);

    public static Task<HttpResponseMessage> PatchAsNewtonsoftJsonAsync(this HttpClient httpClient, string requestUri,
        object content, Dictionary<string, string>? headers = null, CancellationToken cancellationToken = default)
        => httpClient.SendAsNewtonsoftJsonAsync(HttpMethod.Patch, requestUri, content, headers, cancellationToken);

    private static async Task<HttpResponseMessage> SendAsNewtonsoftJsonAsync(this HttpClient httpClient, HttpMethod method, string requestUri, object content, Dictionary<string, string>? headers, CancellationToken cancellationToken)
    {
        var requestJson = JsonConvert.SerializeObject(content, JsonSerializerSettings);
        var request = new HttpRequestMessage(method, requestUri)
        {
            Content = new StringContent(requestJson, Encoding.UTF8, ""application/json"")
        };
        AddHeadersToRequest(request, headers);
        var response = await httpClient.SendAsync(request, cancellationToken);

        response.EnsureSuccessStatusCode();
        return response;
    }


    public static Task<T> GetFromNewtonsoftJsonAsync<T>(this HttpClient httpClient, string requestUri, HttpContent httpContent, Dictionary<string, string>? headers = null, CancellationToken cancellationToken = default)
        => httpClient.SendFromNewtonsoftJsonAsync<T>(HttpMethod.Get, requestUri, httpContent, headers, cancellationToken);

    public static Task<T> DeleteFromNewtonsoftJsonAsync<T>(this HttpClient httpClient, string requestUri, HttpContent httpContent, Dictionary<string, string>? headers = null, CancellationToken cancellationToken = default)
        => httpClient.SendFromNewtonsoftJsonAsync<T>(HttpMethod.Delete, requestUri, httpContent, headers, cancellationToken);

    public static Task<T> PostFromNewtonsoftJsonAsync<T>(this HttpClient httpClient, string requestUri, HttpContent httpContent, Dictionary<string, string>? headers = null, CancellationToken cancellationToken = default)
        => httpClient.SendFromNewtonsoftJsonAsync<T>(HttpMethod.Post, requestUri, httpContent, headers, cancellationToken);

    public static Task<T> PutFromNewtonsoftJsonAsync<T>(this HttpClient httpClient, string requestUri, HttpContent httpContent, Dictionary<string, string>? headers = null, CancellationToken cancellationToken = default)
        => httpClient.SendFromNewtonsoftJsonAsync<T>(HttpMethod.Put, requestUri, httpContent, headers, cancellationToken);

    public static Task<T> PatchFromNewtonsoftJsonAsync<T>(this HttpClient httpClient, string requestUri, HttpContent httpContent, Dictionary<string, string>? headers = null, CancellationToken cancellationToken = default)
        => httpClient.SendFromNewtonsoftJsonAsync<T>(HttpMethod.Patch, requestUri, httpContent, headers, cancellationToken);

    private static async Task<T> SendFromNewtonsoftJsonAsync<T>(this HttpClient httpClient, HttpMethod method, string requestUri, HttpContent content, Dictionary<string, string>? headers, CancellationToken cancellationToken)
    {
        var request = new HttpRequestMessage(method, requestUri)
        {
            Content = content
        };
        AddHeadersToRequest(request, headers);
        var response = await httpClient.SendAsync(request, cancellationToken);

        response.EnsureSuccessStatusCode();
        return await response.ReadNewtonsoftJsonAsync<T>();
    }


    public static Task<T> GetFromNewtonsoftJsonAsync<T>(this HttpClient httpClient, string requestUri, Dictionary<string, string>? headers = null, CancellationToken cancellationToken = default)
        => httpClient.SendFromNewtonsoftJsonAsync<T>(HttpMethod.Get, requestUri, headers, cancellationToken);

    public static Task<T> DeleteFromNewtonsoftJsonAsync<T>(this HttpClient httpClient, string requestUri, Dictionary<string, string>? headers = null, CancellationToken cancellationToken = default)
        => httpClient.SendFromNewtonsoftJsonAsync<T>(HttpMethod.Delete, requestUri, headers, cancellationToken);

    public static Task<T> PostFromNewtonsoftJsonAsync<T>(this HttpClient httpClient, string requestUri, Dictionary<string, string>? headers = null, CancellationToken cancellationToken = default)
        => httpClient.SendFromNewtonsoftJsonAsync<T>(HttpMethod.Post, requestUri, headers, cancellationToken);

    public static Task<T> PutFromNewtonsoftJsonAsync<T>(this HttpClient httpClient, string requestUri, Dictionary<string, string>? headers = null, CancellationToken cancellationToken = default)
        => httpClient.SendFromNewtonsoftJsonAsync<T>(HttpMethod.Put, requestUri, headers, cancellationToken);

    public static Task<T> PatchFromNewtonsoftJsonAsync<T>(this HttpClient httpClient, string requestUri, Dictionary<string, string>? headers = null, CancellationToken cancellationToken = default)
        => httpClient.SendFromNewtonsoftJsonAsync<T>(HttpMethod.Patch, requestUri, headers, cancellationToken);

    private static async Task<T> SendFromNewtonsoftJsonAsync<T>(this HttpClient httpClient, HttpMethod method, string requestUri, Dictionary<string, string>? headers, CancellationToken cancellationToken)
    {
        var request = new HttpRequestMessage(method, requestUri);
        AddHeadersToRequest(request, headers);
        var response = await httpClient.SendAsync(request, cancellationToken);
        response.EnsureSuccessStatusCode();
        return await response.ReadNewtonsoftJsonAsync<T>();
    }


    public static Task<HttpResponseMessage> GetAsync(this HttpClient httpClient, string requestUri, HttpContent httpContent, Dictionary<string, string>? headers = null, CancellationToken cancellationToken = default)
        => httpClient.SendAsync(HttpMethod.Get, requestUri, httpContent, headers, cancellationToken);

    public static Task<HttpResponseMessage> PostAsync(this HttpClient httpClient, string requestUri, HttpContent httpContent, Dictionary<string, string>? headers = null, CancellationToken cancellationToken = default)
        => httpClient.SendAsync(HttpMethod.Post, requestUri, httpContent, headers, cancellationToken);

    public static Task<HttpResponseMessage> PutAsync(this HttpClient httpClient, string requestUri, HttpContent httpContent, Dictionary<string, string>? headers = null, CancellationToken cancellationToken = default)
        => httpClient.SendAsync(HttpMethod.Put, requestUri, httpContent, headers, cancellationToken);

    public static Task<HttpResponseMessage> PatchAsync(this HttpClient httpClient, string requestUri, HttpContent httpContent, Dictionary<string, string>? headers = null, CancellationToken cancellationToken = default)
        => httpClient.SendAsync(HttpMethod.Patch, requestUri, httpContent, headers, cancellationToken);

    public static Task<HttpResponseMessage> DeleteAsync(this HttpClient httpClient, string requestUri, HttpContent httpContent, Dictionary<string, string>? headers = null, CancellationToken cancellationToken = default)
        => httpClient.SendAsync(HttpMethod.Delete, requestUri, httpContent, headers, cancellationToken);

    private static async Task<HttpResponseMessage> SendAsync(this HttpClient httpClient, HttpMethod method, string requestUri, HttpContent httpContent, Dictionary<string, string>? headers, CancellationToken cancellationToken)
    {
        var request = new HttpRequestMessage(method, requestUri)
        {
            Content = httpContent
        };
        AddHeadersToRequest(request, headers);
        var response = await httpClient.SendAsync(request, cancellationToken);
        response.EnsureSuccessStatusCode();
        return response;
    }


    public static Task<HttpResponseMessage> GetAsync(this HttpClient httpClient, string requestUri, Dictionary<string, string>? headers = null, CancellationToken cancellationToken = default)
        => httpClient.SendAsync(HttpMethod.Get, requestUri, headers, cancellationToken);

    public static Task<HttpResponseMessage> PostAsync(this HttpClient httpClient, string requestUri, Dictionary<string, string>? headers = null, CancellationToken cancellationToken = default)
        => httpClient.SendAsync(HttpMethod.Post, requestUri, headers, cancellationToken);

    public static Task<HttpResponseMessage> PutAsync(this HttpClient httpClient, string requestUri, Dictionary<string, string>? headers = null, CancellationToken cancellationToken = default)
        => httpClient.SendAsync(HttpMethod.Put, requestUri, headers, cancellationToken);

    public static Task<HttpResponseMessage> PatchAsync(this HttpClient httpClient, string requestUri, Dictionary<string, string>? headers = null, CancellationToken cancellationToken = default)
        => httpClient.SendAsync(HttpMethod.Patch, requestUri, headers, cancellationToken);

    public static Task<HttpResponseMessage> DeleteAsync(this HttpClient httpClient, string requestUri, Dictionary<string, string>? headers = null, CancellationToken cancellationToken = default)
        => httpClient.SendAsync(HttpMethod.Delete, requestUri, headers, cancellationToken);

    private static async Task<HttpResponseMessage> SendAsync(this HttpClient httpClient, HttpMethod method, string requestUri, Dictionary<string, string>? headers, CancellationToken cancellationToken)
    {
        var request = new HttpRequestMessage(method, requestUri);
        AddHeadersToRequest(request, headers);
        var response = await httpClient.SendAsync(request, cancellationToken);

        response.EnsureSuccessStatusCode();
        return response;
    }

    public static async Task<T> ReadNewtonsoftJsonAsync<T>(this HttpResponseMessage response)
    {
        var stringContent = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<T>(stringContent, JsonSerializerSettings);
    }

    private static void AddHeadersToRequest(HttpRequestMessage request, Dictionary<string, string>? headers)
    {
        if (headers != null)
        {
            foreach (var (key, value) in headers)
            {
                request.Headers.TryAddWithoutValidation(key, value);
            }
        }
    }

    class IgnoreResponse { }
}
public class EmptyRequest { }
public class EmptyResponse { }";

    }
}
