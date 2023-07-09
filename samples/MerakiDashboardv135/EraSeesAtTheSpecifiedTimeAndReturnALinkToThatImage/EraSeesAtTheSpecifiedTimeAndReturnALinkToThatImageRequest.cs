using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<EraSeesAtTheSpecifiedTimeAndReturnALinkToThatImageRequest>(myJsonResponse);
namespace MerakiDashboardv135
{
    public class EraSeesAtTheSpecifiedTimeAndReturnALinkToThatImageRequest
    {
        public string Timestamp { get; set; }
        public string Fullframe { get; set; }
    }
}