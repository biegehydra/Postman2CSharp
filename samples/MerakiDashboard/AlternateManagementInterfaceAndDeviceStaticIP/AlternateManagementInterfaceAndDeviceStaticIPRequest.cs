using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<AlternateManagementInterfaceAndDeviceStaticIPRequest>(myJsonResponse);
namespace MerakiDashboard
{
    public class AlternateManagementInterfaceAndDeviceStaticIPRequest
    {
        public string Enabled { get; set; }
        public string VlanId { get; set; }
        public List<string> Protocols { get; set; }
        public List<AccessPoints> AccessPoints { get; set; }
    }
}