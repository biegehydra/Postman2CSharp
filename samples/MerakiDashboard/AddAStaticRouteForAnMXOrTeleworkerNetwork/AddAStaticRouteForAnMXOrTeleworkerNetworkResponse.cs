using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<AddAStaticRouteForAnMXOrTeleworkerNetworkResponse>(myJsonResponse);
namespace MerakiDashboard
{
    public class AddAStaticRouteForAnMXOrTeleworkerNetworkResponse
    {
        public string Id { get; set; }
        public int IpVersion { get; set; }
        public string NetworkId { get; set; }
        public bool Enabled { get; set; }
        public string Name { get; set; }
        public string Subnet { get; set; }
        public string GatewayIp { get; set; }
        public FixedIpAssignments FixedIpAssignments { get; set; }
        public List<object> ReservedIpRanges { get; set; }
        public int GatewayVlanId { get; set; }
    }
}