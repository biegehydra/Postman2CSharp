using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<RAllDiscoveredDevicesAndConnectionsInANetworkResponse>(myJsonResponse);
namespace MerakiDashboard
{
    public class RAllDiscoveredDevicesAndConnectionsInANetworkResponse
    {
        public List<Nodes> Nodes { get; set; }
        public List<Links> Links { get; set; }
        public List<object> Errors { get; set; }
    }

    public class Device4
    {
        public string Serial { get; set; }
        public string Name { get; set; }
        public string Product { get; set; }
        public string Model { get; set; }
        public string Status { get; set; }
        public DateTime LastReportedAt { get; set; }
        public Clients Clients { get; set; }
        public Switch Switch { get; set; }
        public List<Uplinks4> Uplinks { get; set; }
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
        public Device Device { get; set; }
    }

    public class Lldp3
    {
        public string ChassisId { get; set; }
        public string SystemName { get; set; }
        public string SystemDescription { get; set; }
        public List<string> SystemCapabilities { get; set; }
        public string ManagementAddress { get; set; }
    }

    public class Members
    {
        public string DerivedId { get; set; }
        public string Mac { get; set; }
        public string Type { get; set; }
        public Device4 Device { get; set; }
    }

    public class Stack
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Members> Members { get; set; }
        public Clients Clients { get; set; }
    }

    public class Ends
    {
        public Node Node { get; set; }
        public Device Device { get; set; }
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

    public class Node
    {
        public string DerivedId { get; set; }
        public string Type { get; set; }
    }

    public class Uplinks4
    {
        public int VlanId { get; set; }
        public Pppoe Pppoe { get; set; }
    }

    public class ByStatus
    {
        public int Active { get; set; }
    }

    public class Clients
    {
        public Counts Counts { get; set; }
    }

    public class Ports3
    {
        public Counts Counts { get; set; }
    }

    public class Switch
    {
        public Ports3 Ports { get; set; }
    }
}