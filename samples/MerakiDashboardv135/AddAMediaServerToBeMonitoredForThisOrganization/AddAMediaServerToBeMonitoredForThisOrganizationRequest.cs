using System;
using System.Collections.Generic;
using System.Text.Json;

namespace MerakiDashboard
{
    // Root myDeserializedClass = JsonSerializer.Deserialize<AddAMediaServerToBeMonitoredForThisOrganizationRequest>(myJsonResponse);
    public class AddAMediaServerToBeMonitoredForThisOrganizationRequest
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string BestEffortMonitoringEnabled { get; set; }
    }
}