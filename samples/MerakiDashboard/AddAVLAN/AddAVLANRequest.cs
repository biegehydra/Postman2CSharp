using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<AddAVLANRequest>(myJsonResponse);
namespace MerakiDashboard
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
        public VlanTagging3 Ipv6 { get; set; }
        public VlanTagging3 MandatoryDhcp { get; set; }
    }
}