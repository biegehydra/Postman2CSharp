using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<SetThe1ManyNATMappingRulesForAnMXNetworkResponse>(myJsonResponse);
namespace MerakiDashboard
{
    public class SetThe1ManyNATMappingRulesForAnMXNetworkResponse
    {
        public List<Rules> Rules { get; set; }
    }
}