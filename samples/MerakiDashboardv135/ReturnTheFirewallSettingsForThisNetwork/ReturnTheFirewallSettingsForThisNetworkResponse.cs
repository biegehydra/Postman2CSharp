using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<ReturnTheFirewallSettingsForThisNetworkResponse>(myJsonResponse);
namespace MerakiDashboardv135
{
    public class ReturnTheFirewallSettingsForThisNetworkResponse
    {
        public SpoofingProtection SpoofingProtection { get; set; }
    }

    public class IpSourceGuard
    {
        public string Mode { get; set; }
    }

    public class SpoofingProtection
    {
        public IpSourceGuard IpSourceGuard { get; set; }
    }
}