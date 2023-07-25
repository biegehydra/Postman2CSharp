using System;
using System.Collections.Generic;
using System.Text.Json;

namespace MerakiDashboard
{
    // Root myDeserializedClass = JsonSerializer.Deserialize<ReturnsAllSupportedIntrusionSettingsForAnMXNetworkResponse>(myJsonResponse);
    public class ReturnsAllSupportedIntrusionSettingsForAnMXNetworkResponse
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