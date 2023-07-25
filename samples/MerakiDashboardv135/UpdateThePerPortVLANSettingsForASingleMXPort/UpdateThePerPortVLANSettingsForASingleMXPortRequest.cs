using System;
using System.Collections.Generic;
using System.Text.Json;

namespace MerakiDashboard
{
    // Root myDeserializedClass = JsonSerializer.Deserialize<UpdateThePerPortVLANSettingsForASingleMXPortRequest>(myJsonResponse);
    public class UpdateThePerPortVLANSettingsForASingleMXPortRequest
    {
        public string Enabled { get; set; }
        public string DropUntaggedTraffic { get; set; }
        public string Type { get; set; }
        public string Vlan { get; set; }
        public string AllowedVlans { get; set; }
        public string AccessPolicy { get; set; }
    }
}