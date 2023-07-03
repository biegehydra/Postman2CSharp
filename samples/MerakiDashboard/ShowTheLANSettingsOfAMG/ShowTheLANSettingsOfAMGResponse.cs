using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<ShowTheLANSettingsOfAMGResponse>(myJsonResponse);
namespace MerakiDashboard
{
    public class ShowTheLANSettingsOfAMGResponse
    {
        public string DeviceName { get; set; }
        public string DeviceLanIp { get; set; }
        public string DeviceSubnet { get; set; }
        public List<FixedIpAssignments6> FixedIpAssignments { get; set; }
        public List<ReservedIpRanges> ReservedIpRanges { get; set; }
    }

    public class FixedIpAssignments6
    {
        public string Mac { get; set; }
        public string Name { get; set; }
        public string Ip { get; set; }
    }
}