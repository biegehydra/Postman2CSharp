using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Services;
using Postman2CSharp.UI.Infrastructure;
using Postman2CSharp.UI.Infrastructure.Interfaces;

namespace Postman2CSharp.UI.Services
{
    /// <summary>
    /// When loading additional css or js files through here, make sure to add them
    /// to the _Layout of the server project as well.
    /// </summary>
    public class LazyLoader : ILazyLoader
    {
        private const string MarkDownSource = "https://cdnjs.cloudflare.com/ajax/libs/github-markdown-css/5.2.0/github-markdown-dark.min.css";
        private static string[] JzipJsFileSources { get; set; } = new[]
        {
            "https://cdnjs.cloudflare.com/ajax/libs/jszip/3.10.1/jszip.min.js",
            "_content/Postman2CSharp.UI/jzip/jzip-interop.js",
            "https://cdnjs.cloudflare.com/ajax/libs/showdown/2.1.0/showdown.min.js"
        };
        private static List<string> UploadAssemblies { get; set; } = new()
        {
            "Microsoft.CodeAnalysis.CSharp.dll",
            "Microsoft.CodeAnalysis.dll",
            "Newtonsoft.Json.dll",
            "Humanizer.dll",
            "System.Text.RegularExpressions.dll",
            "System.Collections.Immutable.dll",
            "System.Linq.Expressions.dll",
            "System.ComponentModel.TypeConverter.dll",
            "System.Collections.NonGeneric.dll",
            "System.Runtime.CompilerServices.Unsafe.dll",
            "System.Runtime.Serialization.Formatters.dll",
            "System.Runtime.Serialization.Primitives.dll",
            "System.ComponentModel.Primitives.dll",
            "System.ComponentModel.Annotations.dll",
            "System.Runtime.Numerics.dll",
            "System.Private.Xml.dll",
            "System.Private.Xml.Linq.dll",
            "System.Xml.ReaderWriter.dll",
            "System.Xml.Linq.dll",
            "System.Xml.XDocument.dll",
            "System.Xml.XPath.XDocument.dll",
            "System.Diagnostics.TraceSource.dll",
        };

        private static List<string> AdvancedSettingsAssemblies { get; set; } = new()
        {
            "System.Linq.Expressions.dll",
            "System.ComponentModel.Annotations.dll",
            "System.ComponentModel.TypeConverter.dll",
            "System.ComponentModel.Primitives.dll",
        };

        private static List<string> HttpSecurityDlls { get; set; } = new()
        {
            "System.Security.Cryptography.dll"
        };

        private static List<string> Json2CsharpPlusJsFiles { get; set; } = new()
        {
            "_content/Postman2CSharp.UI/json-editor/json-editor-interop.js",
            "_content/Postman2CSharp.UI/ace/ace.js",
            "_content/Postman2CSharp.UI/ace/worker-json.js"
        };

        private static List<string> Json2CsharpPlusCssFiles { get; set; } = new()
        {
            "_content/Postman2CSharp.UI/ace/ace.css",
        };

        private readonly NavigationManager _navigationManager;
        private readonly LazyAssemblyLoader _lazyAssemblyLoader;
        private readonly AppState _appState;
        private readonly Interop _interop;

        public LazyLoader(LazyAssemblyLoader lazyAssemblyLoader, Interop interop, NavigationManager navigationManager, AppState appState)
        {
            _lazyAssemblyLoader = lazyAssemblyLoader;
            _interop = interop;
            _navigationManager = navigationManager;
            _appState = appState;
        }

        public async Task LoadHttpSecurityAssemblies()
        {
            await _lazyAssemblyLoader.LoadAssembliesAsync(HttpSecurityDlls);
        }

        public async Task LoadConvertAssemblies()
        {
            if (_appState.IsConvertLoaded) return;
            await _lazyAssemblyLoader.LoadAssembliesAsync(UploadAssemblies);
            await _appState.InvokeConvertLoaded();
        }


        public async Task LoadAdvancedSettingsAssemblies()
        {
            if (_appState.IsAdvancedSettingsLoaded) return;
            await _lazyAssemblyLoader.LoadAssembliesAsync(AdvancedSettingsAssemblies);
            await _appState.InvokeAdvancedSettingsLoaded();
        }

        private void LoadCollectionJs()
        {
            var tasks = JzipJsFileSources.Select(_interop.LoadFile).ToList();
            var getCssTask = _interop.LoadStyle(MarkDownSource);
            tasks.Add(getCssTask);
            _ = Task.WhenAll(tasks);
        }

        private async Task LoadJson2CsharpPlusFiles()
        {
            var tasks = Json2CsharpPlusJsFiles.Select(_interop.LoadFile).ToList();
            var getCssTask = Json2CsharpPlusCssFiles.Select(_interop.LoadStyle).ToList();
            tasks.AddRange(getCssTask);
            await Task.WhenAll(tasks);
        }

        private static int _uploadAttempts;
        public async Task OnNavigating()
        {
            _uploadAttempts = 0;
            retry:
            _uploadAttempts++;
            var uri = _navigationManager.Uri.ToLower();
            try
            {
                if (string.IsNullOrEmpty(uri))
                {
                    return;
                }
                if (uri.Contains("upload") || uri.Contains("collection") || uri.Contains("convert") || uri.Contains("json") || uri.Contains("interactive-demo"))
                {
                    await LoadConvertAssemblies();
                }
                if (uri.Contains("json"))
                {
                    await LoadJson2CsharpPlusFiles();
                }
                if (uri.Contains("collection"))
                {
                    LoadCollectionJs();
                }
                if (uri.Contains("advanced-settings"))
                {
                    await LoadAdvancedSettingsAssemblies();
                }
            }
            catch (Exception ex)
            {
                #if DEBUG
                Console.WriteLine(ex);
                #endif
                if (_uploadAttempts < 10)
                {
                    goto retry;
                }
            }
        }
    }
}
