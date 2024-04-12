using Blazor.DriverJs.Models;
using Microsoft.JSInterop;

namespace Blazor.DriverJs;

public class DriverJs : IDriverJs
{
    private readonly Lazy<Task<IJSObjectReference>> _jsModule;

    public DriverJs(IJSRuntime js)
    {
        _jsModule = new Lazy<Task<IJSObjectReference>>(
            js.InvokeAsync<IJSObjectReference>("import", "./_content/Blazor.DriverJs/index.js")
                .AsTask());
    }

    public async Task StartDrive(DriverModel model, DotNetObjectReference<DriverStore> dotObj)
    {
        var module = await _jsModule.Value;
        await module.InvokeVoidAsync("startDrive", model, dotObj);
    }

    public async Task Highlight(HighlightModel model)
    {
        var module = await _jsModule.Value;
        await module.InvokeVoidAsync("highlight", model);
    }

    public void Dispose()
    {
        if (_jsModule.IsValueCreated) _jsModule.Value.Dispose();
    }
}