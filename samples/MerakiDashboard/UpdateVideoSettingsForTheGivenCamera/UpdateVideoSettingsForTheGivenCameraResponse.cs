using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<UpdateVideoSettingsForTheGivenCameraResponse>(myJsonResponse);
namespace MerakiDashboard
{
    public class UpdateVideoSettingsForTheGivenCameraResponse
    {
        public bool ExternalRtspEnabled { get; set; }
        public string RtspUrl { get; set; }
    }
}