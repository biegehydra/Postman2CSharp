using Microsoft.AspNetCore.Components.WebAssembly.Services;
using System.Reflection;
using Microsoft.AspNetCore.Components;

namespace Postman2CSharp.Wasm.Services
{
    public class LazyLoader
    {
        public static List<Assembly> AdditionalAssemblies { get; set; } = new();
        private const string MarkDownSource = "https://cdnjs.cloudflare.com/ajax/libs/github-markdown-css/5.2.0/github-markdown-dark.min.css";
        private static string[] JzipJsFileSources { get; set; } = new[]
        {
            "https://cdnjs.cloudflare.com/ajax/libs/jszip/3.10.1/jszip.min.js",
            "jzip/jzip-interop.js",
            "https://cdnjs.cloudflare.com/ajax/libs/showdown/2.1.0/showdown.min.js"
        };
        public static event Func<bool, Task>? AdvancedSettingsLoadedChanged;
        public static bool AdvancedSettingsLoaded { get; set; }
        public static event Func<bool, Task>? UploadLoadedChanged;
        public static bool ConvertLoaded { get; set; }
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
            "json-editor/json-editor-interop.js",
            "ace/ace.js",
            "ace/worker-json.js"
        };

        private static List<string> Json2CsharpPlusCssFiles { get; set; } = new()
        {
            "ace/ace.css",
        };

        private NavigationManager NavigationManager { get; }

        private Interop Interop { get; }

        private readonly LazyAssemblyLoader _lazyAssemblyLoader;

        public LazyLoader(LazyAssemblyLoader lazyAssemblyLoader, Interop interop, NavigationManager navigationManager)
        {
            _lazyAssemblyLoader = lazyAssemblyLoader;
            Interop = interop;
            NavigationManager = navigationManager;
        }

        public async Task LoadHttpSecurityAssemblies()
        {
            await _lazyAssemblyLoader.LoadAssembliesAsync(HttpSecurityDlls);
        }

        public async Task LoadConvertAssemblies()
        {
            if (ConvertLoaded) return;
            await _lazyAssemblyLoader.LoadAssembliesAsync(UploadAssemblies);
            ConvertLoaded = true;
            UploadLoadedChanged?.Invoke(ConvertLoaded);
        }

        public async Task LoadAdvancedSettingsAssemblies()
        {
            if (AdvancedSettingsLoaded) return;
            await _lazyAssemblyLoader.LoadAssembliesAsync(AdvancedSettingsAssemblies);
            AdvancedSettingsLoaded = true;
            AdvancedSettingsLoadedChanged?.Invoke(AdvancedSettingsLoaded);
        }

        private void LoadCollectionJs()
        {
            var tasks = JzipJsFileSources.Select(Interop.LoadFile).ToList();
            var getCssTask = Interop.LoadStyle(MarkDownSource);
            tasks.Add(getCssTask);
            _ = Task.WhenAll(tasks);
        }

        private async Task LoadJson2CsharpPlusFile()
        {
            var tasks = Json2CsharpPlusJsFiles.Select(Interop.LoadFile).ToList();
            var getCssTask = Json2CsharpPlusCssFiles.Select(Interop.LoadStyle).ToList();
            tasks.AddRange(getCssTask);
            await Task.WhenAll(tasks);
        }

        private static int _uploadAttempts;
        public async Task OnNavigating()
        {
            _uploadAttempts = 0;
            retry:
            _uploadAttempts++;
            var uri = NavigationManager.Uri;
            try
            {
                if (string.IsNullOrEmpty(uri))
                {

                }
                if (uri.Contains("Upload") || uri.Contains("Collection") || uri.Contains("Convert") || uri.Contains("json"))
                {
                    await LoadConvertAssemblies();
                }
                if (uri.Contains("json"))
                {
                    await LoadJson2CsharpPlusFile();
                }
                if (uri.Contains("Collection"))
                {
                    LoadCollectionJs();
                }
                if (uri.Contains("Advanced-Settings"))
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
