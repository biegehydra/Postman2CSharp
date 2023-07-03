using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<UpdateTheMXL7FirewallRulesForAnMXNetworkResponse>(myJsonResponse);
namespace MerakiDashboard
{
    public class UpdateTheMXL7FirewallRulesForAnMXNetworkResponse
    {
        public List<Rules> Rules { get; set; }
    }
}