using Blazor.DriverJs;
using Blazor.DriverJs.Models;

namespace Postman2CSharp.UI.Shared
{
    public class AppSettings
    {
        public static readonly string[] ShowButtons =
        [
            DriverJsDefaultValues.ButtonNext,
            DriverJsDefaultValues.ButtonPrevious,
        ];
        public bool ShowSnackBarPopups { get; set; } = true;

        public static DriverConfigurationModel DriverConfig = new DriverConfigurationModel()
        {
            AllowClose = false, PopoverClass = "driverjs-theme", ShowButtons = AppSettings.ShowButtons,
            DisableActiveInteraction = true, SmoothScroll = true
        };
    }
}
