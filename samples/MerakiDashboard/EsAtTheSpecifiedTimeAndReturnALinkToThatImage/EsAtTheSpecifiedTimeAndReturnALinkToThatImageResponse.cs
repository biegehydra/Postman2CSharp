using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<EsAtTheSpecifiedTimeAndReturnALinkToThatImageResponse>(myJsonResponse);
namespace MerakiDashboard
{
    public class EsAtTheSpecifiedTimeAndReturnALinkToThatImageResponse
    {
        public string Url { get; set; }
        public string Expiry { get; set; }
    }
}