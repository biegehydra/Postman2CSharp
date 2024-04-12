using Blazor.DriverJs;
using Blazor.DriverJs.Models;
using Microsoft.JSInterop;

namespace Postman2CSharp.UI.Services;

/// <summary>
/// For now driver js doesn't work on server so this
/// will act as mock
/// </summary>
public class ServerDriverJs : IDriverJs
{
    public void Dispose()
    {
        
    }

    public Task Highlight(HighlightModel model)
    {
        return Task.CompletedTask;
    }

    public Task StartDrive(DriverModel model, DotNetObjectReference<DriverStore> dotObj)
    {
        return Task.CompletedTask;
    }
}