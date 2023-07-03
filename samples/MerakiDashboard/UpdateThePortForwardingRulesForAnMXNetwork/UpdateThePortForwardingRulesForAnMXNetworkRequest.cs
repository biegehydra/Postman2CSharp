using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<UpdateThePortForwardingRulesForAnMXNetworkRequest>(myJsonResponse);
namespace MerakiDashboard
{
    public class UpdateThePortForwardingRulesForAnMXNetworkRequest
    {
        public List<Rules> Rules { get; set; }
    }
}