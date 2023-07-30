using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Postman2CSharp.UI;
using Postman2CSharp.UI.Infrastructure;
using Postman2CSharp.UI.Infrastructure.Interfaces;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
if (!builder.RootComponents.Any())
{
    builder.RootComponents.Add<App>("#app");
    builder.RootComponents.Add<HeadOutlet>("head::after");
}

ConfigureServices(builder.Services);

var host = builder.Build();
var wasmEnvironment = host.Services.GetService<IWebAssemblyHostEnvironment>();
if (wasmEnvironment?.Environment != "Prerendering")
{
    var lazyLoader = host.Services.GetService<ILazyLoader>();
    if (lazyLoader != null)
    {
        await lazyLoader.OnNavigating();
    }

}
await host.RunAsync();

static void ConfigureServices(IServiceCollection services)
{
    services.AddMudBlazorServices();
    services.AddPostman2CSharpServices(isServer: false);
}
