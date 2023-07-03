using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<List<FetchOnboardingStatusOfCamerasResponse>>(myJsonResponse);
namespace MerakiDashboard
{
    public class FetchOnboardingStatusOfCamerasResponse
    {
        public string NetworkId { get; set; }
        public string Serial { get; set; }
        public string Status { get; set; }
        public string UpdatedAt { get; set; }
    }
}