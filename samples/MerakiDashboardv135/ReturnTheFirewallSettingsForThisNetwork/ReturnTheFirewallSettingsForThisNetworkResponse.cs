using System;
using System.Collections.Generic;
using System.Text.Json;

namespace MerakiDashboard
{
    // Root myDeserializedClass = JsonSerializer.Deserialize<ReturnTheFirewallSettingsForThisNetworkResponse>(myJsonResponse);
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