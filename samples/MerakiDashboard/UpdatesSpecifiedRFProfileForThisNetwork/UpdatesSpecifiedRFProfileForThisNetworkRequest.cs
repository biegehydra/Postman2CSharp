using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<UpdatesSpecifiedRFProfileForThisNetworkRequest>(myJsonResponse);
namespace MerakiDashboard
{
    public class UpdatesSpecifiedRFProfileForThisNetworkRequest
    {
        public string Name { get; set; }
        public TwoFourGhzSettings TwoFourGhzSettings { get; set; }
        public FiveGhzSettings FiveGhzSettings { get; set; }
        public PerSsidSettings PerSsidSettings { get; set; }
    }
}