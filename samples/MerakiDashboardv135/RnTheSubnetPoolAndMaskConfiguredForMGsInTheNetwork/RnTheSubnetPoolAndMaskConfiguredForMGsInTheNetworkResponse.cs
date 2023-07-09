using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<RnTheSubnetPoolAndMaskConfiguredForMGsInTheNetworkResponse>(myJsonResponse);
namespace MerakiDashboardv135
{
    public class RnTheSubnetPoolAndMaskConfiguredForMGsInTheNetworkResponse
    {
        public string DeploymentMode { get; set; }
        public string Cidr { get; set; }
        public int Mask { get; set; }
        public List<Subnets2> Subnets { get; set; }
    }

    public class Subnets2
    {
        public string Serial { get; set; }
        public string Name { get; set; }
        public string ApplianceIp { get; set; }
        public string Subnet { get; set; }
    }
}