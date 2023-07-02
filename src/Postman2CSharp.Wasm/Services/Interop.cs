using Microsoft.JSInterop;
using MudBlazor;
using Postman2CSharp.Core.Core;
using Postman2CSharp.Core.Models;
using Postman2CSharp.Core.Serialization;
using Postman2CSharp.Wasm.Components;
using Postman2CSharp.Wasm.Models;

namespace Postman2CSharp.Wasm.Services
{
    public class Interop
    {
        private readonly IJSRuntime _jsRuntime;
        private readonly Lazy<ISnackbar?> _snackBar;

        public Interop(IJSRuntime jsRuntime, Lazy<ISnackbar?> snackBar)
        {
            _jsRuntime = jsRuntime;
            _snackBar = snackBar;
        }

        public async Task DownloadFile(string fileName, string fileContent, string fileExtension = ".cs")
        {
            await _jsRuntime.InvokeVoidAsync("downloadFile", fileName, fileContent, fileExtension);
        }

        private static readonly List<string> SpecialFileEndings = new()
        {
            $"{Consts.Response}.cs",
            $"{Consts.Request}.cs",
            $"{Consts.MultipartFormData}.cs",
            $"{Consts.FormData}.cs",
            $"{Consts.Parameters}.cs"
        };

        public event Func<float, Task>? FileProgressCallback; 

        public async Task DownloadApiClient(ApiClient apiClient)
        {
            var detailsDict = apiClient.ExportToDict();
            var sourceCodeDict = detailsDict?.ToDictionary(x => x.Key, x => x.Value.SourceCode);
            if (sourceCodeDict == null)
            {
                _snackBar.Value?.Add("Something went wrong processing the Api Client. It could not be serialized correctly.");
                return;
            }

            var fileNamePathDict = new Dictionary<string, string>();

            foreach (var fileName in sourceCodeDict.Keys.ToList().Where(x => !x.Contains(nameof(CoreCsFile.OAuth2QueryParameters))))
            {
                var specialFileEnding = SpecialFileEndings.FirstOrDefault(fileName.EndsWith);
                if (specialFileEnding == null) continue;
                // GetCampaignsRequest.cs => { "GetCampaignsRequest.cs", "GetCampaigns" }
                fileNamePathDict.Add(fileName, fileName.Replace(specialFileEnding, string.Empty));
            }
            await _jsRuntime.InvokeVoidAsync("createZipAndDownload", apiClient.Name, sourceCodeDict, fileNamePathDict);
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

        public async Task ScrollToSection(string elementId, int length)
        {
            await _jsRuntime.InvokeVoidAsync("scrollToSection", elementId, length);
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
    }
}
