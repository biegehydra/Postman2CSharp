using System.Reflection;
using Postman2CSharp.UI.Infrastructure;
using Postman2CSharp.UI.Infrastructure.Interfaces;

namespace Postman2CSharp.UI.Services
{
    public class ServerLazyLoader : ILazyLoader
    {
        public Task LoadHttpSecurityAssemblies()
        {
            return Task.CompletedTask;
        }

        public Task LoadAdvancedSettingsAssemblies()
        {
            LazyLoading.AdvancedSettingsLoaded = true;
            LazyLoading.InvokeAdvancedSettingsLoaded(true);
            return Task.CompletedTask;
        }

        public Task LoadConvertAssemblies()
        {
            LazyLoading.ConvertLoaded = true;
            LazyLoading.InvokeConvertLoaded(true); 
            return Task.CompletedTask;
        }

        public Task OnNavigating()
        {
            LazyLoading.AdditionalAssemblies = new List<Assembly>() {typeof(App).Assembly};
            LazyLoading.ConvertLoaded = true;
            LazyLoading.InvokeConvertLoaded(true);
            LazyLoading.AdvancedSettingsLoaded = true;
            LazyLoading.InvokeAdvancedSettingsLoaded(true);
            return Task.CompletedTask;
        }
    }
}
