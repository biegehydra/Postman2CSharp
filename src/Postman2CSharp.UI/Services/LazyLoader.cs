﻿using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Services;
using Postman2CSharp.UI.Infrastructure;
using Postman2CSharp.UI.Infrastructure.Interfaces;

namespace Postman2CSharp.UI.Services
{
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
            if (LazyLoading.ConvertLoaded) return;
            await _lazyAssemblyLoader.LoadAssembliesAsync(UploadAssemblies);
            LazyLoading.ConvertLoaded = true;
            LazyLoading.InvokeConvertLoaded(true);
        }


        public async Task LoadAdvancedSettingsAssemblies()
        {
            if (LazyLoading.AdvancedSettingsLoaded) return;
            await _lazyAssemblyLoader.LoadAssembliesAsync(AdvancedSettingsAssemblies);
            LazyLoading.AdvancedSettingsLoaded = true;
            LazyLoading.InvokeAdvancedSettingsLoaded(true);
        }

        private void LoadCollectionJs()
        {
            var tasks = JzipJsFileSources.Select(Interop.LoadFile).ToList();
            var getCssTask = Interop.LoadStyle(MarkDownSource);
            tasks.Add(getCssTask);
            _ = Task.WhenAll(tasks);
        }

        private async Task LoadJson2CsharpPlusFiles()
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
            var uri = NavigationManager.Uri.ToLower();
            try
            {
                if (string.IsNullOrEmpty(uri))
                {

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
