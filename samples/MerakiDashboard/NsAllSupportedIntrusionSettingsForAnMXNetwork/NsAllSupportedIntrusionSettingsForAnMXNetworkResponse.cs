using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<NsAllSupportedIntrusionSettingsForAnMXNetworkResponse>(myJsonResponse);
namespace MerakiDashboard
{
    public class NsAllSupportedIntrusionSettingsForAnMXNetworkResponse
    {
        public string Mode { get; set; }
        public string IdsRulesets { get; set; }
        public ProtectedNetworks ProtectedNetworks { get; set; }
    }

    public class ProtectedNetworks
    {
        public bool UseDefault { get; set; }
        public List<string> IncludedCidr { get; set; }
        public List<string> ExcludedCidr { get; set; }
    }
}