using System.Text;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
namespace Postman2CSharp
{
    /// <summary>
    /// Extension methods for working with JSON APIs.
    /// </summary>
    public static class NewtonsoftHttpClientJsonExtensions
    {
        private static readonly JsonSerializerSettings JsonSerializerSettings = new JsonSerializerSettings();
        /// <summary>
        /// Sends a GET request to the specified URI, and parses the JSON response body
        /// to create an object of the generic type.
        /// </summary>
        /// <typeparam name="T">A type into which the response body can be JSON-deserialized.</typeparam>
        /// <param name="httpClient">The <see cref="HttpClient"/>.</param>
        /// <param name="requestUri">The URI that the request will be sent to.</param>
        /// <returns>The response parsed as an object of the generic type.</returns>
        public static Task<T> GetNewtonsoftJsonAsync<T>(this HttpClient httpClient, string requestUri, object content)
            => httpClient.SendNewtonsoftJsonAsync<T>(HttpMethod.Get, requestUri, content);
    
    
        /// <summary>
        /// Sends a POST request to the specified URI, including the specified <paramref name="content"/>
        /// in JSON-encoded format, and parses the JSON response body to create an object of the generic type.
        /// </summary>
        /// <typeparam name="T">A type into which the response body can be JSON-deserialized.</typeparam>
        /// <param name="httpClient">The <see cref="HttpClient"/>.</param>
        /// <param name="requestUri">The URI that the request will be sent to.</param>
        /// <param name="content">Content for the request body. This will be JSON-encoded and sent as a string.</param>
        /// <returns>The response parsed as an object of the generic type.</returns>
        public static Task<T> PatchNewtonsoftJsonAsync<T>(this HttpClient httpClient, string requestUri, object content)
            => httpClient.SendNewtonsoftJsonAsync<T>(HttpMethod.Patch, requestUri, content);
    
        /// <summary>
        /// Sends a POST request to the specified URI, including the specified <paramref name="content"/>
        /// in JSON-encoded format, and parses the JSON response body to create an object of the generic type.
        /// </summary>
        /// <typeparam name="T">A type into which the response body can be JSON-deserialized.</typeparam>
        /// <param name="httpClient">The <see cref="HttpClient"/>.</param>
        /// <param name="requestUri">The URI that the request will be sent to.</param>
        /// <param name="content">Content for the request body. This will be JSON-encoded and sent as a string.</param>
        /// <returns>The response parsed as an object of the generic type.</returns>
        public static Task<T> DeleteNewtonsoftJsonAsync<T>(this HttpClient httpClient, string requestUri, object content)
            => httpClient.SendNewtonsoftJsonAsync<T>(HttpMethod.Delete, requestUri, content);
    
        /// <summary>
        /// Sends a POST request to the specified URI, including the specified <paramref name="content"/>
        /// in JSON-encoded format, and parses the JSON response body to create an object of the generic type.
        /// </summary>
        /// <typeparam name="T">A type into which the response body can be JSON-deserialized.</typeparam>
        /// <param name="httpClient">The <see cref="HttpClient"/>.</param>
        /// <param name="requestUri">The URI that the request will be sent to.</param>
        /// <param name="content">Content for the request body. This will be JSON-encoded and sent as a string.</param>
        /// <returns>The response parsed as an object of the generic type.</returns>
        public static Task<T> PostNewtonsoftJsonAsync<T>(this HttpClient httpClient, string requestUri, object content)
            => httpClient.SendNewtonsoftJsonAsync<T>(HttpMethod.Post, requestUri, content);
    
        /// <summary>
        /// Sends a PUT request to the specified URI, including the specified <paramref name="content"/>
        /// in JSON-encoded format, and parses the JSON response body to create an object of the generic type.
        /// </summary>
        /// <typeparam name="T">A type into which the response body can be JSON-deserialized.</typeparam>
        /// <param name="httpClient">The <see cref="HttpClient"/>.</param>
        /// <param name="requestUri">The URI that the request will be sent to.</param>
        /// <param name="content">Content for the request body. This will be JSON-encoded and sent as a string.</param>
        /// <returns>The response parsed as an object of the generic type.</returns>
        public static Task<T> PutJsonNewtonsoftAsync<T>(this HttpClient httpClient, string requestUri, object content)
            => httpClient.SendNewtonsoftJsonAsync<T>(HttpMethod.Put, requestUri, content);
    
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
        public static async Task<T> SendNewtonsoftJsonAsync<T>(this HttpClient httpClient, HttpMethod method, string requestUri, object content)
        {
            var requestJson = JsonConvert.SerializeObject(content, JsonSerializerSettings);
            var response = await httpClient.SendAsync(new HttpRequestMessage(method, requestUri)
            {
                Content = new StringContent(requestJson, Encoding.UTF8, "application/json")
            });
    
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
            object content)
            => httpClient.SendAsNewtonsoftJsonAsync(HttpMethod.Get, requestUri, content);
    
