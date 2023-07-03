using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<List<ReturnTheStatusForAllThePortsOfASwitchResponse>>(myJsonResponse);
namespace MerakiDashboard
{
    public class ReturnTheStatusForAllThePortsOfASwitchResponse
    {
        public string PortId { get; set; }
        public bool Enabled { get; set; }
        public string Status { get; set; }
        public bool IsUplink { get; set; }
        public List<string> Errors { get; set; }
        public List<string> Warnings { get; set; }
        public string Speed { get; set; }
        public string Duplex { get; set; }
        public UsageInKb UsageInKb { get; set; }
        public Cdp Cdp { get; set; }
        public Lldp Lldp { get; set; }
        public int ClientCount { get; set; }
        public double PowerUsageInWh { get; set; }
        public TrafficInKbps TrafficInKbps { get; set; }
        public SecurePort SecurePort { get; set; }
    }

    public class Cdp
    {
        public string SystemName { get; set; }
        public string Platform { get; set; }
        public string DeviceId { get; set; }
        public string PortId { get; set; }
        public int NativeVlan { get; set; }
        public string Address { get; set; }
        public string ManagementAddress { get; set; }
        public string Version { get; set; }
        public string VtpManagementDomain { get; set; }
        public string Capabilities { get; set; }
    }

    public class Lldp
    {
        public string SystemName { get; set; }
        public string SystemDescription { get; set; }
        public string ChassisId { get; set; }
        public string PortId { get; set; }
        public int ManagementVlan { get; set; }
        public int PortVlan { get; set; }
        public string ManagementAddress { get; set; }
        public string PortDescription { get; set; }
        public string SystemCapabilities { get; set; }
    }

    public class ConfigOverrides
    {
        public string Type { get; set; }
        public int Vlan { get; set; }
        public int VoiceVlan { get; set; }
        public string AllowedVlans { get; set; }
    }

    public class SecurePort
    {
        public bool Enabled { get; set; }
        public bool Active { get; set; }
        public string AuthenticationStatus { get; set; }
        public ConfigOverrides ConfigOverrides { get; set; }
    }

    public class TrafficInKbps
    {
        public double Total { get; set; }
        public double Sent { get; set; }
        public int Recv { get; set; }
    }

    public class UsageInKb
    {
        public int Total { get; set; }
        public int Sent { get; set; }
        public int Recv { get; set; }
    }
}