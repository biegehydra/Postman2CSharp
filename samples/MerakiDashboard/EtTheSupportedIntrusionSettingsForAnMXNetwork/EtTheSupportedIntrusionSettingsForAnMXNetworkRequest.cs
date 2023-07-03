using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<EtTheSupportedIntrusionSettingsForAnMXNetworkRequest>(myJsonResponse);
namespace MerakiDashboard
{
    public class EtTheSupportedIntrusionSettingsForAnMXNetworkRequest
    {
        public string Mode { get; set; }
        public string IdsRulesets { get; set; }
        public ProtectedNetworks2 ProtectedNetworks { get; set; }
    }

    public class ProtectedNetworks2
    {
        public string UseDefault { get; set; }
        public List<string> IncludedCidr { get; set; }
        public List<string> ExcludedCidr { get; set; }
    }
}