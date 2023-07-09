using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<List<ListTheSplashLoginAttemptsForANetworkResponse>>(myJsonResponse);
namespace MerakiDashboardv135
{
    public class ListTheSplashLoginAttemptsForANetworkResponse
    {
        public string Name { get; set; }
        public string Login { get; set; }
        public string Ssid { get; set; }
        public DateTime LoginAt { get; set; }
        public string GatewayDeviceMac { get; set; }
        public string ClientMac { get; set; }
        public string ClientId { get; set; }
        public string Authorization { get; set; }
    }
}