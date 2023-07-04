using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<List<RentUplinkAddressesForDevicesInAnOrganizationResponse>>(myJsonResponse);
namespace MerakiDashboard
{
    public class RentUplinkAddressesForDevicesInAnOrganizationResponse
    {
        public string Mac { get; set; }
        public string Name { get; set; }
        public ResultingNetworks Network { get; set; }
        public string ProductType { get; set; }
        public string Serial { get; set; }
        public List<string> Tags { get; set; }
        public List<Uplinks6> Uplinks { get; set; }
    }

    public class Addresses
    {
        public string Protocol { get; set; }
        public string AssignmentMode { get; set; }
        public string Address { get; set; }
        public string Gateway { get; set; }
        public Ipv6 Public { get; set; }
    }

    public class Uplinks6
    {
        public string Interface { get; set; }
        public List<Addresses> Addresses { get; set; }
    }
}