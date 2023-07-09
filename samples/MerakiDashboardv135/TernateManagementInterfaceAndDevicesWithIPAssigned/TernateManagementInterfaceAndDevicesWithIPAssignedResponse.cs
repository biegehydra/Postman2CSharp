using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<TernateManagementInterfaceAndDevicesWithIPAssignedResponse>(myJsonResponse);
namespace MerakiDashboardv135
{
    public class TernateManagementInterfaceAndDevicesWithIPAssignedResponse
    {
        public bool Enabled { get; set; }
        public int VlanId { get; set; }
        public List<string> Protocols { get; set; }
        public List<AccessPoints> AccessPoints { get; set; }
    }

    public class AccessPoints
    {
        public string Serial { get; set; }
        public string AlternateManagementIp { get; set; }
        public string SubnetMask { get; set; }
        public string Gateway { get; set; }
        public string Dns1 { get; set; }
        public string Dns2 { get; set; }
    }
}