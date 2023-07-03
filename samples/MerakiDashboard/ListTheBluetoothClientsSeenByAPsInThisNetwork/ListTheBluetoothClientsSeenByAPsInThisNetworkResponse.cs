using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<List<ListTheBluetoothClientsSeenByAPsInThisNetworkResponse>>(myJsonResponse);
namespace MerakiDashboard
{
    public class ListTheBluetoothClientsSeenByAPsInThisNetworkResponse
    {
        public string Id { get; set; }
        public string Mac { get; set; }
        public string NetworkId { get; set; }
        public string Name { get; set; }
        public string DeviceName { get; set; }
        public string Manufacturer { get; set; }
        public int LastSeen { get; set; }
        public string SeenByDeviceMac { get; set; }
        public bool InSightAlert { get; set; }
        public bool OutOfSightAlert { get; set; }
        public List<string> Tags { get; set; }
    }
}