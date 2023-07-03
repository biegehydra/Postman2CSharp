using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<EturnAMonitoredMediaServerForThisOrganizationResponse>(myJsonResponse);
namespace MerakiDashboard
{
    public class EturnAMonitoredMediaServerForThisOrganizationResponse
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public bool BestEffortMonitoringEnabled { get; set; }
    }
}