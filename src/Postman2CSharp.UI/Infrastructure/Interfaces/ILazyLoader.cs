namespace Postman2CSharp.UI.Infrastructure.Interfaces;

public interface ILazyLoader
{
    Task LoadHttpSecurityAssemblies();
    Task LoadAdvancedSettingsAssemblies();
    Task LoadConvertAssemblies();
    Task OnNavigating();
}