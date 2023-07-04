using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<AddAVLANResponse>(myJsonResponse);
namespace MerakiDashboard
{
    public class AddAVLANResponse
    {
        public string Id { get; set; }
        public string InterfaceId { get; set; }
        public string Name { get; set; }
        public string Subnet { get; set; }
        public string ApplianceIp { get; set; }
        public string GroupPolicyId { get; set; }
        public string TemplateVlanType { get; set; }
        public string Cidr { get; set; }
        public int Mask { get; set; }
        public Wan2 MandatoryDhcp { get; set; }
        public Ipv6 Ipv6 { get; set; }
    }
}