using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<UpdateTheFirewallSettingsForThisNetworkResponse>(myJsonResponse);
namespace MerakiDashboard
{
    public class UpdateTheFirewallSettingsForThisNetworkResponse
    {
        public SpoofingProtection SpoofingProtection { get; set; }
    }
}