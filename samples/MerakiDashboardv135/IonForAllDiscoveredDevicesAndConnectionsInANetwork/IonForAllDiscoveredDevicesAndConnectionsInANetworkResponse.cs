using System;
using System.Collections.Generic;
using System.Text.Json;

namespace MerakiDashboard
{
    // Root myDeserializedClass = JsonSerializer.Deserialize<IonForAllDiscoveredDevicesAndConnectionsInANetworkResponse>(myJsonResponse);
    public class IonForAllDiscoveredDevicesAndConnectionsInANetworkResponse
    {
        public List<Nodes> Nodes { get; set; }
        public List<Links> Links { get; set; }
        public List<object> Errors { get; set; }
    }

    public class Device3
    {
        public string Serial { get; set; }
        public string Name { get; set; }
        public string Product { get; set; }
        public string Model { get; set; }
        public string Status { get; set; }
        public DateTime LastReportedAt { get; set; }
        public NAnOverviewOfAlertOccurrencesOverATimespanByMetricResponse Clients { get; set; }
        public ListLLDPAndCDPInformationForADeviceResponse Switch { get; set; }
        public List<Uplinks3> Uplinks { get; set; }
        public string ProductType { get; set; }
    }

    public class Nodes
    {
        public string DerivedId { get; set; }
        public string Mac { get; set; }
        public string Type { get; set; }
        public bool Root { get; set; }
        public Discovered Discovered { get; set; }
        public Stack Stack { get; set; }
        public Device3 Device { get; set; }
    }

    public class Lldp3
    {
        public string ChassisId { get; set; }
        public string SystemName { get; set; }
        public string SystemDescription { get; set; }
        public List<string> SystemCapabilities { get; set; }
        public string ManagementAddress { get; set; }
        public string PortId { get; set; }
        public string PortDescription { get; set; }
    }

    public class Members
    {
        public string DerivedId { get; set; }
        public string Mac { get; set; }
        public string Type { get; set; }
        public Device3 Device { get; set; }
    }

    public class Stack
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Members> Members { get; set; }
        public NAnOverviewOfAlertOccurrencesOverATimespanByMetricResponse Clients { get; set; }
    }

    public class Ends
    {
        public Members Node { get; set; }
        public Device3 Device { get; set; }
        public Discovered Discovered { get; set; }
    }

    public class Discovered
    {
        public Lldp3 Lldp { get; set; }
        public string Cdp { get; set; }
    }

    public class Links
    {
        public List<Ends> Ends { get; set; }
        public DateTime LastReportedAt { get; set; }
    }

    public class Uplinks3
    {
        public int VlanId { get; set; }
        public Aggregation Pppoe { get; set; }
    }

    public class ByStatus
    {
        public int Active { get; set; }
    }
}