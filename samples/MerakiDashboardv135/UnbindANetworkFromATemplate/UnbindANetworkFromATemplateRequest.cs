using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<UnbindANetworkFromATemplateRequest>(myJsonResponse);
namespace MerakiDashboardv135
{
    public class UnbindANetworkFromATemplateRequest
    {
        public string RetainConfigs { get; set; }
    }
}