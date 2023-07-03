using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<UpdateThePortForwardingRulesForAnMXNetworkResponse>(myJsonResponse);
namespace MerakiDashboard
{
    public class UpdateThePortForwardingRulesForAnMXNetworkResponse
    {
        public List<Rules> Rules { get; set; }
    }
}