using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<UpdateVideoSettingsForTheGivenCameraRequest>(myJsonResponse);
namespace MerakiDashboard
{
    public class UpdateVideoSettingsForTheGivenCameraRequest
    {
        public string ExternalRtspEnabled { get; set; }
    }
}