using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<List<LabilityinformationfordevicesinanorganizationResponse>>(myJsonResponse);
namespace MerakiDashboard
{
    public class LabilityinformationfordevicesinanorganizationResponse
    {
        public string Mac { get; set; }
        public string Name { get; set; }
        public Network Network { get; set; }
        public string ProductType { get; set; }
        public string Serial { get; set; }
        public string Status { get; set; }
        public List<string> Tags { get; set; }
    }
}