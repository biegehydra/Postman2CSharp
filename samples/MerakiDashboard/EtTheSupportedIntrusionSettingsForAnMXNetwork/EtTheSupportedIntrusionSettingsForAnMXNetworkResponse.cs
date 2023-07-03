using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<EtTheSupportedIntrusionSettingsForAnMXNetworkResponse>(myJsonResponse);
namespace MerakiDashboard
{
    public class EtTheSupportedIntrusionSettingsForAnMXNetworkResponse
    {
        public string Mode { get; set; }
        public string IdsRulesets { get; set; }
        public ProtectedNetworks ProtectedNetworks { get; set; }
    }
}