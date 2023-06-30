using System.Text;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json;
namespace SystemTextJson
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
        public static Task<T> GetJsonAsync<T>(this HttpClient httpClient, string requestUri, object content)
            => httpClient.SendJsonAsync<T>(HttpMethod.Get, requestUri, content);
    
    
        /// <summary>
        /// Sends a POST request to the specified URI, including the specified <paramref name="content"/>
        /// in JSON-encoded format, and parses the JSON response body to create an object of the generic type.
        /// </summary>
        /// <typeparam name="T">A type into which the response body can be JSON-deserialized.</typeparam>
        /// <param name="httpClient">The <see cref="HttpClient"/>.</param>
        /// <param name="requestUri">The URI that the request will be sent to.</param>
        /// <param name="content">Content for the request body. This will be JSON-encoded and sent as a string.</param>
        /// <returns>The response parsed as an object of the generic type.</returns>
        public static Task<T> PatchJsonAsync<T>(this HttpClient httpClient, string requestUri, object content)
            => httpClient.SendJsonAsync<T>(HttpMethod.Patch, requestUri, content);
    
        /// <summary>
        /// Sends a POST request to the specified URI, including the specified <paramref name="content"/>
        /// in JSON-encoded format, and parses the JSON response body to create an object of the generic type.
        /// </summary>
        /// <typeparam name="T">A type into which the response body can be JSON-deserialized.</typeparam>
        /// <param name="httpClient">The <see cref="HttpClient"/>.</param>
        /// <param name="requestUri">The URI that the request will be sent to.</param>
        /// <param name="content">Content for the request body. This will be JSON-encoded and sent as a string.</param>
        /// <returns>The response parsed as an object of the generic type.</returns>
        public static Task<T> DeleteJsonAsync<T>(this HttpClient httpClient, string requestUri, object content)
            => httpClient.SendJsonAsync<T>(HttpMethod.Delete, requestUri, content);
    
        /// <summary>
        /// Sends a POST request to the specified URI, including the specified <paramref name="content"/>
        /// in JSON-encoded format, and parses the JSON response body to create an object of the generic type.
        /// </summary>
        /// <typeparam name="T">A type into which the response body can be JSON-deserialized.</typeparam>
        /// <param name="httpClient">The <see cref="HttpClient"/>.</param>
        /// <param name="requestUri">The URI that the request will be sent to.</param>
        /// <param name="content">Content for the request body. This will be JSON-encoded and sent as a string.</param>
        /// <returns>The response parsed as an object of the generic type.</returns>
        public static Task<T> PostJsonAsync<T>(this HttpClient httpClient, string requestUri, object content)
            => httpClient.SendJsonAsync<T>(HttpMethod.Post, requestUri, content);
    
        /// <summary>
        /// Sends a PUT request to the specified URI, including the specified <paramref name="content"/>
        /// in JSON-encoded format, and parses the JSON response body to create an object of the generic type.
        /// </summary>
        /// <typeparam name="T">A type into which the response body can be JSON-deserialized.</typeparam>
        /// <param name="httpClient">The <see cref="HttpClient"/>.</param>
        /// <param name="requestUri">The URI that the request will be sent to.</param>
        /// <param name="content">Content for the request body. This will be JSON-encoded and sent as a string.</param>
        /// <returns>The response parsed as an object of the generic type.</returns>
        public static Task<T> PutJsonAsync<T>(this HttpClient httpClient, string requestUri, object content)
            => httpClient.SendJsonAsync<T>(HttpMethod.Put, requestUri, content);
    
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
        public static async Task<T> SendJsonAsync<T>(this HttpClient httpClient, HttpMethod method, string requestUri, object content)
        {
            var requestJson = JsonSerializer.Serialize(content, JsonSerializerOptions)!;
            var response = await httpClient.SendAsync(new HttpRequestMessage(method, requestUri)
            {
                Content = new StringContent(requestJson, Encoding.UTF8, "application/json")
            });
            // Make sure the call was successful before we
            // attempt to process the response content
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
            object content)
            => httpClient.SendAsJsonAsync(HttpMethod.Get, requestUri, content);
    
        public static Task<HttpResponseMessage> DeleteAsJsonAsync(this HttpClient httpClient, string requestUri,
            object content)
            => httpClient.SendAsJsonAsync(HttpMethod.Delete, requestUri, content);
    
        public static Task<HttpResponseMessage> PostAsJsonAsync(this HttpClient httpClient, string requestUri,
            object content)
            => httpClient.SendAsJsonAsync(HttpMethod.Post, requestUri, content);
    
        public static Task<HttpResponseMessage> PutAsJsonAsync(this HttpClient httpClient, string requestUri,
            object content)
            => httpClient.SendAsJsonAsync(HttpMethod.Put, requestUri, content);
    
        public static Task<HttpResponseMessage> PatchAsJsonAsync(this HttpClient httpClient, string requestUri,
            object content)
            => httpClient.SendAsJsonAsync(HttpMethod.Patch, requestUri, content);
    
        private static async Task<HttpResponseMessage> SendAsJsonAsync(this HttpClient httpClient, HttpMethod method, string requestUri, object content)
        {
            var requestJson = JsonSerializer.Serialize(content, JsonSerializerOptions)!;
            var response = await httpClient.SendAsync(new HttpRequestMessage(method, requestUri)
            {
                Content = new StringContent(requestJson, Encoding.UTF8, "application/json")
            });
            response.EnsureSuccessStatusCode();
            return response;
        }
    
    
        public static Task<T> GetFromJsonAsync<T>(this HttpClient httpClient, string requestUri, HttpContent httpContent)
        => httpClient.SendFromJsonAsync<T>(HttpMethod.Get, requestUri, httpContent);
    
        public static Task<T> DeleteFromJsonAsync<T>(this HttpClient httpClient, string requestUri, HttpContent httpContent)
            => httpClient.SendFromJsonAsync<T>(HttpMethod.Delete, requestUri, httpContent);
    
        public static Task<T> PostFromJsonAsync<T>(this HttpClient httpClient, string requestUri, HttpContent httpContent)
            => httpClient.SendFromJsonAsync<T>(HttpMethod.Post, requestUri, httpContent);
    
        public static Task<T> PutFromJsonAsync<T>(this HttpClient httpClient, string requestUri, HttpContent httpContent)
            => httpClient.SendFromJsonAsync<T>(HttpMethod.Put, requestUri, httpContent);
    
        public static Task<T> PatchFromJsonAsync<T>(this HttpClient httpClient, string requestUri, HttpContent httpContent)
            => httpClient.SendFromJsonAsync<T>(HttpMethod.Patch, requestUri, httpContent);
    
        private static async Task<T> SendFromJsonAsync<T>(this HttpClient httpClient, HttpMethod method, string requestUri, HttpContent content)
        {
            var response = await httpClient.SendAsync(new HttpRequestMessage(method, requestUri)
            {
                Content = content
            });
    
            response.EnsureSuccessStatusCode();
            return await response.ReadJsonAsync<T>();
        }
    
    
        public static Task<T> GetFromJsonAsync<T>(this HttpClient httpClient, string requestUri)
            => httpClient.SendFromJsonAsync<T>(HttpMethod.Get, requestUri);
    
        public static Task<T> DeleteFromJsonAsync<T>(this HttpClient httpClient, string requestUri)
            => httpClient.SendFromJsonAsync<T>(HttpMethod.Delete, requestUri);
    
        public static Task<T> PostFromJsonAsync<T>(this HttpClient httpClient, string requestUri)
            => httpClient.SendFromJsonAsync<T>(HttpMethod.Post, requestUri);
    
        public static Task<T> PutFromJsonAsync<T>(this HttpClient httpClient, string requestUri)
            => httpClient.SendFromJsonAsync<T>(HttpMethod.Put, requestUri);
    
        public static Task<T> PatchFromJsonAsync<T>(this HttpClient httpClient, string requestUri)
            => httpClient.SendFromJsonAsync<T>(HttpMethod.Patch, requestUri);
    
        private static async Task<T> SendFromJsonAsync<T>(this HttpClient httpClient, HttpMethod method, string requestUri)
        {
            var response = await httpClient.SendAsync(new HttpRequestMessage(method, requestUri));
            response.EnsureSuccessStatusCode();
            return await response.ReadJsonAsync<T>();
        }
    
        // Missing overloads from core library
        public static Task<HttpResponseMessage> GetAsync(this HttpClient httpClient, string requestUri, HttpContent httpContent)
            => httpClient.SendAsync(HttpMethod.Get, requestUri, httpContent);
    
        public static Task<HttpResponseMessage> PostAsync(this HttpClient httpClient, string requestUri)
            => httpClient.SendAsync(HttpMethod.Post, requestUri);
    
        public static Task PutAsync(this HttpClient httpClient, string requestUri)
            => httpClient.SendAsync(HttpMethod.Put, requestUri);
    
        public static Task PatchAsync(this HttpClient httpClient, string requestUri)
            => httpClient.SendAsync(HttpMethod.Patch, requestUri);
    
        public static Task DeleteFromJsonAsync(this HttpClient httpClient, string requestUri)
            => httpClient.SendAsync(HttpMethod.Delete, requestUri);
    
        private static async Task<HttpResponseMessage> SendAsync(this HttpClient httpClient, HttpMethod method, string requestUri, HttpContent httpContent)
        {
            var response = await httpClient.SendAsync(new HttpRequestMessage(method, requestUri)
            {
                Content = httpContent
            });
            response.EnsureSuccessStatusCode();
            return response;
        }
        private static async Task<HttpResponseMessage> SendAsync(this HttpClient httpClient, HttpMethod method, string requestUri)
        {
            var response = await httpClient.SendAsync(new HttpRequestMessage(method, requestUri));
            response.EnsureSuccessStatusCode();
            return response;
        }
    
        public static async Task<T> ReadJsonAsync<T>(this HttpResponseMessage response)
        {
            var stringContent = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<T>(stringContent, JsonSerializerOptions)!;
        }
    
        class IgnoreResponse { }
    }
    public class EmptyRequest { }
    public class EmptyResponse { }
}