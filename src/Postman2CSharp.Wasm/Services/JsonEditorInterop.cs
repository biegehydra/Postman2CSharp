using Microsoft.JSInterop;
using Postman2CSharp.Wasm.Components.OtherTools;

namespace Postman2CSharp.Wasm.Services
{
    public class JsonEditorInterop
    {
        private readonly IJSRuntime _jsRuntime;

        public JsonEditorInterop(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
        }

        public async Task InitJsonEditor(DotNetObjectReference<Json2CSharpPlusComponent> dotNetObjRef)
        {
            await _jsRuntime.InvokeVoidAsync("initJsonEditor", dotNetObjRef);
        }
        public async Task<string> GetJsonEditorValue()
        {
            return await _jsRuntime.InvokeAsync<string>("getJsonEditorValue");
        }

        public async Task ResetJsonEditorValue()
        {
            await _jsRuntime.InvokeVoidAsync("resetJsonEditor");
        }

        public async Task DestroyJsonEditor()
        {
            await _jsRuntime.InvokeVoidAsync("destroyJsonEditor");
        }
    }
}
