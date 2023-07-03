using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<SetThe11NATMappingRulesForAnMXNetworkRequest>(myJsonResponse);
namespace MerakiDashboard
{
    public class SetThe11NATMappingRulesForAnMXNetworkRequest
    {
        public List<Rules> Rules { get; set; }
    }
}