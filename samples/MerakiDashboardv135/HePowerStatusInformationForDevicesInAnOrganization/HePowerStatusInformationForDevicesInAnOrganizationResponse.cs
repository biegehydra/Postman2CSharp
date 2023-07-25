using System;
using System.Collections.Generic;
using System.Text.Json;

namespace MerakiDashboard
{
    // Root myDeserializedClass = JsonSerializer.Deserialize<List<HePowerStatusInformationForDevicesInAnOrganizationResponse>>(myJsonResponse);
    public class HePowerStatusInformationForDevicesInAnOrganizationResponse
    {
        public string Mac { get; set; }
        public string Name { get; set; }
        public BypassActivationLockAttemptResponse Network { get; set; }
        public string ProductType { get; set; }
        public string Serial { get; set; }
        public List<string> Tags { get; set; }
        public List<Slots> Slots { get; set; }
    }

    public class Slots
    {
        public int Number { get; set; }
        public string Serial { get; set; }
        public string Model { get; set; }
        public string Status { get; set; }
    }
}