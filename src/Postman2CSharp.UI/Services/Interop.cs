using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.JSInterop;
using MudBlazor;
using Postman2CSharp.Core.Models;
using Postman2CSharp.Core.Utilities;
using Postman2CSharp.UI.Components;
using Postman2CSharp.UI.Models;
using Postman2CSharp.UI.Shared;

namespace Postman2CSharp.UI.Services
{
    public class Interop
    {
        private readonly AppState _appState;
        private readonly AnalyticsInterop _analytics;
        private readonly IJSRuntime _jsRuntime;
        private readonly Lazy<ISnackbar?> _snackBar;
        private readonly IWebAssemblyHostEnvironment _hostEnv;

        public Interop(IJSRuntime jsRuntime, Lazy<ISnackbar?> snackBar, AnalyticsInterop analytics, AppState appState, IWebAssemblyHostEnvironment hostEnv)
        {
            _jsRuntime = jsRuntime;
            _snackBar = snackBar;
            _analytics = analytics;
            _appState = appState;
            _hostEnv = hostEnv;
        }

        public async Task DownloadFile(string fileName, string fileContent, string fileExtension = ".cs")
        {
            await _analytics.TrackDownload("Download Singular File");
            await _jsRuntime.InvokeVoidAsync("downloadFile", fileName, fileContent, fileExtension);
        }

        public event Func<float, Task>? FileProgressCallback; 

        public async Task DownloadApiClient(ApiClient apiClient, Func<Task>? invokeStateHasChanged)
        {
            _appState.SiteSettings.TotalApiClientsDownloaded++;
            Snackbar? snackRef = null;
            if (invokeStateHasChanged != null)
            {
                snackRef = _snackBar.Value?.Add($"Downloading {apiClient.Name}...", Severity.Normal, x =>
                {
                    x.VisibleStateDuration = 10000;
                });
                if (_appState.SiteSettings.TotalApiClientsDownloaded == 1 || _appState.SiteSettings.TotalApiClientsDownloaded % 13 == 0)
                {
                    _snackBar.Value?.Add(Fragments.RequestToStarGithubFragment(), Severity.Info, x =>
                    {
                        x.VisibleStateDuration = 25_000;
                        x.SnackbarTypeClass = "mud-theme-tertiary";
                    });
                }
                await invokeStateHasChanged();
                await Task.Delay(5);
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

                if (invokeStateHasChanged != null && snackRef != null)
                {
                    _snackBar.Value?.Remove(snackRef);
                    _snackBar.Value?.Add($"Downloaded {apiClient.Name}!", Severity.Success, x =>
                    {
                        x.VisibleStateDuration = 5000;
                    });

                    await invokeStateHasChanged();
                    await Task.Delay(5);
                }

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
                
                await DownloadApiClient(apiClient, null);
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

        public ValueTask ScrollToSection(string elementId, int scrollDuration)
        {
            return _jsRuntime.InvokeVoidAsync("scrollToSection", elementId, scrollDuration);
        }

        public async Task LoadFile(string filePathAndName)
        {
            await _jsRuntime.InvokeVoidAsync("loadScript", filePathAndName);
        }

        public async Task LoadStyle(string stylePath)
        {
            await _jsRuntime.InvokeVoidAsync("loadStyle", stylePath);
        }

        public ValueTask FixFaviconViewBox(string? guid = null)
        {
            return _jsRuntime.InvokeVoidAsync("setFaviconViewBox", guid);
        }

        public ValueTask ApplyMarkdown(string elementId, string markdownText)
        {
            return _jsRuntime.InvokeVoidAsync("applyMarkdown", elementId, markdownText);
        }

        public ValueTask EmptyElement(string elementId)
        {
            return _jsRuntime.InvokeVoidAsync("emptyElement", elementId);
        }

        public ValueTask SplitVertical(string id1, string id2)
        {
            return _jsRuntime.InvokeVoidAsync("splitVertical", id1, id2);
        }

        public ValueTask SetupPrismObserver()
        {
            return _jsRuntime.InvokeVoidAsync("setupPrismObserver");
        }

        public ValueTask RemovePrismObserver()
        {
            return _jsRuntime.InvokeVoidAsync("removePrismObserver");
        }

        public ValueTask ClearLocalStorage()
        {
            return _jsRuntime.InvokeVoidAsync("clearLocalStorage");
        }

        public ValueTask<T> GetFromStorage<T>(string name)
        {
            return _jsRuntime.InvokeAsync<T>("getLocalStorage", name);
        }

        public ValueTask SetLocalStorage<T>(string name, T obj)
        {
            return _jsRuntime.InvokeVoidAsync("setLocalStorage", name, obj);
        }

        public ValueTask DeleteFromStorage(string name)
        {
            return _jsRuntime.InvokeVoidAsync("deleteLocalStorage", name);
        }

        public ValueTask ScrollToElement(string elementId, int extraScrollDistance)
        {
            return _jsRuntime.InvokeVoidAsync("scrollToElement", elementId, extraScrollDistance);
        }

        public ValueTask<double> GetCurrentScrollPosition()
        {
            return _jsRuntime.InvokeAsync<double>("getCurrentScrollPosition");
        }

        public ValueTask ScrollToSavedPosition(double savedPosition)
        {
            return _jsRuntime.InvokeVoidAsync("scrollToSavedPosition", savedPosition);
        }

        public async Task SetScrollPosition(string elementId, ScrollPosition scrollPosition)
        {
            if (_hostEnv.Environment == "Server")
            {
                return;
            }
            await _jsRuntime.InvokeVoidAsync("setScrollPosition", elementId, scrollPosition.ScrollTop, scrollPosition.ScrollLeft);
        }

        public ScrollPosition GetScrollPosition(string elementId)
        {
            if (_hostEnv.Environment == "Server")
            {
                return new ScrollPosition()
                {
                    ScrollLeft = 0,
                    ScrollTop = 0
                };
            }
            var scrollTop = ((IJSInProcessRuntime)_jsRuntime).Invoke<double>("getScrollPositionTop", elementId);
            var scrollLeft = ((IJSInProcessRuntime)_jsRuntime).Invoke<double>("getScrollPositionLeft", elementId);
            return new ScrollPosition()
            {
                ScrollTop = scrollTop,
                ScrollLeft = scrollLeft
            };
        }
    }
    public class ScrollPosition
    {
        public double ScrollTop { get; set; }
        public double ScrollLeft { get; set; }
    }
}
