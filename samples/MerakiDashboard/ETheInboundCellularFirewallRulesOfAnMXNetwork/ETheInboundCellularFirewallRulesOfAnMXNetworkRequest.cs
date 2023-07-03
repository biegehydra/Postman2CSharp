using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<ETheInboundCellularFirewallRulesOfAnMXNetworkRequest>(myJsonResponse);
namespace MerakiDashboard
{
    public class ETheInboundCellularFirewallRulesOfAnMXNetworkRequest
    {
        public List<Rules> Rules { get; set; }
    }
}