using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<ListTheMXL7FirewallRulesForAnMXNetworkResponse>(myJsonResponse);
namespace MerakiDashboard
{
    public class ListTheMXL7FirewallRulesForAnMXNetworkResponse
    {
        public List<Rules11> Rules { get; set; }
    }

    public class Rules11
    {
        public string Policy { get; set; }
        public string Type { get; set; }
        public string Value { get; set; }
    }
}