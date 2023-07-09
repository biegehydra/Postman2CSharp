using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<UrnsAllSupportedIntrusionSettingsForAnOrganizationResponse>(myJsonResponse);
namespace MerakiDashboardv135
{
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