using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<ListLLDPAndCDPInformationForADeviceResponse>(myJsonResponse);
namespace MerakiDashboard
{
    public class ListLLDPAndCDPInformationForADeviceResponse
    {
        public string SourceMac { get; set; }
        public PerSsidSettings2 Ports { get; set; }
    }

    public class Cdp2
    {
        public string DeviceId { get; set; }
        public string PortId { get; set; }
        public string Address { get; set; }
        public string SourcePort { get; set; }
    }

    public class Lldp2
    {
        public string SystemName { get; set; }
        public string PortId { get; set; }
        public string ManagementAddress { get; set; }
        public string SourcePort { get; set; }
    }
}