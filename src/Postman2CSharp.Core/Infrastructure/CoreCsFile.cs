using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net;
using Xamasoft.JsonClassGenerator.Models;


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

        public static string GraphQLRequest(JsonLibrary jsonLibrary)
        {
            return jsonLibrary switch
            {
                JsonLibrary.NewtonsoftJson => GraphQlRequestNewtonsoft,
                JsonLibrary.SystemTextJson => GraphQlRequestSystemTextJson,
                _ => throw new ArgumentOutOfRangeException(nameof(jsonLibrary), jsonLibrary, null)
            };
        }

        private const string GraphQlRequestNewtonsoft = @"public class GraphQLRequest
{
    [JsonProperty(""query"")]
    public string Query { get; set; }

    // Using 'object' to allow for flexible input, either a structured object or a JSON string.
    [JsonProperty(""variables"", NullValueHandling = NullValueHandling.Ignore)]
    public object Variables { get; set; }

    [JsonProperty(""operationName"", NullValueHandling = NullValueHandling.Ignore)]
    public string OperationName { get; set; }

    public GraphQLRequest(string query, object variables = null, string operationName = null)
    {
        Query = query;
        Variables = variables;
        OperationName = operationName;
    }
}";
        private const string GraphQlRequestSystemTextJson = @"public class GraphQLRequest
{
    [JsonPropertyName(""query"")]
    public string Query { get; set; }

    // Using 'object' to allow for various input types, including structured objects or JSON strings.
    [JsonPropertyName(""variables"")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public object Variables { get; set; }

    [JsonPropertyName(""operationName"")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string OperationName { get; set; }

    public GraphQLRequest(string query, object variables = null, string operationName = null)
    {
        Query = query;
        Variables = variables;
        OperationName = operationName;
    }
}";

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
    public static Task<T> GetJsonAsync<T>(this HttpClient httpClient, string requestUri, object content, Dictionary<string, string>? headers = null, CancellationToken cancellationToken = default, JsonSerializerOptions? jsonSerializerOptions = null)
        => httpClient.SendJsonAsync<T>(HttpMethod.Get, requestUri, content, headers, cancellationToken, jsonSerializerOptions);


    /// <summary>
    /// Sends a POST request to the specified URI, including the specified <paramref name=""content""/>
    /// in JSON-encoded format, and parses the JSON response body to create an object of the generic type.
    /// </summary>
    /// <typeparam name=""T"">A type into which the response body can be JSON-deserialized.</typeparam>
    /// <param name=""httpClient"">The <see cref=""HttpClient""/>.</param>
    /// <param name=""requestUri"">The URI that the request will be sent to.</param>
    /// <param name=""content"">Content for the request body. This will be JSON-encoded and sent as a string.</param>
    /// <returns>The response parsed as an object of the generic type.</returns>
    public static Task<T> PatchJsonAsync<T>(this HttpClient httpClient, string requestUri, object content, Dictionary<string, string>? headers = null, CancellationToken cancellationToken = default, JsonSerializerOptions? jsonSerializerOptions = null)
        => httpClient.SendJsonAsync<T>(HttpMethod.Patch, requestUri, content, headers, cancellationToken, jsonSerializerOptions);

    /// <summary>
    /// Sends a POST request to the specified URI, including the specified <paramref name=""content""/>
    /// in JSON-encoded format, and parses the JSON response body to create an object of the generic type.
    /// </summary>
    /// <typeparam name=""T"">A type into which the response body can be JSON-deserialized.</typeparam>
    /// <param name=""httpClient"">The <see cref=""HttpClient""/>.</param>
    /// <param name=""requestUri"">The URI that the request will be sent to.</param>
    /// <param name=""content"">Content for the request body. This will be JSON-encoded and sent as a string.</param>
    /// <returns>The response parsed as an object of the generic type.</returns>
    public static Task<T> DeleteJsonAsync<T>(this HttpClient httpClient, string requestUri, object content, Dictionary<string, string>? headers = null, CancellationToken cancellationToken = default, JsonSerializerOptions? jsonSerializerOptions = null)
        => httpClient.SendJsonAsync<T>(HttpMethod.Delete, requestUri, content, headers, cancellationToken, jsonSerializerOptions);

    /// <summary>
    /// Sends a POST request to the specified URI, including the specified <paramref name=""content""/>
    /// in JSON-encoded format, and parses the JSON response body to create an object of the generic type.
    /// </summary>
    /// <typeparam name=""T"">A type into which the response body can be JSON-deserialized.</typeparam>
    /// <param name=""httpClient"">The <see cref=""HttpClient""/>.</param>
    /// <param name=""requestUri"">The URI that the request will be sent to.</param>
    /// <param name=""content"">Content for the request body. This will be JSON-encoded and sent as a string.</param>
    /// <returns>The response parsed as an object of the generic type.</returns>
    public static Task<T> PostJsonAsync<T>(this HttpClient httpClient, string requestUri, object content, Dictionary<string, string>? headers = null, CancellationToken cancellationToken = default, JsonSerializerOptions? jsonSerializerOptions = null)
        => httpClient.SendJsonAsync<T>(HttpMethod.Post, requestUri, content, headers, cancellationToken, jsonSerializerOptions);

    /// <summary>
    /// Sends a PUT request to the specified URI, including the specified <paramref name=""content""/>
    /// in JSON-encoded format, and parses the JSON response body to create an object of the generic type.
    /// </summary>
    /// <typeparam name=""T"">A type into which the response body can be JSON-deserialized.</typeparam>
    /// <param name=""httpClient"">The <see cref=""HttpClient""/>.</param>
    /// <param name=""requestUri"">The URI that the request will be sent to.</param>
    /// <param name=""content"">Content for the request body. This will be JSON-encoded and sent as a string.</param>
    /// <returns>The response parsed as an object of the generic type.</returns>
    public static Task<T> PutJsonAsync<T>(this HttpClient httpClient, string requestUri, object content, Dictionary<string, string>? headers = null, CancellationToken cancellationToken = default, JsonSerializerOptions? jsonSerializerOptions = null)
        => httpClient.SendJsonAsync<T>(HttpMethod.Put, requestUri, content, headers, cancellationToken, jsonSerializerOptions);

    public static Task<T> HeadJsonAsync<T>(this HttpClient httpClient, string requestUri, object content, Dictionary<string, string>? headers = null, CancellationToken cancellationToken = default, JsonSerializerOptions? jsonSerializerOptions = null)
        => httpClient.SendJsonAsync<T>(HttpMethod.Head, requestUri, content, headers, cancellationToken, jsonSerializerOptions);

    public static Task<T> OptionsJsonAsync<T>(this HttpClient httpClient, string requestUri, object content, Dictionary<string, string>? headers = null, CancellationToken cancellationToken = default, JsonSerializerOptions? jsonSerializerOptions = null)
        => httpClient.SendJsonAsync<T>(HttpMethod.Options, requestUri, content, headers, cancellationToken, jsonSerializerOptions);

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
    private static async Task<T> SendJsonAsync<T>(this HttpClient httpClient, HttpMethod method, string requestUri, object content, Dictionary<string, string>? headers, CancellationToken cancellationToken, JsonSerializerOptions? jsonSerializerOptions)
    {
        var requestJson = JsonSerializer.Serialize(content, jsonSerializerOptions ?? JsonSerializerOptions)!;
        var request = new HttpRequestMessage(method, requestUri)
        {
            Content = new StringContent(requestJson, Encoding.UTF8, ""application/json"")
        };
        AddHeadersToRequest(request, headers);
        var response = await httpClient.SendAsync(request, cancellationToken);
        if (typeof(T) == typeof(IgnoreResponse))
        {
            return default!;
        }
        else
        {
            return await response.ReadJsonAsync<T>(jsonSerializerOptions, cancellationToken: cancellationToken);
        }
    }


    public static Task<HttpResponseMessage> GetAsJsonAsync(this HttpClient httpClient, string requestUri,
        object content, Dictionary<string, string>? headers = null, CancellationToken cancellationToken = default, JsonSerializerOptions? jsonSerializerOptions = null)
        => httpClient.SendAsJsonAsync(HttpMethod.Get, requestUri, content, headers, cancellationToken, jsonSerializerOptions);

    public static Task<HttpResponseMessage> DeleteAsJsonAsync(this HttpClient httpClient, string requestUri,
        object content, Dictionary<string, string>? headers = null, CancellationToken cancellationToken = default, JsonSerializerOptions? jsonSerializerOptions = null)
        => httpClient.SendAsJsonAsync(HttpMethod.Delete, requestUri, content, headers, cancellationToken, jsonSerializerOptions);

    public static Task<HttpResponseMessage> PostAsJsonAsync(this HttpClient httpClient, string requestUri,
        object content, Dictionary<string, string>? headers = null, CancellationToken cancellationToken = default, JsonSerializerOptions? jsonSerializerOptions = null)
        => httpClient.SendAsJsonAsync(HttpMethod.Post, requestUri, content, headers, cancellationToken, jsonSerializerOptions);

    public static Task<HttpResponseMessage> PutAsJsonAsync(this HttpClient httpClient, string requestUri,
        object content, Dictionary<string, string>? headers = null, CancellationToken cancellationToken = default, JsonSerializerOptions? jsonSerializerOptions = null)
        => httpClient.SendAsJsonAsync(HttpMethod.Put, requestUri, content, headers, cancellationToken, jsonSerializerOptions);

    public static Task<HttpResponseMessage> PatchAsJsonAsync(this HttpClient httpClient, string requestUri,
        object content, Dictionary<string, string>? headers = null, CancellationToken cancellationToken = default, JsonSerializerOptions? jsonSerializerOptions = null)
        => httpClient.SendAsJsonAsync(HttpMethod.Patch, requestUri, content, headers, cancellationToken, jsonSerializerOptions);

    public static Task<HttpResponseMessage> HeadAsJsonAsync(this HttpClient httpClient, string requestUri,
        object content, Dictionary<string, string>? headers = null, CancellationToken cancellationToken = default, JsonSerializerOptions? jsonSerializerOptions = null)
        => httpClient.SendAsJsonAsync(HttpMethod.Head, requestUri, content, headers, cancellationToken, jsonSerializerOptions);

    public static Task<HttpResponseMessage> OptionsAsJsonAsync(this HttpClient httpClient, string requestUri,
        object content, Dictionary<string, string>? headers = null, CancellationToken cancellationToken = default, JsonSerializerOptions? jsonSerializerOptions = null)
        => httpClient.SendAsJsonAsync(HttpMethod.Options, requestUri, content, headers, cancellationToken, jsonSerializerOptions);

    private static async Task<HttpResponseMessage> SendAsJsonAsync(this HttpClient httpClient, HttpMethod method, string requestUri, object content, Dictionary<string, string>? headers, CancellationToken cancellationToken, JsonSerializerOptions? jsonSerializerOptions)
    {
        var requestJson = JsonSerializer.Serialize(content, jsonSerializerOptions ?? JsonSerializerOptions)!;
        var request = new HttpRequestMessage(method, requestUri)
        {
            Content = new StringContent(requestJson, Encoding.UTF8, ""application/json"")
        };
        AddHeadersToRequest(request, headers);
        var response = await httpClient.SendAsync(request, cancellationToken);
        return response;
    }


    public static Task<T> GetFromJsonAsync<T>(this HttpClient httpClient, string requestUri, HttpContent httpContent, Dictionary<string, string>? headers = null, CancellationToken cancellationToken = default, JsonSerializerOptions? jsonSerializerOptions = null)
    => httpClient.SendFromJsonAsync<T>(HttpMethod.Get, requestUri, httpContent, headers, cancellationToken, jsonSerializerOptions);

    public static Task<T> DeleteFromJsonAsync<T>(this HttpClient httpClient, string requestUri, HttpContent httpContent, Dictionary<string, string>? headers = null, CancellationToken cancellationToken = default, JsonSerializerOptions? jsonSerializerOptions = null)
        => httpClient.SendFromJsonAsync<T>(HttpMethod.Delete, requestUri, httpContent, headers, cancellationToken, jsonSerializerOptions);

    public static Task<T> PostFromJsonAsync<T>(this HttpClient httpClient, string requestUri, HttpContent httpContent, Dictionary<string, string>? headers = null, CancellationToken cancellationToken = default, JsonSerializerOptions? jsonSerializerOptions = null)
        => httpClient.SendFromJsonAsync<T>(HttpMethod.Post, requestUri, httpContent, headers, cancellationToken, jsonSerializerOptions);

    public static Task<T> PutFromJsonAsync<T>(this HttpClient httpClient, string requestUri, HttpContent httpContent, Dictionary<string, string>? headers = null, CancellationToken cancellationToken = default, JsonSerializerOptions? jsonSerializerOptions = null)
        => httpClient.SendFromJsonAsync<T>(HttpMethod.Put, requestUri, httpContent, headers, cancellationToken, jsonSerializerOptions);

    public static Task<T> PatchFromJsonAsync<T>(this HttpClient httpClient, string requestUri, HttpContent httpContent, Dictionary<string, string>? headers = null, CancellationToken cancellationToken = default, JsonSerializerOptions? jsonSerializerOptions = null)
        => httpClient.SendFromJsonAsync<T>(HttpMethod.Patch, requestUri, httpContent, headers, cancellationToken, jsonSerializerOptions);

    public static Task<T> HeadFromJsonAsync<T>(this HttpClient httpClient, string requestUri, HttpContent httpContent, Dictionary<string, string>? headers = null, CancellationToken cancellationToken = default, JsonSerializerOptions? jsonSerializerOptions = null)
        => httpClient.SendFromJsonAsync<T>(HttpMethod.Head, requestUri, httpContent, headers, cancellationToken, jsonSerializerOptions);

    public static Task<T> OptionsFromJsonAsync<T>(this HttpClient httpClient, string requestUri, HttpContent httpContent, Dictionary<string, string>? headers = null, CancellationToken cancellationToken = default, JsonSerializerOptions? jsonSerializerOptions = null)
        => httpClient.SendFromJsonAsync<T>(HttpMethod.Options, requestUri, httpContent, headers, cancellationToken, jsonSerializerOptions);

    private static async Task<T> SendFromJsonAsync<T>(this HttpClient httpClient, HttpMethod method, string requestUri, HttpContent content, Dictionary<string, string>? headers, CancellationToken cancellationToken, JsonSerializerOptions? jsonSerializerOptions)
    {
        var request = new HttpRequestMessage(method, requestUri) { Content = content };
        AddHeadersToRequest(request, headers);
        var response = await httpClient.SendAsync(request, cancellationToken);
        return await response.ReadJsonAsync<T>(jsonSerializerOptions, cancellationToken: cancellationToken);
    }


    public static Task<T> GetFromJsonAsync<T>(this HttpClient httpClient, string requestUri, Dictionary<string, string>? headers = null, CancellationToken cancellationToken = default, JsonSerializerOptions? jsonSerializerOptions = null)
        => httpClient.SendFromJsonAsync<T>(HttpMethod.Get, requestUri, headers, cancellationToken, jsonSerializerOptions);

    public static Task<T> DeleteFromJsonAsync<T>(this HttpClient httpClient, string requestUri, Dictionary<string, string>? headers = null, CancellationToken cancellationToken = default, JsonSerializerOptions? jsonSerializerOptions = null)
        => httpClient.SendFromJsonAsync<T>(HttpMethod.Delete, requestUri, headers, cancellationToken, jsonSerializerOptions);

    public static Task<T> PostFromJsonAsync<T>(this HttpClient httpClient, string requestUri, Dictionary<string, string>? headers = null, CancellationToken cancellationToken = default, JsonSerializerOptions? jsonSerializerOptions = null)
        => httpClient.SendFromJsonAsync<T>(HttpMethod.Post, requestUri, headers, cancellationToken, jsonSerializerOptions);

    public static Task<T> PutFromJsonAsync<T>(this HttpClient httpClient, string requestUri, Dictionary<string, string>? headers = null, CancellationToken cancellationToken = default, JsonSerializerOptions? jsonSerializerOptions = null)
        => httpClient.SendFromJsonAsync<T>(HttpMethod.Put, requestUri, headers, cancellationToken, jsonSerializerOptions);

    public static Task<T> PatchFromJsonAsync<T>(this HttpClient httpClient, string requestUri, Dictionary<string, string>? headers = null, CancellationToken cancellationToken = default, JsonSerializerOptions? jsonSerializerOptions = null)
        => httpClient.SendFromJsonAsync<T>(HttpMethod.Patch, requestUri, headers, cancellationToken, jsonSerializerOptions);

    public static Task<T> HeadFromJsonAsync<T>(this HttpClient httpClient, string requestUri, Dictionary<string, string>? headers = null, CancellationToken cancellationToken = default, JsonSerializerOptions? jsonSerializerOptions = null)
        => httpClient.SendFromJsonAsync<T>(HttpMethod.Head, requestUri, headers, cancellationToken, jsonSerializerOptions);

    public static Task<T> OptionsFromJsonAsync<T>(this HttpClient httpClient, string requestUri, Dictionary<string, string>? headers = null, CancellationToken cancellationToken = default, JsonSerializerOptions? jsonSerializerOptions = null)
        => httpClient.SendFromJsonAsync<T>(HttpMethod.Options, requestUri, headers, cancellationToken, jsonSerializerOptions);

    private static async Task<T> SendFromJsonAsync<T>(this HttpClient httpClient, HttpMethod method, string requestUri, Dictionary<string, string>? headers, CancellationToken cancellationToken, JsonSerializerOptions? jsonSerializerOptions)
    {
        var request = new HttpRequestMessage(method, requestUri);
        AddHeadersToRequest(request, headers);
        var response = await httpClient.SendAsync(request, cancellationToken);
        return await response.ReadJsonAsync<T>(jsonSerializerOptions, cancellationToken: cancellationToken);
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

    public static Task<HttpResponseMessage> HeadAsync(this HttpClient httpClient, string requestUri, HttpContent httpContent, Dictionary<string, string>? headers = null, CancellationToken cancellationToken = default)
        => httpClient.SendAsync(HttpMethod.Head, requestUri, httpContent, headers, cancellationToken);

    public static Task<HttpResponseMessage> OptionsAsync(this HttpClient httpClient, string requestUri, HttpContent httpContent, Dictionary<string, string>? headers = null, CancellationToken cancellationToken = default)
        => httpClient.SendAsync(HttpMethod.Options, requestUri, httpContent, headers, cancellationToken);

    private static async Task<HttpResponseMessage> SendAsync(this HttpClient httpClient, HttpMethod method, string requestUri, HttpContent httpContent, Dictionary<string, string>? headers, CancellationToken cancellationToken)
    {
        var request = new HttpRequestMessage(method, requestUri)
        {
            Content = httpContent
        };
        AddHeadersToRequest(request, headers);
        var response = await httpClient.SendAsync(request, cancellationToken);
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

    public static Task<HttpResponseMessage> HeadAsync(this HttpClient httpClient, string requestUri, Dictionary<string, string>? headers = null, CancellationToken cancellationToken = default)
        => httpClient.SendAsync(HttpMethod.Head, requestUri, headers, cancellationToken);

    public static Task<HttpResponseMessage> OptionsAsync(this HttpClient httpClient, string requestUri, Dictionary<string, string>? headers = null, CancellationToken cancellationToken = default)
        => httpClient.SendAsync(HttpMethod.Options, requestUri, headers, cancellationToken);

    private static async Task<HttpResponseMessage> SendAsync(this HttpClient httpClient, HttpMethod method, string requestUri, Dictionary<string, string>? headers, CancellationToken cancellationToken)
    {
        var request = new HttpRequestMessage(method, requestUri);
        AddHeadersToRequest(request, headers);
        var response = await httpClient.SendAsync(request, cancellationToken);
        return response;
    }

    public static async Task<T> ReadJsonAsync<T>(this HttpResponseMessage response, JsonSerializerOptions? jsonSerializerOptions = null, CancellationToken cancellationToken = default)
    {
        var stringContent = await response.Content.ReadAsStringAsync(cancellationToken);
        return JsonSerializer.Deserialize<T>(stringContent, jsonSerializerOptions ?? JsonSerializerOptions)!;
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
public class EmptyResponse { }
// modify to suit your needs
public class UnexpectedStatusCodeResponse
{
    public HttpStatusCode StatusCode { get; init; }
    public Dictionary<string, string> Headers { get; init; }
    public string? Body { get; init; }

    public UnexpectedStatusCodeResponse(HttpResponseMessage response)
    {
        if (response == null)
            throw new ArgumentNullException(nameof(response));

        StatusCode = response.StatusCode;
        Headers = new Dictionary<string, string>();

        foreach (var header in response.Headers)
        {
            Headers[header.Key] = string.Join("""", """", header.Value);
        }

        if (response.Content != null!)
        {
            Body = response.Content.ReadAsStringAsync().Result; // Consider async handling
        }
    }
}";

        public const string NewtonsoftHttpJsonExtensions = @"/// <summary>
/// Extension methods for working with JSON APIs.
/// </summary>
public static class NewtonsoftHttpClientJsonExtensions
{
    private static readonly JsonSerializerSettings _jsonSerializerSettings = new JsonSerializerSettings();

    /// <summary>
    /// Sends a GET request to the specified URI, and parses the JSON response body
    /// to create an object of the generic type.
    /// </summary>
    /// <typeparam name=""T"">A type into which the response body can be JSON-deserialized.</typeparam>
    /// <param name=""httpClient"">The <see cref=""HttpClient""/>.</param>
    /// <param name=""requestUri"">The URI that the request will be sent to.</param>
    /// <returns>The response parsed as an object of the generic type.</returns>
    public static Task<T> GetNewtonsoftJsonAsync<T>(this HttpClient httpClient, string requestUri, object content,
        Dictionary<string, string>? headers = null, CancellationToken cancellationToken = default,
        JsonSerializerSettings serializerSettings = null)
        => httpClient.SendNewtonsoftJsonAsync<T>(HttpMethod.Get, requestUri, content, headers, cancellationToken,
            serializerSettings);


    /// <summary>
    /// Sends a POST request to the specified URI, including the specified <paramref name=""content""/>
    /// in JSON-encoded format, and parses the JSON response body to create an object of the generic type.
    /// </summary>
    /// <typeparam name=""T"">A type into which the response body can be JSON-deserialized.</typeparam>
    /// <param name=""httpClient"">The <see cref=""HttpClient""/>.</param>
    /// <param name=""requestUri"">The URI that the request will be sent to.</param>
    /// <param name=""content"">Content for the request body. This will be JSON-encoded and sent as a string.</param>
    /// <returns>The response parsed as an object of the generic type.</returns>
    public static Task<T> PatchNewtonsoftJsonAsync<T>(this HttpClient httpClient, string requestUri, object content,
        Dictionary<string, string>? headers = null, CancellationToken cancellationToken = default,
        JsonSerializerSettings serializerSettings = null)
        => httpClient.SendNewtonsoftJsonAsync<T>(HttpMethod.Patch, requestUri, content, headers, cancellationToken,
            serializerSettings);

    /// <summary>
    /// Sends a POST request to the specified URI, including the specified <paramref name=""content""/>
    /// in JSON-encoded format, and parses the JSON response body to create an object of the generic type.
    /// </summary>
    /// <typeparam name=""T"">A type into which the response body can be JSON-deserialized.</typeparam>
    /// <param name=""httpClient"">The <see cref=""HttpClient""/>.</param>
    /// <param name=""requestUri"">The URI that the request will be sent to.</param>
    /// <param name=""content"">Content for the request body. This will be JSON-encoded and sent as a string.</param>
    /// <returns>The response parsed as an object of the generic type.</returns>
    public static Task<T> DeleteNewtonsoftJsonAsync<T>(this HttpClient httpClient, string requestUri,
        object content, Dictionary<string, string>? headers = null, CancellationToken cancellationToken = default,
        JsonSerializerSettings serializerSettings = null)
        => httpClient.SendNewtonsoftJsonAsync<T>(HttpMethod.Delete, requestUri, content, headers, cancellationToken,
            serializerSettings);

    /// <summary>
    /// Sends a POST request to the specified URI, including the specified <paramref name=""content""/>
    /// in JSON-encoded format, and parses the JSON response body to create an object of the generic type.
    /// </summary>
    /// <typeparam name=""T"">A type into which the response body can be JSON-deserialized.</typeparam>
    /// <param name=""httpClient"">The <see cref=""HttpClient""/>.</param>
    /// <param name=""requestUri"">The URI that the request will be sent to.</param>
    /// <param name=""content"">Content for the request body. This will be JSON-encoded and sent as a string.</param>
    /// <returns>The response parsed as an object of the generic type.</returns>
    public static Task<T> PostNewtonsoftJsonAsync<T>(this HttpClient httpClient, string requestUri, object content,
        Dictionary<string, string>? headers = null, CancellationToken cancellationToken = default,
        JsonSerializerSettings serializerSettings = null)
        => httpClient.SendNewtonsoftJsonAsync<T>(HttpMethod.Post, requestUri, content, headers, cancellationToken,
            serializerSettings);

    /// <summary>
    /// Sends a PUT request to the specified URI, including the specified <paramref name=""content""/>
    /// in JSON-encoded format, and parses the JSON response body to create an object of the generic type.
    /// </summary>
    /// <typeparam name=""T"">A type into which the response body can be JSON-deserialized.</typeparam>
    /// <param name=""httpClient"">The <see cref=""HttpClient""/>.</param>
    /// <param name=""requestUri"">The URI that the request will be sent to.</param>
    /// <param name=""content"">Content for the request body. This will be JSON-encoded and sent as a string.</param>
    /// <returns>The response parsed as an object of the generic type.</returns>
    public static Task<T> PutNewtonsoftJsonAsync<T>(this HttpClient httpClient, string requestUri, object content,
        Dictionary<string, string>? headers = null, CancellationToken cancellationToken = default,
        JsonSerializerSettings serializerSettings = null)
        => httpClient.SendNewtonsoftJsonAsync<T>(HttpMethod.Put, requestUri, content, headers, cancellationToken,
            serializerSettings);

    public static Task<T> HeadNewtonsoftJsonAsync<T>(this HttpClient httpClient, string requestUri, object content,
        Dictionary<string, string>? headers = null, CancellationToken cancellationToken = default,
        JsonSerializerSettings serializerSettings = null)
        => httpClient.SendNewtonsoftJsonAsync<T>(HttpMethod.Head, requestUri, content, headers, cancellationToken,
            serializerSettings);

    public static Task<T> OptionsNewtonsoftJsonAsync<T>(this HttpClient httpClient, string requestUri, object content,
        Dictionary<string, string>? headers = null, CancellationToken cancellationToken = default,
        JsonSerializerSettings serializerSettings = null)
        => httpClient.SendNewtonsoftJsonAsync<T>(HttpMethod.Options, requestUri, content, headers, cancellationToken,
            serializerSettings);

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
    public static async Task<T> SendNewtonsoftJsonAsync<T>(this HttpClient httpClient, HttpMethod method,
        string requestUri, object content, Dictionary<string, string>? headers, CancellationToken cancellationToken,
        JsonSerializerSettings serializerSettings)
    {
        var requestJson = JsonConvert.SerializeObject(content, serializerSettings ?? _jsonSerializerSettings);
        var request = new HttpRequestMessage(method, requestUri)
        {
            Content = new StringContent(requestJson, Encoding.UTF8, ""application/json"")
        };
        AddHeadersToRequest(request, headers);
        var response = await httpClient.SendAsync(request, cancellationToken);

        if (typeof(T) == typeof(IgnoreResponse))
        {
            return default!;
        }
        else
        {
            return await response.ReadNewtonsoftJsonAsync<T>(serializerSettings, cancellationToken: cancellationToken);
        }
    }


    public static Task<HttpResponseMessage> GetAsNewtonsoftJsonAsync(this HttpClient httpClient, string requestUri,
        object content, Dictionary<string, string>? headers = null, CancellationToken cancellationToken = default,
        JsonSerializerSettings serializerSettings = null)
        => httpClient.SendAsNewtonsoftJsonAsync(HttpMethod.Get, requestUri, content, headers, cancellationToken,
            serializerSettings);

    public static Task<HttpResponseMessage> DeleteAsNewtonsoftJsonAsync(this HttpClient httpClient,
        string requestUri,
        object content, Dictionary<string, string>? headers = null, CancellationToken cancellationToken = default,
        JsonSerializerSettings serializerSettings = null)
        => httpClient.SendAsNewtonsoftJsonAsync(HttpMethod.Delete, requestUri, content, headers, cancellationToken,
            serializerSettings);

    public static Task<HttpResponseMessage> PostAsNewtonsoftJsonAsync(this HttpClient httpClient, string requestUri,
        object content, Dictionary<string, string>? headers = null, CancellationToken cancellationToken = default,
        JsonSerializerSettings serializerSettings = null)
        => httpClient.SendAsNewtonsoftJsonAsync(HttpMethod.Post, requestUri, content, headers, cancellationToken,
            serializerSettings);

    public static Task<HttpResponseMessage> PutAsNewtonsoftJsonAsync(this HttpClient httpClient, string requestUri,
        object content, Dictionary<string, string>? headers = null, CancellationToken cancellationToken = default,
        JsonSerializerSettings serializerSettings = null)
        => httpClient.SendAsNewtonsoftJsonAsync(HttpMethod.Put, requestUri, content, headers, cancellationToken,
            serializerSettings);

    public static Task<HttpResponseMessage> PatchAsNewtonsoftJsonAsync(this HttpClient httpClient,
        string requestUri,
        object content, Dictionary<string, string>? headers = null, CancellationToken cancellationToken = default,
        JsonSerializerSettings serializerSettings = null)
        => httpClient.SendAsNewtonsoftJsonAsync(HttpMethod.Patch, requestUri, content, headers, cancellationToken,
            serializerSettings);

    public static Task<HttpResponseMessage> HeadAsNewtonsoftJsonAsync(this HttpClient httpClient,
        string requestUri,
        object content, Dictionary<string, string>? headers = null, CancellationToken cancellationToken = default,
        JsonSerializerSettings serializerSettings = null)
        => httpClient.SendAsNewtonsoftJsonAsync(HttpMethod.Head, requestUri, content, headers, cancellationToken,
            serializerSettings);

    public static Task<HttpResponseMessage> OptionsAsNewtonsoftJsonAsync(this HttpClient httpClient,
        string requestUri,
        object content, Dictionary<string, string>? headers = null, CancellationToken cancellationToken = default,
        JsonSerializerSettings serializerSettings = null)
        => httpClient.SendAsNewtonsoftJsonAsync(HttpMethod.Options, requestUri, content, headers, cancellationToken,
            serializerSettings);

    private static async Task<HttpResponseMessage> SendAsNewtonsoftJsonAsync(this HttpClient httpClient,
        HttpMethod method, string requestUri, object content, Dictionary<string, string>? headers,
        CancellationToken cancellationToken, JsonSerializerSettings serializerSettings)
    {
        var requestJson = JsonConvert.SerializeObject(content, serializerSettings ?? _jsonSerializerSettings);
        var request = new HttpRequestMessage(method, requestUri)
        {
            Content = new StringContent(requestJson, Encoding.UTF8, ""application/json"")
        };
        AddHeadersToRequest(request, headers);
        var response = await httpClient.SendAsync(request, cancellationToken);

        return response;
    }


    public static Task<T> GetFromNewtonsoftJsonAsync<T>(this HttpClient httpClient, string requestUri,
        HttpContent httpContent, Dictionary<string, string>? headers = null,
        CancellationToken cancellationToken = default, JsonSerializerSettings serializerSettings = null)
        => httpClient.SendFromNewtonsoftJsonAsync<T>(HttpMethod.Get, requestUri, httpContent, headers,
            cancellationToken, serializerSettings);

    public static Task<T> DeleteFromNewtonsoftJsonAsync<T>(this HttpClient httpClient, string requestUri,
        HttpContent httpContent, Dictionary<string, string>? headers = null,
        CancellationToken cancellationToken = default, JsonSerializerSettings serializerSettings = null)
        => httpClient.SendFromNewtonsoftJsonAsync<T>(HttpMethod.Delete, requestUri, httpContent, headers,
            cancellationToken, serializerSettings);

    public static Task<T> PostFromNewtonsoftJsonAsync<T>(this HttpClient httpClient, string requestUri,
        HttpContent httpContent, Dictionary<string, string>? headers = null,
        CancellationToken cancellationToken = default, JsonSerializerSettings serializerSettings = null)
        => httpClient.SendFromNewtonsoftJsonAsync<T>(HttpMethod.Post, requestUri, httpContent, headers,
            cancellationToken, serializerSettings);

    public static Task<T> PutFromNewtonsoftJsonAsync<T>(this HttpClient httpClient, string requestUri,
        HttpContent httpContent, Dictionary<string, string>? headers = null,
        CancellationToken cancellationToken = default, JsonSerializerSettings serializerSettings = null)
        => httpClient.SendFromNewtonsoftJsonAsync<T>(HttpMethod.Put, requestUri, httpContent, headers,
            cancellationToken, serializerSettings);

    public static Task<T> PatchFromNewtonsoftJsonAsync<T>(this HttpClient httpClient, string requestUri,
        HttpContent httpContent, Dictionary<string, string>? headers = null,
        CancellationToken cancellationToken = default, JsonSerializerSettings serializerSettings = null)
        => httpClient.SendFromNewtonsoftJsonAsync<T>(HttpMethod.Patch, requestUri, httpContent, headers,
            cancellationToken, serializerSettings);

    public static Task<T> HeadFromNewtonsoftJsonAsync<T>(this HttpClient httpClient, string requestUri,
        HttpContent httpContent, Dictionary<string, string>? headers = null,
        CancellationToken cancellationToken = default, JsonSerializerSettings serializerSettings = null)
        => httpClient.SendFromNewtonsoftJsonAsync<T>(HttpMethod.Head, requestUri, httpContent, headers,
            cancellationToken, serializerSettings);

    public static Task<T> OptionsFromNewtonsoftJsonAsync<T>(this HttpClient httpClient, string requestUri,
        HttpContent httpContent, Dictionary<string, string>? headers = null,
        CancellationToken cancellationToken = default, JsonSerializerSettings serializerSettings = null)
        => httpClient.SendFromNewtonsoftJsonAsync<T>(HttpMethod.Options, requestUri, httpContent, headers,
            cancellationToken, serializerSettings);

    private static async Task<T> SendFromNewtonsoftJsonAsync<T>(this HttpClient httpClient, HttpMethod method,
        string requestUri, HttpContent content, Dictionary<string, string>? headers,
        CancellationToken cancellationToken, JsonSerializerSettings serializerSettings)
    {
        var request = new HttpRequestMessage(method, requestUri)
        {
            Content = content
        };
        AddHeadersToRequest(request, headers);
        var response = await httpClient.SendAsync(request, cancellationToken);

        return await response.ReadNewtonsoftJsonAsync<T>(serializerSettings, cancellationToken: cancellationToken);
    }


    public static Task<T> GetFromNewtonsoftJsonAsync<T>(this HttpClient httpClient, string requestUri,
        Dictionary<string, string>? headers = null, CancellationToken cancellationToken = default,
        JsonSerializerSettings serializerSettings = null)
        => httpClient.SendFromNewtonsoftJsonAsync<T>(HttpMethod.Get, requestUri, headers, cancellationToken,
            serializerSettings);

    public static Task<T> DeleteFromNewtonsoftJsonAsync<T>(this HttpClient httpClient, string requestUri,
        Dictionary<string, string>? headers = null, CancellationToken cancellationToken = default,
        JsonSerializerSettings serializerSettings = null)
        => httpClient.SendFromNewtonsoftJsonAsync<T>(HttpMethod.Delete, requestUri, headers, cancellationToken,
            serializerSettings);

    public static Task<T> PostFromNewtonsoftJsonAsync<T>(this HttpClient httpClient, string requestUri,
        Dictionary<string, string>? headers = null, CancellationToken cancellationToken = default,
        JsonSerializerSettings serializerSettings = null)
        => httpClient.SendFromNewtonsoftJsonAsync<T>(HttpMethod.Post, requestUri, headers, cancellationToken,
            serializerSettings);

    public static Task<T> PutFromNewtonsoftJsonAsync<T>(this HttpClient httpClient, string requestUri,
        Dictionary<string, string>? headers = null, CancellationToken cancellationToken = default,
        JsonSerializerSettings serializerSettings = null)
        => httpClient.SendFromNewtonsoftJsonAsync<T>(HttpMethod.Put, requestUri, headers, cancellationToken,
            serializerSettings);

    public static Task<T> PatchFromNewtonsoftJsonAsync<T>(this HttpClient httpClient, string requestUri,
        Dictionary<string, string>? headers = null, CancellationToken cancellationToken = default,
        JsonSerializerSettings serializerSettings = null)
        => httpClient.SendFromNewtonsoftJsonAsync<T>(HttpMethod.Patch, requestUri, headers, cancellationToken,
            serializerSettings);

    public static Task<T> HeadFromNewtonsoftJsonAsync<T>(this HttpClient httpClient, string requestUri,
        Dictionary<string, string>? headers = null, CancellationToken cancellationToken = default,
        JsonSerializerSettings serializerSettings = null)
        => httpClient.SendFromNewtonsoftJsonAsync<T>(HttpMethod.Head, requestUri, headers, cancellationToken,
            serializerSettings);

    public static Task<T> OptionsFromNewtonsoftJsonAsync<T>(this HttpClient httpClient, string requestUri,
        Dictionary<string, string>? headers = null, CancellationToken cancellationToken = default,
        JsonSerializerSettings serializerSettings = null)
        => httpClient.SendFromNewtonsoftJsonAsync<T>(HttpMethod.Options, requestUri, headers, cancellationToken,
            serializerSettings);

    private static async Task<T> SendFromNewtonsoftJsonAsync<T>(this HttpClient httpClient, HttpMethod method,
        string requestUri, Dictionary<string, string>? headers, CancellationToken cancellationToken,
        JsonSerializerSettings serializerSettings)
    {
        var request = new HttpRequestMessage(method, requestUri);
        AddHeadersToRequest(request, headers);
        var response = await httpClient.SendAsync(request, cancellationToken);

        return await response.ReadNewtonsoftJsonAsync<T>(serializerSettings, cancellationToken: cancellationToken);
    }


    public static Task<HttpResponseMessage> GetAsync(this HttpClient httpClient, string requestUri,
        HttpContent httpContent, Dictionary<string, string>? headers = null,
        CancellationToken cancellationToken = default)
        => httpClient.SendAsync(HttpMethod.Get, requestUri, httpContent, headers, cancellationToken);

    public static Task<HttpResponseMessage> PostAsync(this HttpClient httpClient, string requestUri,
        HttpContent httpContent, Dictionary<string, string>? headers = null,
        CancellationToken cancellationToken = default)
        => httpClient.SendAsync(HttpMethod.Post, requestUri, httpContent, headers, cancellationToken);

    public static Task<HttpResponseMessage> PutAsync(this HttpClient httpClient, string requestUri,
        HttpContent httpContent, Dictionary<string, string>? headers = null,
        CancellationToken cancellationToken = default)
        => httpClient.SendAsync(HttpMethod.Put, requestUri, httpContent, headers, cancellationToken);

    public static Task<HttpResponseMessage> PatchAsync(this HttpClient httpClient, string requestUri,
        HttpContent httpContent, Dictionary<string, string>? headers = null,
        CancellationToken cancellationToken = default)
        => httpClient.SendAsync(HttpMethod.Patch, requestUri, httpContent, headers, cancellationToken);

    public static Task<HttpResponseMessage> DeleteAsync(this HttpClient httpClient, string requestUri,
        HttpContent httpContent, Dictionary<string, string>? headers = null,
        CancellationToken cancellationToken = default)
        => httpClient.SendAsync(HttpMethod.Delete, requestUri, httpContent, headers, cancellationToken);

    public static Task<HttpResponseMessage> HeadAsync(this HttpClient httpClient, string requestUri,
        HttpContent httpContent, Dictionary<string, string>? headers = null,
        CancellationToken cancellationToken = default)
        => httpClient.SendAsync(HttpMethod.Head, requestUri, httpContent, headers, cancellationToken);

    public static Task<HttpResponseMessage> OptionsAsync(this HttpClient httpClient, string requestUri,
        HttpContent httpContent, Dictionary<string, string>? headers = null,
        CancellationToken cancellationToken = default)
        => httpClient.SendAsync(HttpMethod.Options, requestUri, httpContent, headers, cancellationToken);

    private static async Task<HttpResponseMessage> SendAsync(this HttpClient httpClient, HttpMethod method,
        string requestUri, HttpContent httpContent, Dictionary<string, string>? headers,
        CancellationToken cancellationToken)
    {
        var request = new HttpRequestMessage(method, requestUri)
        {
            Content = httpContent
        };
        AddHeadersToRequest(request, headers);
        var response = await httpClient.SendAsync(request, cancellationToken);

        return response;
    }


    public static Task<HttpResponseMessage> GetAsync(this HttpClient httpClient, string requestUri,
        Dictionary<string, string>? headers = null, CancellationToken cancellationToken = default)
        => httpClient.SendAsync(HttpMethod.Get, requestUri, headers, cancellationToken);

    public static Task<HttpResponseMessage> PostAsync(this HttpClient httpClient, string requestUri,
        Dictionary<string, string>? headers = null, CancellationToken cancellationToken = default)
        => httpClient.SendAsync(HttpMethod.Post, requestUri, headers, cancellationToken);

    public static Task<HttpResponseMessage> PutAsync(this HttpClient httpClient, string requestUri,
        Dictionary<string, string>? headers = null, CancellationToken cancellationToken = default)
        => httpClient.SendAsync(HttpMethod.Put, requestUri, headers, cancellationToken);

    public static Task<HttpResponseMessage> PatchAsync(this HttpClient httpClient, string requestUri,
        Dictionary<string, string>? headers = null, CancellationToken cancellationToken = default)
        => httpClient.SendAsync(HttpMethod.Patch, requestUri, headers, cancellationToken);

    public static Task<HttpResponseMessage> DeleteAsync(this HttpClient httpClient, string requestUri,
        Dictionary<string, string>? headers = null, CancellationToken cancellationToken = default)
        => httpClient.SendAsync(HttpMethod.Delete, requestUri, headers, cancellationToken);

    public static Task<HttpResponseMessage> HeadAsync(this HttpClient httpClient, string requestUri,
        Dictionary<string, string>? headers = null, CancellationToken cancellationToken = default)
        => httpClient.SendAsync(HttpMethod.Head, requestUri, headers, cancellationToken);

    public static Task<HttpResponseMessage> OptionsAsync(this HttpClient httpClient, string requestUri,
        Dictionary<string, string>? headers = null, CancellationToken cancellationToken = default)
        => httpClient.SendAsync(HttpMethod.Options, requestUri, headers, cancellationToken);

    private static async Task<HttpResponseMessage> SendAsync(this HttpClient httpClient, HttpMethod method,
        string requestUri, Dictionary<string, string>? headers, CancellationToken cancellationToken)
    {
        var request = new HttpRequestMessage(method, requestUri);
        AddHeadersToRequest(request, headers);
        var response = await httpClient.SendAsync(request, cancellationToken);

        response.EnsureSuccessStatusCode();
        return response;
    }

    public static async Task<T> ReadNewtonsoftJsonAsync<T>(this HttpResponseMessage response,
        JsonSerializerSettings? serializerSettings = null, CancellationToken cancellationToken = default)
    {
        var stringContent = await response.Content.ReadAsStringAsync(cancellationToken);
        return JsonConvert.DeserializeObject<T>(stringContent, serializerSettings ?? _jsonSerializerSettings)!;
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
public class EmptyResponse { }
// modify to suit your needs
public class UnexpectedStatusCodeResponse
{
    public HttpStatusCode StatusCode { get; init; }
    public Dictionary<string, string> Headers { get; init; }
    public string? Body { get; init; }

    public UnexpectedStatusCodeResponse(HttpResponseMessage response)
    {
        if (response == null)
            throw new ArgumentNullException(nameof(response));

        StatusCode = response.StatusCode;
        Headers = new Dictionary<string, string>();

        foreach (var header in response.Headers)
        {
            Headers[header.Key] = string.Join("""", """", header.Value);
        }

        if (response.Content != null!)
        {
            Body = response.Content.ReadAsStringAsync().Result; // Consider async handling
        }
    }
}";

    }
}
