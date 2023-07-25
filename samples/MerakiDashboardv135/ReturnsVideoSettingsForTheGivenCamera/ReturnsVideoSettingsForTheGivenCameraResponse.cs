using System;
using System.Collections.Generic;
using System.Text.Json;

namespace MerakiDashboard
{
    // Root myDeserializedClass = JsonSerializer.Deserialize<ReturnsVideoSettingsForTheGivenCameraResponse>(myJsonResponse);
    public class ReturnsVideoSettingsForTheGivenCameraResponse
    {
        public bool ExternalRtspEnabled { get; set; }
        public string RtspUrl { get; set; }
    }
}