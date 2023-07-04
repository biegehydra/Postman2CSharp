using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<UpdatesThePortForwardingRulesForASingleMGRequest>(myJsonResponse);
namespace MerakiDashboard
{
    public class UpdatesThePortForwardingRulesForASingleMGRequest
    {
        public List<Rules> Rules { get; set; }
    }
}