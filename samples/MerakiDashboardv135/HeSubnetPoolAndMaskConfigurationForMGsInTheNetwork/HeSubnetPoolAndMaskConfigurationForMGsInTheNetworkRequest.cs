using System;
using System.Collections.Generic;
using System.Text.Json;

namespace MerakiDashboard
{
    // Root myDeserializedClass = JsonSerializer.Deserialize<HeSubnetPoolAndMaskConfigurationForMGsInTheNetworkRequest>(myJsonResponse);
    public class HeSubnetPoolAndMaskConfigurationForMGsInTheNetworkRequest
    {
        public string Mask { get; set; }
        public string Cidr { get; set; }
    }
}