using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<UpdatesThePortForwardingRulesForASingleMGResponse>(myJsonResponse);
namespace MerakiDashboard
{
    public class UpdatesThePortForwardingRulesForASingleMGResponse
    {
        public List<Rules> Rules { get; set; }
    }
}