        public static Task<HttpResponseMessage> DeleteAsNewtonsoftJsonAsync(this HttpClient httpClient, string requestUri,
            object content)
            => httpClient.SendAsNewtonsoftJsonAsync(HttpMethod.Delete, requestUri, content);
    
        public static Task<HttpResponseMessage> PostAsNewtonsoftJsonAsync(this HttpClient httpClient, string requestUri,
            object content)
            => httpClient.SendAsNewtonsoftJsonAsync(HttpMethod.Post, requestUri, content);
    
        public static Task<HttpResponseMessage> PutAsNewtonsoftJsonAsync(this HttpClient httpClient, string requestUri,
            object content)
            => httpClient.SendAsNewtonsoftJsonAsync(HttpMethod.Put, requestUri, content);
    
        public static Task<HttpResponseMessage> PatchAsNewtonsoftJsonAsync(this HttpClient httpClient, string requestUri,
            object content)
            => httpClient.SendAsNewtonsoftJsonAsync(HttpMethod.Patch, requestUri, content);
    
        private static async Task<HttpResponseMessage> SendAsNewtonsoftJsonAsync(this HttpClient httpClient, HttpMethod method, string requestUri, object content)
        {
            var requestJson = JsonConvert.SerializeObject(content, JsonSerializerSettings);
            var response = await httpClient.SendAsync(new HttpRequestMessage(method, requestUri)
            {
                Content = new StringContent(requestJson, Encoding.UTF8, "application/json")
            });
    
            response.EnsureSuccessStatusCode();
            return response;
        }
    
    
        public static Task<T> GetFromNewtonsoftJsonAsync<T>(this HttpClient httpClient, string requestUri, HttpContent httpContent)
            => httpClient.SendFromNewtonsoftJsonAsync<T>(HttpMethod.Get, requestUri, httpContent);
    
        public static Task<T> DeleteFromNewtonsoftJsonAsync<T>(this HttpClient httpClient, string requestUri, HttpContent httpContent)
            => httpClient.SendFromNewtonsoftJsonAsync<T>(HttpMethod.Delete, requestUri, httpContent);
    
        public static Task<T> PostFromNewtonsoftJsonAsync<T>(this HttpClient httpClient, string requestUri, HttpContent httpContent)
            => httpClient.SendFromNewtonsoftJsonAsync<T>(HttpMethod.Post, requestUri, httpContent);
    
        public static Task<T> PutFromNewtonsoftJsonAsync<T>(this HttpClient httpClient, string requestUri, HttpContent httpContent)
            => httpClient.SendFromNewtonsoftJsonAsync<T>(HttpMethod.Put, requestUri, httpContent);
    
        public static Task<T> PatchFromNewtonsoftJsonAsync<T>(this HttpClient httpClient, string requestUri, HttpContent httpContent)
            => httpClient.SendFromNewtonsoftJsonAsync<T>(HttpMethod.Patch, requestUri, httpContent);
    
        private static async Task<T> SendFromNewtonsoftJsonAsync<T>(this HttpClient httpClient, HttpMethod method, string requestUri, HttpContent content)
        {
            var response = await httpClient.SendAsync(new HttpRequestMessage(method, requestUri)
            {
                Content = content
            });
    
            response.EnsureSuccessStatusCode();
            return await response.ReadNewtonsoftJsonAsync<T>();
        }
    
    
        public static Task<T> GetFromNewtonsoftJsonAsync<T>(this HttpClient httpClient, string requestUri)
            => httpClient.SendFromNewtonsoftJsonAsync<T>(HttpMethod.Get, requestUri);
    
        public static Task<T> DeleteFromNewtonsoftJsonAsync<T>(this HttpClient httpClient, string requestUri)
            => httpClient.SendFromNewtonsoftJsonAsync<T>(HttpMethod.Delete, requestUri);
    
        public static Task<T> PostFromNewtonsoftJsonAsync<T>(this HttpClient httpClient, string requestUri)
            => httpClient.SendFromNewtonsoftJsonAsync<T>(HttpMethod.Post, requestUri);
    
        public static Task<T> PutFromNewtonsoftJsonAsync<T>(this HttpClient httpClient, string requestUri)
            => httpClient.SendFromNewtonsoftJsonAsync<T>(HttpMethod.Put, requestUri);
    
        public static Task<T> PatchFromNewtonsoftJsonAsync<T>(this HttpClient httpClient, string requestUri)
            => httpClient.SendFromNewtonsoftJsonAsync<T>(HttpMethod.Patch, requestUri);
    
        private static async Task<T> SendFromNewtonsoftJsonAsync<T>(this HttpClient httpClient, HttpMethod method, string requestUri)
        {
            var response = await httpClient.SendAsync(new HttpRequestMessage(method, requestUri));
            response.EnsureSuccessStatusCode();
            return await response.ReadNewtonsoftJsonAsync<T>();
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
    
        public static async Task<T> ReadNewtonsoftJsonAsync<T>(this HttpResponseMessage response)
        {
            var stringContent = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(stringContent, JsonSerializerSettings);
        }
    
        class IgnoreResponse { }
    }
    public class EmptyRequest { }
    public class EmptyResponse { }
}