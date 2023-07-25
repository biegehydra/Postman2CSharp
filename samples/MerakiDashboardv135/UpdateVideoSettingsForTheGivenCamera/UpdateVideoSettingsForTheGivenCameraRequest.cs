using System;
using System.Collections.Generic;
using System.Text.Json;

namespace MerakiDashboard
{
    // Root myDeserializedClass = JsonSerializer.Deserialize<UpdateVideoSettingsForTheGivenCameraRequest>(myJsonResponse);
    public class UpdateVideoSettingsForTheGivenCameraRequest
    {
        public string ExternalRtspEnabled { get; set; }
    }
}