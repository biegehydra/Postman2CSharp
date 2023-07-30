using System.Reflection;

namespace Postman2CSharp.UI.Infrastructure
{
    public static class LazyLoading
    {
        public static List<Assembly> AdditionalAssemblies { get; set; } = new();
        public static event Func<bool, Task>? AdvancedSettingsLoadedChanged;
        public static bool AdvancedSettingsLoaded { get; set; }
        public static event Func<bool, Task>? UploadLoadedChanged;
        public static bool ConvertLoaded { get; set; }

        public static void InvokeConvertLoaded(bool value)
        {
            UploadLoadedChanged?.Invoke(value);
        }

        public static void InvokeAdvancedSettingsLoaded(bool value)
        {
            AdvancedSettingsLoadedChanged?.Invoke(value);
        }
    }
}
