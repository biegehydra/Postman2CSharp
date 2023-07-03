using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<DAMediaServerToBeMonitoredForThisOrganizationRequest>(myJsonResponse);
namespace MerakiDashboard
{
    public class DAMediaServerToBeMonitoredForThisOrganizationRequest
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string BestEffortMonitoringEnabled { get; set; }
    }
}