using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<UpdateTheApplianceSettingsForANetworkResponse>(myJsonResponse);
namespace MerakiDashboard
{
    public class UpdateTheApplianceSettingsForANetworkResponse
    {
        public string ClientTrackingMethod { get; set; }
        public string DeploymentMode { get; set; }
        public DynamicDns DynamicDns { get; set; }
    }
}