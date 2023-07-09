using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<ReturnSingleLANConfigurationResponse>(myJsonResponse);
namespace MerakiDashboardv135
{
    public class ReturnSingleLANConfigurationResponse
    {
        public string Subnet { get; set; }
        public string ApplianceIp { get; set; }
        public MandatoryDhcp MandatoryDhcp { get; set; }
        public Ipv6 Ipv6 { get; set; }
    }

    public class PrefixAssignments
    {
        public bool Autonomous { get; set; }
        public string StaticPrefix { get; set; }
        public string StaticApplianceIp6 { get; set; }
        public Origin2 Origin { get; set; }
    }

    public class Ipv6
    {
        public bool Enabled { get; set; }
        public List<PrefixAssignments> PrefixAssignments { get; set; }
    }

    public class MandatoryDhcp
    {
        public bool Enabled { get; set; }
    }
}