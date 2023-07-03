using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<NanoverviewofthelicensestateforanorganizationResponse>(myJsonResponse);
namespace MerakiDashboard
{
    public class NanoverviewofthelicensestateforanorganizationResponse
    {
        public string Status { get; set; }
        public string ExpirationDate { get; set; }
        public LicensedDeviceCounts LicensedDeviceCounts { get; set; }
    }

    public class LicensedDeviceCounts
    {
        public int MS { get; set; }
    }
}