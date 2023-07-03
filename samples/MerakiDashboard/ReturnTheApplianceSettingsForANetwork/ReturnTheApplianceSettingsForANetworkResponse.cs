using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<ReturnTheApplianceSettingsForANetworkResponse>(myJsonResponse);
namespace MerakiDashboard
{
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