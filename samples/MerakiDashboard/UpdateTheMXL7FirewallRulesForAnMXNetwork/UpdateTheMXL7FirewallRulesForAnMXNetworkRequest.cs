using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<UpdateTheMXL7FirewallRulesForAnMXNetworkRequest>(myJsonResponse);
namespace MerakiDashboard
{
    public class UpdateTheMXL7FirewallRulesForAnMXNetworkRequest
    {
        public List<Rules> Rules { get; set; }
    }
}