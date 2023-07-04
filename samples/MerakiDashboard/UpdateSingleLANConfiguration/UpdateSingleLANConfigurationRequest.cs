using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<UpdateSingleLANConfigurationRequest>(myJsonResponse);
namespace MerakiDashboard
{
    public class UpdateSingleLANConfigurationRequest
    {
        public string Subnet { get; set; }
        public string ApplianceIp { get; set; }
        public Ipv6 Ipv6 { get; set; }
        public VlanTagging3 MandatoryDhcp { get; set; }
    }

    public class PrefixAssignments2
    {
        public string Autonomous { get; set; }
        public string StaticPrefix { get; set; }
        public string StaticApplianceIp6 { get; set; }
        public Origin Origin { get; set; }
    }

    public class Ipv68
    {
        public string Enabled { get; set; }
        public List<PrefixAssignments2> PrefixAssignments { get; set; }
    }
}