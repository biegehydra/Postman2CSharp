using Microsoft.JSInterop;
using MudBlazor;
using Postman2CSharp.Core.Models;
using Postman2CSharp.Core.Utilities;
using Postman2CSharp.UI.Components;
using Postman2CSharp.UI.Models;
using Postman2CSharp.UI.Shared;
using static MudBlazor.CategoryTypes;

namespace Postman2CSharp.UI.Services
{
    public class Interop
    {
        private readonly AppState _appState;
        private readonly AnalyticsInterop _analytics;
        private readonly IJSRuntime _jsRuntime;
        private readonly Lazy<ISnackbar?> _snackBar;

        public Interop(IJSRuntime jsRuntime, Lazy<ISnackbar?> snackBar, AnalyticsInterop analytics, AppState appState)
        {
            _jsRuntime = jsRuntime;
            _snackBar = snackBar;
            _analytics = analytics;
            _appState = appState;
        }

        public async Task DownloadFile(string fileName, string fileContent, string fileExtension = ".cs")
        {
            await _analytics.TrackDownload("Download Singular File");
            await _jsRuntime.InvokeVoidAsync("downloadFile", fileName, fileContent, fileExtension);
        }

        public event Func<float, Task>? FileProgressCallback; 

        public async Task DownloadApiClient(ApiClient apiClient)
        {
            _appState.SiteSettings.TotalApiClientsDownloaded++;
            if (_appState.SiteSettings.TotalApiClientsDownloaded == 1 || _appState.SiteSettings.TotalApiClientsDownloaded % 13 == 0)
            {
                _snackBar.Value?.Add(Fragments.RequestToStarGithubFragment(), Severity.Info, x =>
                {
                    x.VisibleStateDuration = 25_000;
                    x.SnackbarTypeClass = "mud-theme-tertiary";
                });
            }

            await SetLocalStorage("siteSettings", _appState.SiteSettings);


            try
            {
                var detailsDict = apiClient.ExportToDict();
                var sourceCodeDict = detailsDict?.ToDictionary(x => x.Key, x => x.Value.SourceCode);
                if (sourceCodeDict == null)
                {
                    _snackBar.Value?.Add("Something went wrong processing the Api Client. It could not be serialized correctly.");
                    return;
                }

                var fileNamePathDict = new Dictionary<string, string>();

                foreach (var (fileName, (httpCall, _)) in detailsDict!.Where(x => x.Value.HttpCall != null))
                {
                    fileNamePathDict.Add(fileName, httpCall!);
                }

                await _jsRuntime.InvokeVoidAsync("createZipAndDownload", apiClient.Name, sourceCodeDict, fileNamePathDict);

                bool isTestData = apiClient.BaseUrl?.Contains("googleapis") ?? false;
                var dataType = isTestData ? "Test Data" : "Real Data";
                await _analytics.TrackAction($"Download ApiClient - {dataType}");
            }
            catch (OutOfMemoryException)
            {
                _snackBar.Value?.Add("Application ran out of memory during process. Try deleting a large collection then refreshing the page.");
            }
        }

        public async Task DownloadAllApiClient(List<ApiClient> apiClients)
        {
            OpenFileDownloadProgressPopup();
            var total = apiClients.Count;
            var processed = 1;
            foreach (var apiClient in apiClients)
            {
                
                await DownloadApiClient(apiClient);
                await RaiseProgressCallback((float) processed / total);
                await Task.Delay(5);
                processed++;
            }
        }

        private void OpenFileDownloadProgressPopup()
        {
            var options = new ProgressPopupOptions("Downloading ApiClients...", "Downloaded All ApiClients!");
            FileProgressCallback += options.InvokeProgressCallback;

            var snackBar = _snackBar.Value?.Add<ProgressPopup>(new Dictionary<string, object>()
            {
                {"Options", options }
            }, Severity.Normal, configure =>
            {
                configure.ActionVariant = Variant.Text;
                configure.CloseAfterNavigation = false;
                configure.HideIcon = true;
                configure.ShowCloseIcon = false;
                configure.DuplicatesBehavior = SnackbarDuplicatesBehavior.Prevent;
                configure.VisibleStateDuration = 200000;
            });
            if (snackBar == null) return;
            options.Snackbar = snackBar;
        }

        private async Task RaiseProgressCallback(float progress)
        {
            var handlers = FileProgressCallback;
            if (handlers != null)
            {
                var tasks = handlers.GetInvocationList()
                    .Cast<Func<float, Task>>()
                    .Select(handler => handler(progress))
                    .ToArray();

                await Task.WhenAll(tasks);
            }
        }

        public async Task ScrollToSection(string elementId, int scrollDuration)
        {
            await _jsRuntime.InvokeVoidAsync("scrollToSection", elementId, scrollDuration);
        }

        public async Task LoadFile(string filePathAndName)
        {
            await _jsRuntime.InvokeVoidAsync("loadScript", filePathAndName);
        }

        public async Task LoadStyle(string stylePath)
        {
            await _jsRuntime.InvokeVoidAsync("loadStyle", stylePath);
        }

        public async Task FixFaviconViewBox(string? guid = null)
        {
            await _jsRuntime.InvokeVoidAsync("setFaviconViewBox", guid);
        }

        public async Task ApplyMarkdown(string elementId, string markdownText)
        {
            await _jsRuntime.InvokeVoidAsync("applyMarkdown", elementId, markdownText);
        }

        public async Task EmptyElement(string elementId)
        {
            await _jsRuntime.InvokeVoidAsync("emptyElement", elementId);
        }

        public async Task SplitVertical(string id1, string id2)
        {
            await _jsRuntime.InvokeVoidAsync("splitVertical", id1, id2);
        }

        public async Task SetupPrismObserver()
        {
            await _jsRuntime.InvokeVoidAsync("setupPrismObserver");
        }

        public async Task RemovePrismObserver()
        {
            await _jsRuntime.InvokeVoidAsync("removePrismObserver");
        }

        public async Task ClearLocalStorage()
        {
            await _jsRuntime.InvokeVoidAsync("clearLocalStorage");
        }

        public async Task<T> GetFromStorage<T>(string name)
        {
            return await _jsRuntime.InvokeAsync<T>("getLocalStorage", name);
        }

        public async Task SetLocalStorage<T>(string name, T obj)
        {
            await _jsRuntime.InvokeVoidAsync("setLocalStorage", name, obj);
        }

        public async Task DeleteFromStorage(string name)
        {
            await _jsRuntime.InvokeVoidAsync("deleteLocalStorage", name);
        }

        public async Task ScrollToElement(string elementId, int extraScrollDistance)
        {
            await _jsRuntime.InvokeVoidAsync("scrollToElement", elementId, extraScrollDistance);
        }
    }
}
