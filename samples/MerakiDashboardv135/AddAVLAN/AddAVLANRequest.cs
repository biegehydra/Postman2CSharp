using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<AddAVLANRequest>(myJsonResponse);
namespace MerakiDashboardv135
{
    public class AddAVLANRequest
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Subnet { get; set; }
        public string ApplianceIp { get; set; }
        public string GroupPolicyId { get; set; }
        public string TemplateVlanType { get; set; }
        public string Cidr { get; set; }
        public string Mask { get; set; }
        public Ipv8 Ipv6 { get; set; }
        public MandatoryDhcp2 MandatoryDhcp { get; set; }
    }

    public class PrefixAssignments2
    {
        public string Autonomous { get; set; }
        public string StaticPrefix { get; set; }
        public string StaticApplianceIp6 { get; set; }
        public Origin2 Origin { get; set; }
    }

    public class Ipv8
    {
        public string Enabled { get; set; }
        public List<PrefixAssignments2> PrefixAssignments { get; set; }
    }

    public class MandatoryDhcp2
    {
        public string Enabled { get; set; }
    }
}