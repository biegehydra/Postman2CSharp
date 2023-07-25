using System;
using System.Collections.Generic;
using System.Text.Json;

namespace MerakiDashboard
{
    // Root myDeserializedClass = JsonSerializer.Deserialize<ReturnTheApplianceSettingsForANetworkResponse>(myJsonResponse);
    public class ReturnTheApplianceSettingsForANetworkResponse
    {
        public string ClientTrackingMethod { get; set; }
        public string DeploymentMode { get; set; }
        public DynamicDns DynamicDns { get; set; }
    }

    public class DynamicDns
    {
        public bool Enabled { get; set; }
        public string Prefix { get; set; }
        public string Url { get; set; }
    }
}