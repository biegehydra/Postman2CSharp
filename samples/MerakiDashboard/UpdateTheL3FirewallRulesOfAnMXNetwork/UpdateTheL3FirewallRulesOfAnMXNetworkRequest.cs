using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<UpdateTheL3FirewallRulesOfAnMXNetworkRequest>(myJsonResponse);
namespace MerakiDashboard
{
    public class UpdateTheL3FirewallRulesOfAnMXNetworkRequest
    {
        public List<Rules> Rules { get; set; }
        public string SyslogDefaultRule { get; set; }
    }
}