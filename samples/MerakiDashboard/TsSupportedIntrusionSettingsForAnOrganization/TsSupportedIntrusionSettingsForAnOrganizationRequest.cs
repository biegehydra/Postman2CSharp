using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<TsSupportedIntrusionSettingsForAnOrganizationRequest>(myJsonResponse);
namespace MerakiDashboard
{
    public class TsSupportedIntrusionSettingsForAnOrganizationRequest
    {
        public List<AllowedRules> AllowedRules { get; set; }
    }
}