using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Postman2CSharp.Wasm.Services
{
    public class Navigate
    {
        public string BaseUrl => _navManager.BaseUri;
        public string CurrentUrl => _navManager.Uri;
        private readonly NavigationManager _navManager;
        private readonly Lazy<IJSRuntime> _jsRuntime;

        public Navigate(NavigationManager navManager, Lazy<IJSRuntime> jsRuntime)
        {
            _navManager = navManager;
            _jsRuntime = jsRuntime;
        }

        public void ToHome()
        {
            _navManager.NavigateTo("/");
        }

        public void ToHttpCalls(string collectionId, string apiClientId)
        {
            _navManager.NavigateTo($"Collection/{collectionId}/ApiClient/{apiClientId}/HttpCalls");
        }

        public void ToApiClient(string collectionId, string apiClientId)
        {
            _navManager.NavigateTo($"Collection/{collectionId}/ApiClient/{apiClientId}");
        }

        public void ToApiClientInterface(string collectionId, string apiClientId)
        {
            _navManager.NavigateTo($"Collection/{collectionId}/ApiClient/{apiClientId}/Interface");
        }

        public void ToApiClientController(string collectionId, string apiClientId)
        {
            _navManager.NavigateTo($"Collection/{collectionId}/ApiClient/{apiClientId}/Controller");
        }

        public void ToApiClientTests(string collectionId, string apiClientId)
        {
            _navManager.NavigateTo($"Collection/{collectionId}/ApiClient/{apiClientId}/Tests");
        }

        public void ToCollection(string collectionId)
        {
            _navManager.NavigateTo($"Collection/{collectionId}");
        }
        public void ToCoreCsFile(string fileName)
        {
            _navManager.NavigateTo($"CoreFiles/{fileName}");
        }
        public void ToHttpCallClass(string collectionId, string apiClientId, string httpCallName, string className)
        {
            _navManager.NavigateTo($"Collection/{collectionId}/ApiClient/{apiClientId}/HttpCalls/{httpCallName}/{className}");
        }

        public async Task OpenBlank(string url)
        {
            await _jsRuntime.Value.InvokeVoidAsync("open", url, "_blank");
        }
    }
}
