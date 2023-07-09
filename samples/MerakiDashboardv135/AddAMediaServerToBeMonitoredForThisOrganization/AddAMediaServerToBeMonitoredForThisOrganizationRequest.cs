using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<AddAMediaServerToBeMonitoredForThisOrganizationRequest>(myJsonResponse);
namespace MerakiDashboardv135
{
    public class AddAMediaServerToBeMonitoredForThisOrganizationRequest
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string BestEffortMonitoringEnabled { get; set; }
    }
}