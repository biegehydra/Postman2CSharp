using System;
using System.Collections.Generic;
using System.Text.Json;

namespace MerakiDashboard
{
    // Root myDeserializedClass = JsonSerializer.Deserialize<ShowTheLANSettingsOfAMGResponse>(myJsonResponse);
    public class ShowTheLANSettingsOfAMGResponse
    {
        public string DeviceName { get; set; }
        public string DeviceLanIp { get; set; }
        public string DeviceSubnet { get; set; }
        public List<FixedIpAssignments2> FixedIpAssignments { get; set; }
        public List<ReservedIpRanges> ReservedIpRanges { get; set; }
    }

    public class FixedIpAssignments2
    {
        public string Mac { get; set; }
        public string Name { get; set; }
        public string Ip { get; set; }
    }
}