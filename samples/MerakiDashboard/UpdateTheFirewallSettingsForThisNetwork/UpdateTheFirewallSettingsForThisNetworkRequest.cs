using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<UpdateTheFirewallSettingsForThisNetworkRequest>(myJsonResponse);
namespace MerakiDashboard
{
    public class UpdateTheFirewallSettingsForThisNetworkRequest
    {
        public SpoofingProtection SpoofingProtection { get; set; }
    }
}