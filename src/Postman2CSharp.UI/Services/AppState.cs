using Postman2CSharp.Core;
using Postman2CSharp.UI.Models;
using System.Reflection;

namespace Postman2CSharp.UI.Services;

public class AppState
{
    public const string CurrentVersion = "0.4";

    public List<Assembly> AdditionalAssemblies { get; set; } = new();
    public ApiClientOptions ApiClientOptions { get; private set; } = new();
    public SiteSettings SiteSettings { get; private set; } = new() { Version = CurrentVersion };
    public CSharpCodeWriterConfigInputModel CodeWriterConfigInputModel { get; private set; } = CSharpCodeWriterConfigInputModel.InputModelWithDefaults();
    public bool IsAdvancedSettingsLoaded { get; set; }
    public bool IsConvertLoaded { get; set; }

    public event Action? CSharpCodeWriterConfigInputModelChanged;
    public event Action? SettingsChanged;
    public event Action? OptionsChanged;
    public event Func<Task>? ConvertLoaded;
    public event Func<Task>? AdvancedSettingsLoaded;

    public void UpdateSiteSettings(SiteSettings newSettings)
    {
        SiteSettings = newSettings;
        SettingsChanged?.Invoke();
    }

    public void UpdateCodeWriterConfigInputModel(CSharpCodeWriterConfigInputModel newConfig)
    {
        CodeWriterConfigInputModel = newConfig.Clone();
        CSharpCodeWriterConfigInputModelChanged?.Invoke();
    }

    public void UpdateApiClientOptions(ApiClientOptions newOptions)
    {
        ApiClientOptions = newOptions.Clone();
        OptionsChanged?.Invoke();
    }

    public async Task InvokeConvertLoaded()
    {
        IsConvertLoaded = true;
        if (ConvertLoaded != null)
        {
            await ConvertLoaded.Invoke();
        }
    }

    public async Task InvokeAdvancedSettingsLoaded()
    {
        IsAdvancedSettingsLoaded = true;
        if (AdvancedSettingsLoaded != null)
        {
            await AdvancedSettingsLoaded.Invoke();
        }
    }
}