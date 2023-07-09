using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<IonForAllDiscoveredDevicesAndConnectionsInANetworkResponse>(myJsonResponse);
namespace MerakiDashboardv135
{
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
        public Clients2 Clients { get; set; }
        public Switch Switch { get; set; }
        public List<Uplinks2> Uplinks { get; set; }
        public string ProductType { get; set; }
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

    public class Nodes
    {
        public string DerivedId { get; set; }
        public string Mac { get; set; }
        public string Type { get; set; }
        public bool Root { get; set; }
        public Discovered2 Discovered { get; set; }
        public Stack Stack { get; set; }
        public Device3 Device { get; set; }
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
        public Clients2 Clients { get; set; }
    }

    public class Ends
    {
        public Node Node { get; set; }
        public Device3 Device { get; set; }
        public Discovered2 Discovered { get; set; }
    }

    public class Counts5
    {
        public int Total { get; set; }
        public ByStatus ByStatus { get; set; }
    }

    public class Discovered2
    {
        public Lldp3 Lldp { get; set; }
        public string Cdp { get; set; }
    }

    public class Links
    {
        public List<Ends> Ends { get; set; }
        public DateTime LastReportedAt { get; set; }
    }

    public class Node
    {
        public string DerivedId { get; set; }
        public string Type { get; set; }
    }

    public class Uplinks2
    {
        public int VlanId { get; set; }
        public Pppoe2 Pppoe { get; set; }
    }

    public class ByStatus
    {
        public int Active { get; set; }
    }

    public class Clients2
    {
        public Counts5 Counts { get; set; }
    }

    public class Ports3
    {
        public Counts5 Counts { get; set; }
    }

    public class Switch
    {
        public Ports3 Ports { get; set; }
    }
}