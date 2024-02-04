using System.Text;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json;

namespace PaypalSubscription
{
    /// <summary>
    /// Extension methods for working with JSON APIs.
    /// </summary>
    public static class HttpClientJsonExtensions
    {
        private static readonly JsonSerializerOptions JsonSerializerOptions = new(JsonSerializerDefaults.Web);
        /// <summary>
        /// Sends a GET request to the specified URI, and parses the JSON response body
        /// to create an object of the generic type.
        /// </summary>
        /// <typeparam name="T">A type into which the response body can be JSON-deserialized.</typeparam>
        /// <param name="httpClient">The <see cref="HttpClient"/>.</param>
        /// <param name="requestUri">The URI that the request will be sent to.</param>
        /// <returns>The response parsed as an object of the generic type.</returns>
        public static Task<T> GetJsonAsync<T>(this HttpClient httpClient, string requestUri, object content, Dictionary<string, string>? headers = null, CancellationToken cancellationToken = default, JsonSerializerOptions? jsonSerializerOptions = null)
            => httpClient.SendJsonAsync<T>(HttpMethod.Get, requestUri, content, headers, cancellationToken, jsonSerializerOptions);
    
    
        /// <summary>
        /// Sends a POST request to the specified URI, including the specified <paramref name="content"/>
        /// in JSON-encoded format, and parses the JSON response body to create an object of the generic type.
        /// </summary>
        /// <typeparam name="T">A type into which the response body can be JSON-deserialized.</typeparam>
        /// <param name="httpClient">The <see cref="HttpClient"/>.</param>
        /// <param name="requestUri">The URI that the request will be sent to.</param>
        /// <param name="content">Content for the request body. This will be JSON-encoded and sent as a string.</param>
        /// <returns>The response parsed as an object of the generic type.</returns>
        public static Task<T> PatchJsonAsync<T>(this HttpClient httpClient, string requestUri, object content, Dictionary<string, string>? headers = null, CancellationToken cancellationToken = default, JsonSerializerOptions? jsonSerializerOptions = null)
            => httpClient.SendJsonAsync<T>(HttpMethod.Patch, requestUri, content, headers, cancellationToken, jsonSerializerOptions);
    
        /// <summary>
        /// Sends a POST request to the specified URI, including the specified <paramref name="content"/>
        /// in JSON-encoded format, and parses the JSON response body to create an object of the generic type.
        /// </summary>
        /// <typeparam name="T">A type into which the response body can be JSON-deserialized.</typeparam>
        /// <param name="httpClient">The <see cref="HttpClient"/>.</param>
        /// <param name="requestUri">The URI that the request will be sent to.</param>
        /// <param name="content">Content for the request body. This will be JSON-encoded and sent as a string.</param>
        /// <returns>The response parsed as an object of the generic type.</returns>
        public static Task<T> DeleteJsonAsync<T>(this HttpClient httpClient, string requestUri, object content, Dictionary<string, string>? headers = null, CancellationToken cancellationToken = default, JsonSerializerOptions? jsonSerializerOptions = null)
            => httpClient.SendJsonAsync<T>(HttpMethod.Delete, requestUri, content, headers, cancellationToken, jsonSerializerOptions);
    
        /// <summary>
        /// Sends a POST request to the specified URI, including the specified <paramref name="content"/>
        /// in JSON-encoded format, and parses the JSON response body to create an object of the generic type.
        /// </summary>
        /// <typeparam name="T">A type into which the response body can be JSON-deserialized.</typeparam>
        /// <param name="httpClient">The <see cref="HttpClient"/>.</param>
        /// <param name="requestUri">The URI that the request will be sent to.</param>
        /// <param name="content">Content for the request body. This will be JSON-encoded and sent as a string.</param>
        /// <returns>The response parsed as an object of the generic type.</returns>
        public static Task<T> PostJsonAsync<T>(this HttpClient httpClient, string requestUri, object content, Dictionary<string, string>? headers = null, CancellationToken cancellationToken = default, JsonSerializerOptions? jsonSerializerOptions = null)
            => httpClient.SendJsonAsync<T>(HttpMethod.Post, requestUri, content, headers, cancellationToken, jsonSerializerOptions);
    
        /// <summary>
        /// Sends a PUT request to the specified URI, including the specified <paramref name="content"/>
        /// in JSON-encoded format, and parses the JSON response body to create an object of the generic type.
        /// </summary>
        /// <typeparam name="T">A type into which the response body can be JSON-deserialized.</typeparam>
        /// <param name="httpClient">The <see cref="HttpClient"/>.</param>
        /// <param name="requestUri">The URI that the request will be sent to.</param>
        /// <param name="content">Content for the request body. This will be JSON-encoded and sent as a string.</param>
        /// <returns>The response parsed as an object of the generic type.</returns>
        public static Task<T> PutJsonAsync<T>(this HttpClient httpClient, string requestUri, object content, Dictionary<string, string>? headers = null, CancellationToken cancellationToken = default, JsonSerializerOptions? jsonSerializerOptions = null)
            => httpClient.SendJsonAsync<T>(HttpMethod.Put, requestUri, content, headers, cancellationToken, jsonSerializerOptions);
    
        /// <summary>
        /// Sends an HTTP request to the specified URI, including the specified <paramref name="content"/>
        /// in JSON-encoded format, and parses the JSON response body to create an object of the generic type.
        /// </summary>
        /// <typeparam name="T">A type into which the response body can be JSON-deserialized.</typeparam>
        /// <param name="httpClient">The <see cref="HttpClient"/>.</param>
        /// <param name="method">The HTTP method.</param>
        /// <param name="requestUri">The URI that the request will be sent to.</param>
        /// <param name="content">Content for the request body. This will be JSON-encoded and sent as a string.</param>
        /// <returns>The response parsed as an object of the generic type.</returns>
        private static async Task<T> SendJsonAsync<T>(this HttpClient httpClient, HttpMethod method, string requestUri, object content, Dictionary<string, string>? headers, CancellationToken cancellationToken, JsonSerializerOptions? jsonSerializerOptions)
        {
            var requestJson = JsonSerializer.Serialize(content, jsonSerializerOptions ?? JsonSerializerOptions)!;
            var request = new HttpRequestMessage(method, requestUri)
            {
                Content = new StringContent(requestJson, Encoding.UTF8, "application/json")
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
    
        private static async Task<HttpResponseMessage> SendAsJsonAsync(this HttpClient httpClient, HttpMethod method, string requestUri, object content, Dictionary<string, string>? headers, CancellationToken cancellationToken, JsonSerializerOptions? jsonSerializerOptions)
        {
            var requestJson = JsonSerializer.Serialize(content, jsonSerializerOptions ?? JsonSerializerOptions)!;
            var request = new HttpRequestMessage(method, requestUri)
            {
                Content = new StringContent(requestJson, Encoding.UTF8, "application/json")
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
}