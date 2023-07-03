using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<UpdateTheInboundFirewallRulesOfAnMXNetworkRequest>(myJsonResponse);
namespace MerakiDashboard
{
    public class UpdateTheInboundFirewallRulesOfAnMXNetworkRequest
    {
        public List<Rules> Rules { get; set; }
        public string SyslogDefaultRule { get; set; }
    }
}