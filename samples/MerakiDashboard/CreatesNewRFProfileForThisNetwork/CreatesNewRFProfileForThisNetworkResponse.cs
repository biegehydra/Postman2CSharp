using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<CreatesNewRFProfileForThisNetworkResponse>(myJsonResponse);
namespace MerakiDashboard
{
    public class CreatesNewRFProfileForThisNetworkResponse
    {
        public string Id { get; set; }
        public string NetworkId { get; set; }
        public string Name { get; set; }
        public TwoFourGhzSettings TwoFourGhzSettings { get; set; }
        public FiveGhzSettings FiveGhzSettings { get; set; }
        public PerSsidSettings PerSsidSettings { get; set; }
    }
}