using Microsoft.JSInterop;

namespace Postman2CSharp.UI.Services
{
    public class AnalyticsInterop
    {
        private readonly IJSRuntime _jsRuntime;

        public AnalyticsInterop(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
        }

        public async Task TrackDownload(string label)
        {
            await TrackEvent("download", "Download", label);
        }

        public async Task TrackAction(string label)
        {
            await TrackEvent("submit", "Action", label);
        }

        public async Task TrackEvent(string eventType, string eventAction, string eventLabel)
        {
            try
            {
                await _jsRuntime.InvokeVoidAsync("trackImportantEvent", eventType, eventAction, eventLabel);
            }
            catch (Exception) { }
        }
    }
}
