using System;
using System.Collections.Generic;
using System.Text.Json;

namespace MerakiDashboard
{
    // Root myDeserializedClass = JsonSerializer.Deserialize<ReturnTheInboundFirewallRulesForAnMXNetworkResponse>(myJsonResponse);
    public class ReturnTheInboundFirewallRulesForAnMXNetworkResponse
    {
        public List<Rules> Rules { get; set; }
        public bool SyslogDefaultRule { get; set; }
    }
}