using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<List<ErStatusInformationForDevicesInAnOrganizationResponse>>(myJsonResponse);
namespace MerakiDashboard
{
    public class ErStatusInformationForDevicesInAnOrganizationResponse
    {
        public string Mac { get; set; }
        public string Name { get; set; }
        public Network Network { get; set; }
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