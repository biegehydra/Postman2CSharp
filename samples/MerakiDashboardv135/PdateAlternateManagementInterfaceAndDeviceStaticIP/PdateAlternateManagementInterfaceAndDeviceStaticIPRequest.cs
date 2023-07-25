using System;
using System.Collections.Generic;
using System.Text.Json;

namespace MerakiDashboard
{
    // Root myDeserializedClass = JsonSerializer.Deserialize<PdateAlternateManagementInterfaceAndDeviceStaticIPRequest>(myJsonResponse);
    public class PdateAlternateManagementInterfaceAndDeviceStaticIPRequest
    {
        public string Enabled { get; set; }
        public string VlanId { get; set; }
        public List<string> Protocols { get; set; }
        public List<AccessPoints> AccessPoints { get; set; }
    }
}