using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<EraSeesAtTheSpecifiedTimeAndReturnALinkToThatImageResponse>(myJsonResponse);
namespace MerakiDashboardv135
{
    public class EraSeesAtTheSpecifiedTimeAndReturnALinkToThatImageResponse
    {
        public string Url { get; set; }
        public string Expiry { get; set; }
    }
}