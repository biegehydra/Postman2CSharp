using System;
using System.Collections.Generic;
using System.Text.Json;

namespace MerakiDashboard
{
    // Root myDeserializedClass = JsonSerializer.Deserialize<AddAStaticRouteForAnMXOrTeleworkerNetworkRequest>(myJsonResponse);
    public class AddAStaticRouteForAnMXOrTeleworkerNetworkRequest
    {
        public string Name { get; set; }
        public string Subnet { get; set; }
        public string GatewayIp { get; set; }
        public string GatewayVlanId { get; set; }
    }
}