using Blazor.DriverJs.Models;
using Microsoft.JSInterop;

namespace Blazor.DriverJs;

public interface IDriverJs : IDisposable
{
    Task Highlight(HighlightModel model);
    Task StartDrive(DriverModel model, DotNetObjectReference<DriverStore> dotObj);
}