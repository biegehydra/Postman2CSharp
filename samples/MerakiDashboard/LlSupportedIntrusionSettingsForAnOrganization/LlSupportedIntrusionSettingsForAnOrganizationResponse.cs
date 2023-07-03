using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<LlSupportedIntrusionSettingsForAnOrganizationResponse>(myJsonResponse);
namespace MerakiDashboard
{
    public class LlSupportedIntrusionSettingsForAnOrganizationResponse
    {
        public List<AllowedRules> AllowedRules { get; set; }
    }

    public class AllowedRules
    {
        public string RuleId { get; set; }
        public string Message { get; set; }
    }
}