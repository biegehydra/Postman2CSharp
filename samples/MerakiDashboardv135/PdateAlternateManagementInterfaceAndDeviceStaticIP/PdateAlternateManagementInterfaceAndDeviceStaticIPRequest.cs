using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<PdateAlternateManagementInterfaceAndDeviceStaticIPRequest>(myJsonResponse);
namespace MerakiDashboardv135
{
    public class PdateAlternateManagementInterfaceAndDeviceStaticIPRequest
    {
        public string Enabled { get; set; }
        public string VlanId { get; set; }
        public List<string> Protocols { get; set; }
        public List<AccessPoints> AccessPoints { get; set; }
    }
}