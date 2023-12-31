﻿using Microsoft.JSInterop;
using Postman2CSharp.UI.Shared;

namespace Postman2CSharp.UI.Services
{
    public class JsonEditorInterop
    {
        private readonly IJSRuntime _jsRuntime;

        public JsonEditorInterop(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
        }

        public async Task Init(DotNetObjectReference<JsonEditor> dotNetObjRef, string id)
        {
            await _jsRuntime.InvokeVoidAsync("initJsonEditor", dotNetObjRef, id);
        }
        public async Task<string> GetValue(string id)
        {
            return await _jsRuntime.InvokeAsync<string>("getJsonEditorValue", id);
        }

        public async Task SetValue(string id, string value)
        { 
            await _jsRuntime.InvokeVoidAsync("setJsonEditorValue", id, value);
        }

        public async Task ResetValue(string id)
        {
            await _jsRuntime.InvokeVoidAsync("resetJsonEditor", id);
        }

        public async Task Destroy(string id)
        {
            await _jsRuntime.InvokeVoidAsync("destroyJsonEditor", id);
        }
    }
}
