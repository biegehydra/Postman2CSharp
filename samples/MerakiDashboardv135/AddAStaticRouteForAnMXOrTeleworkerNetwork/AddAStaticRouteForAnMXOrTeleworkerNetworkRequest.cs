using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<AddAStaticRouteForAnMXOrTeleworkerNetworkRequest>(myJsonResponse);
namespace MerakiDashboardv135
{
    public class AddAStaticRouteForAnMXOrTeleworkerNetworkRequest
    {
        public string Name { get; set; }
        public string Subnet { get; set; }
        public string GatewayIp { get; set; }
        public string GatewayVlanId { get; set; }
    }
}