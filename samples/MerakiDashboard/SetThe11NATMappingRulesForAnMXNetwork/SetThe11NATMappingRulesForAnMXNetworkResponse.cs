using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<SetThe11NATMappingRulesForAnMXNetworkResponse>(myJsonResponse);
namespace MerakiDashboard
{
    public class SetThe11NATMappingRulesForAnMXNetworkResponse
    {
        public List<Rules> Rules { get; set; }
    }
}