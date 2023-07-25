using System;
using System.Collections.Generic;
using System.Text.Json;

namespace MerakiDashboard
{
    // Root myDeserializedClass = JsonSerializer.Deserialize<EraSeesAtTheSpecifiedTimeAndReturnALinkToThatImageRequest>(myJsonResponse);
    public class EraSeesAtTheSpecifiedTimeAndReturnALinkToThatImageRequest
    {
        public string Timestamp { get; set; }
        public string Fullframe { get; set; }
    }
}