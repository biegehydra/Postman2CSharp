using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<AlternateManagementInterfaceAndDeviceStaticIPResponse>(myJsonResponse);
namespace MerakiDashboard
{
    public class AlternateManagementInterfaceAndDeviceStaticIPResponse
    {
        public bool Enabled { get; set; }
        public int VlanId { get; set; }
        public List<string> Protocols { get; set; }
        public List<AccessPoints> AccessPoints { get; set; }
    }
}