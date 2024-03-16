using Blazor.DriverJs;

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
    }
}
