using System;
using System.Collections.Generic;
using System.Text.Json;

namespace MerakiDashboard
{
    // Root myDeserializedClass = JsonSerializer.Deserialize<UrnsAllSupportedIntrusionSettingsForAnOrganizationResponse>(myJsonResponse);
    public class UrnsAllSupportedIntrusionSettingsForAnOrganizationResponse
    {
        public List<AllowedRules> AllowedRules { get; set; }
    }

    public class AllowedRules
    {
        public string RuleId { get; set; }
        public string Message { get; set; }
    }
}