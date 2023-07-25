using System;
using System.Collections.Generic;
using System.Text.Json;

namespace MerakiDashboard
{
    // Root myDeserializedClass = JsonSerializer.Deserialize<EraSeesAtTheSpecifiedTimeAndReturnALinkToThatImageResponse>(myJsonResponse);
    public class EraSeesAtTheSpecifiedTimeAndReturnALinkToThatImageResponse
    {
        public string Url { get; set; }
        public string Expiry { get; set; }
    }
}