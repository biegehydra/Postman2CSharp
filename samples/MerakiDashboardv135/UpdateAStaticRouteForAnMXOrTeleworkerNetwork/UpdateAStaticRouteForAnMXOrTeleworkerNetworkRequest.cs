using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<UpdateAStaticRouteForAnMXOrTeleworkerNetworkRequest>(myJsonResponse);
namespace MerakiDashboardv135
{
    public class UpdateAStaticRouteForAnMXOrTeleworkerNetworkRequest
    {
        public string Name { get; set; }
        public string Subnet { get; set; }
        public string GatewayIp { get; set; }
        public string GatewayVlanId { get; set; }
        public string Enabled { get; set; }
        public FixedIpAssignments FixedIpAssignments { get; set; }
        public List<ReservedIpRanges> ReservedIpRanges { get; set; }
    }
}