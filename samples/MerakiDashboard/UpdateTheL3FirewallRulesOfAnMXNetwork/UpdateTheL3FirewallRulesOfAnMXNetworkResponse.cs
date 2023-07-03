using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<UpdateTheL3FirewallRulesOfAnMXNetworkResponse>(myJsonResponse);
namespace MerakiDashboard
{
    public class UpdateTheL3FirewallRulesOfAnMXNetworkResponse
    {
        public List<Rules> Rules { get; set; }
    }
}