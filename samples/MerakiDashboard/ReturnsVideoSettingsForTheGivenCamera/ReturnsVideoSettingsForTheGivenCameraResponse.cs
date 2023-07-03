using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<ReturnsVideoSettingsForTheGivenCameraResponse>(myJsonResponse);
namespace MerakiDashboard
{
    public class ReturnsVideoSettingsForTheGivenCameraResponse
    {
        public bool ExternalRtspEnabled { get; set; }
        public string RtspUrl { get; set; }
    }
}