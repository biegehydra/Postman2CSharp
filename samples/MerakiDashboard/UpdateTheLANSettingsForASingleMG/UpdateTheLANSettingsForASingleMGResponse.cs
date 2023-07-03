using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<UpdateTheLANSettingsForASingleMGResponse>(myJsonResponse);
namespace MerakiDashboard
{
    public class UpdateTheLANSettingsForASingleMGResponse
    {
        public string DeviceName { get; set; }
        public string DeviceLanIp { get; set; }
        public string DeviceSubnet { get; set; }
        public List<FixedIpAssignments> FixedIpAssignments { get; set; }
        public List<ReservedIpRanges> ReservedIpRanges { get; set; }
    }
}