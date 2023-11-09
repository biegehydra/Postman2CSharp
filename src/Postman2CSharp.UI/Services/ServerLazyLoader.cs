using System.Reflection;
using Postman2CSharp.UI.Infrastructure.Interfaces;

namespace Postman2CSharp.UI.Services
{
    public class ServerLazyLoader : ILazyLoader
    {
        private readonly AppState _appState;
        public ServerLazyLoader(AppState appState)
        {
            _appState = appState;
        }
        public Task LoadHttpSecurityAssemblies()
        {
            return Task.CompletedTask;
        }

        public Task LoadAdvancedSettingsAssemblies()
        {
            return _appState.InvokeAdvancedSettingsLoaded();
        }

        public Task LoadConvertAssemblies()
        {
            return _appState.InvokeConvertLoaded();
        }

        public async Task OnNavigating()
        {
            _appState.AdditionalAssemblies = new List<Assembly>() {typeof(App).Assembly};
            await _appState.InvokeConvertLoaded();
            await _appState.InvokeAdvancedSettingsLoaded();
        }
    }
}
