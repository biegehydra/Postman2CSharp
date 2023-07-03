using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<UpdateTheApplianceSettingsForANetworkRequest>(myJsonResponse);
namespace MerakiDashboard
{
    public class UpdateTheApplianceSettingsForANetworkRequest
    {
        public string ClientTrackingMethod { get; set; }
        public string DeploymentMode { get; set; }
        public DynamicDns2 DynamicDns { get; set; }
    }

    public class DynamicDns2
    {
        public string Prefix { get; set; }
        public string Enabled { get; set; }
    }
}