using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<TsSupportedIntrusionSettingsForAnOrganizationResponse>(myJsonResponse);
namespace MerakiDashboard
{
    public class TsSupportedIntrusionSettingsForAnOrganizationResponse
    {
        public List<AllowedRules> AllowedRules { get; set; }
    }
}