using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<UnbindANetworkFromATemplateRequest>(myJsonResponse);
namespace MerakiDashboard
{
    public class UnbindANetworkFromATemplateRequest
    {
        public string RetainConfigs { get; set; }
    }
}