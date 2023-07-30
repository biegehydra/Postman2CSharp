using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using MudBlazor;
using Postman2CSharp.UI;
using Postman2CSharp.UI.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
if (!builder.RootComponents.Any())
{
    builder.RootComponents.Add<App>("#app");
    builder.RootComponents.Add<HeadOutlet>("head::after");
}

ConfigureServices(builder.Services, builder.HostEnvironment.BaseAddress);

var host = builder.Build();
var wasmEnvironment = host.Services.GetService<IWebAssemblyHostEnvironment>();
if (wasmEnvironment?.Environment != "Prerendering")
{
    var lazyLoader = host.Services.GetService<LazyLoader>();
    if (lazyLoader != null)
    {
        await lazyLoader.OnNavigating();
    }

}
await host.RunAsync();

static void ConfigureServices(IServiceCollection services, string baseAddress)
{
    services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(baseAddress) });
    services.AddMudServices(config =>
    {
        config.SnackbarConfiguration.PositionClass = Defaults.Classes.Position.BottomRight;

        config.SnackbarConfiguration.PreventDuplicates = false;
        config.SnackbarConfiguration.NewestOnTop = false;
        config.SnackbarConfiguration.ShowCloseIcon = true;
        config.SnackbarConfiguration.VisibleStateDuration = 4000;
        config.SnackbarConfiguration.HideTransitionDuration = 700;
        config.SnackbarConfiguration.ShowTransitionDuration = 700;
        config.SnackbarConfiguration.SnackbarVariant = Variant.Filled;
    });
    services.AddScoped<Navigate>();
    services.AddScoped<Interop>();
    services.AddScoped<AnalyticsInterop>();
    services.AddScoped<JsonEditorInterop>();
    services.AddScoped<TabsService>();
    services.AddScoped(typeof(Lazy<>), typeof(Lazy<>));
    services.AddScoped<LazyLoader>();
}
