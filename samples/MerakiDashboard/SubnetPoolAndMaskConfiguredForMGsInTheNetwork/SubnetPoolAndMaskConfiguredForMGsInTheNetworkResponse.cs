using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<SubnetPoolAndMaskConfiguredForMGsInTheNetworkResponse>(myJsonResponse);
namespace MerakiDashboard
{
    public class SubnetPoolAndMaskConfiguredForMGsInTheNetworkResponse
    {
        public string DeploymentMode { get; set; }
        public string Cidr { get; set; }
        public int Mask { get; set; }
        public List<Subnets4> Subnets { get; set; }
    }

    public class Subnets4
    {
        public string Serial { get; set; }
        public string Name { get; set; }
        public string ApplianceIp { get; set; }
        public string Subnet { get; set; }
    }
}