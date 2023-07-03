using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<NetPoolAndMaskConfigurationForMGsInTheNetworkResponse>(myJsonResponse);
namespace MerakiDashboard
{
    public class NetPoolAndMaskConfigurationForMGsInTheNetworkResponse
    {
        public string DeploymentMode { get; set; }
        public string Cidr { get; set; }
        public int Mask { get; set; }
        public List<Subnets> Subnets { get; set; }
    }
}