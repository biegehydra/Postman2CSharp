using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<UpdateSingleLANConfigurationResponse>(myJsonResponse);
namespace MerakiDashboard
{
    public class UpdateSingleLANConfigurationResponse
    {
        public string Subnet { get; set; }
        public string ApplianceIp { get; set; }
        public MandatoryDhcp MandatoryDhcp { get; set; }
        public Ipv6 Ipv6 { get; set; }
    }
}