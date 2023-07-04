using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<CreateALinkAggregationGroupResponse>(myJsonResponse);
namespace MerakiDashboard
{
    public class CreateALinkAggregationGroupResponse
    {
        public string Id { get; set; }
        public List<SwitchPorts> SwitchPorts { get; set; }
    }
}