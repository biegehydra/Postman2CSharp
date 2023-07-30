using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using MudBlazor;
using MudBlazor.Services;
using Postman2CSharp.UI.Infrastructure.Interfaces;
using Postman2CSharp.UI.Services;

namespace Postman2CSharp.UI.Infrastructure
{
    public static class ServiceCollectionExtensions
    {
        public static void AddMudBlazorServices(this IServiceCollection services)
        {
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
        }
        public static void AddPostman2CSharpServices(this IServiceCollection services, bool isServer)
        {
            services.AddScoped<Navigate>();
            services.AddScoped<Interop>();
            services.AddScoped<AnalyticsInterop>();
            services.AddScoped<JsonEditorInterop>();
            services.AddScoped<TabsService>();
            services.AddScoped(typeof(Lazy<>), typeof(Lazy<>));
            if (isServer)
            {
                services.AddScoped<ILazyLoader, ServerLazyLoader>();
                services.AddScoped<IWebAssemblyHostEnvironment, ServerHostEnvironment>();
            }
            else
            {
                services.AddScoped<ILazyLoader, LazyLoader>();
            }
        }
    }
}
