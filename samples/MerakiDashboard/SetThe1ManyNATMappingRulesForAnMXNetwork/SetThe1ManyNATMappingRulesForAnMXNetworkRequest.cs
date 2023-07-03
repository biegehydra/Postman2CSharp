using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<SetThe1ManyNATMappingRulesForAnMXNetworkRequest>(myJsonResponse);
namespace MerakiDashboard
{
    public class SetThe1ManyNATMappingRulesForAnMXNetworkRequest
    {
        public List<Rules> Rules { get; set; }
    }
}