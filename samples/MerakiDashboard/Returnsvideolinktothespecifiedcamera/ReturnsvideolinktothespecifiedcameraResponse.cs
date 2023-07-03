using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<ReturnsVideoLinkToTheSpecifiedCameraResponse>(myJsonResponse);
namespace MerakiDashboard
{
    public class ReturnsVideoLinkToTheSpecifiedCameraResponse
    {
        public string Url { get; set; }
    }
}