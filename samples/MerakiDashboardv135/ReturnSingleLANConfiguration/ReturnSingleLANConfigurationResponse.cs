using System;
using System.Collections.Generic;
using System.Text.Json;

namespace MerakiDashboard
{
    // Root myDeserializedClass = JsonSerializer.Deserialize<ReturnSingleLANConfigurationResponse>(myJsonResponse);
    public class ReturnSingleLANConfigurationResponse
    {
        public string Subnet { get; set; }
        public string ApplianceIp { get; set; }
        public Ipv6 MandatoryDhcp { get; set; }
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
}