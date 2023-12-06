using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.JSInterop;

namespace Postman2CSharp.UI.Services
{
    public class Navigate : IDisposable
    {
        public string BaseUrl => _navManager.BaseUri;
        public string CurrentUrl => _navManager.Uri;
        private readonly NavigationManager _navManager;
        private readonly Lazy<IJSRuntime> _jsRuntime;
        public event EventHandler<LocationChangedEventArgs>? LocationChanged;

        public Navigate(NavigationManager navManager, Lazy<IJSRuntime> jsRuntime)
        {
            _navManager = navManager;
            _jsRuntime = jsRuntime;
            _navManager.LocationChanged += OnLocationChanged;
        }

        private void OnLocationChanged(object? sender, LocationChangedEventArgs e)
        {
            LocationChanged?.Invoke(sender, e);
        }

        public string ToHome()
        {
            _navManager.NavigateTo("/");
            return "/";
        }

        public string ToHttpCalls(string collectionId, string apiClientId)
        {
            var url = $"Collection/{collectionId}/ApiClient/{apiClientId}/HttpCalls";
            _navManager.NavigateTo(url);
            return url;
        }

        public string ToApiClient(string collectionId, string apiClientId)
        {
            var url = $"Collection/{collectionId}/ApiClient/{apiClientId}";
            _navManager.NavigateTo(url);
            return url;
        }

        public string ToApiClientInterface(string collectionId, string apiClientId)
        {
            var url = $"Collection/{collectionId}/ApiClient/{apiClientId}/Interface";
            _navManager.NavigateTo(url);
            return url;
        }

        public string ToApiClientDuplicateRoots(string collectionId, string apiClientId)
        {
            var url = $"Collection/{collectionId}/ApiClient/{apiClientId}/DuplicateRoots";
            _navManager.NavigateTo(url);
            return url;
        }

        public string ToApiClientController(string collectionId, string apiClientId)
        {
            var url = $"Collection/{collectionId}/ApiClient/{apiClientId}/Controller";
            _navManager.NavigateTo(url);
            return url;
        }

        public string ToApiClientTests(string collectionId, string apiClientId)
        {
            var url = $"Collection/{collectionId}/ApiClient/{apiClientId}/Tests";
            _navManager.NavigateTo(url);
            return url;
        }

        public string ToCollection(string collectionId)
        {
            var url = $"Collection/{collectionId}";
            _navManager.NavigateTo(url);
            return url;
        }
        public string ToCoreCsFile(string fileName)
        {
            var url = $"Core/{fileName}";
            _navManager.NavigateTo(url);
            return url;
        }
        public string ToHttpCallClass(string collectionId, string apiClientId, string httpCallName, string className)
        {
            var url = $"Collection/{collectionId}/ApiClient/{apiClientId}/HttpCalls/{httpCallName}/{className}";
            _navManager.NavigateTo(url);
            return url;
        }

        public async Task OpenBlank(string url)
        {
            await _jsRuntime.Value.InvokeVoidAsync("open", url, "_blank");
        }

        public void Dispose()
        {
            _navManager.LocationChanged -= OnLocationChanged;
        }
    }
}
