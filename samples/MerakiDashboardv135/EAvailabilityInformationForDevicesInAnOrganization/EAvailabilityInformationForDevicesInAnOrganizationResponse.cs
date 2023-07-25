using System;
using System.Collections.Generic;
using System.Text.Json;

namespace MerakiDashboard
{
    // Root myDeserializedClass = JsonSerializer.Deserialize<List<EAvailabilityInformationForDevicesInAnOrganizationResponse>>(myJsonResponse);
    public class EAvailabilityInformationForDevicesInAnOrganizationResponse
    {
        public string Mac { get; set; }
        public string Name { get; set; }
        public BypassActivationLockAttemptResponse Network { get; set; }
        public string ProductType { get; set; }
        public string Serial { get; set; }
        public string Status { get; set; }
        public List<string> Tags { get; set; }
    }
}