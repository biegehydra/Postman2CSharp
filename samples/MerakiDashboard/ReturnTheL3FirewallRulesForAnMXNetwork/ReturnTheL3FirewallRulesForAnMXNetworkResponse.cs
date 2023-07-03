using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<ReturnTheL3FirewallRulesForAnMXNetworkResponse>(myJsonResponse);
namespace MerakiDashboard
{
    public class ReturnTheL3FirewallRulesForAnMXNetworkResponse
    {
        public List<Rules> Rules { get; set; }
    }
}