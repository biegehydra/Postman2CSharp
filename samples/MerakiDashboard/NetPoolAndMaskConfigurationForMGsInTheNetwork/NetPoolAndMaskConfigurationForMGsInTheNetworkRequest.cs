using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<NetPoolAndMaskConfigurationForMGsInTheNetworkRequest>(myJsonResponse);
namespace MerakiDashboard
{
    public class NetPoolAndMaskConfigurationForMGsInTheNetworkRequest
    {
        public string Mask { get; set; }
        public string Cidr { get; set; }
    }
}