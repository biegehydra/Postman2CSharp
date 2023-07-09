using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<ReturnTheUplinkSettingsForAnMXApplianceResponse>(myJsonResponse);
namespace MerakiDashboardv135
{
    public class ReturnTheUplinkSettingsForAnMXApplianceResponse
    {
        public Interfaces Interfaces { get; set; }
    }

    public class Ipv5
    {
        public string AssignmentMode { get; set; }
        public string Address { get; set; }
        public string Gateway { get; set; }
        public Nameservers2 Nameservers { get; set; }
    }

    public class Ipv7
    {
        public string AssignmentMode { get; set; }
        public string Address { get; set; }
        public string Gateway { get; set; }
        public Nameservers2 Nameservers { get; set; }
    }

    public class Wan1
    {
        public bool Enabled { get; set; }
        public VlanTagging2 VlanTagging { get; set; }
        public Svis2 Svis { get; set; }
        public Pppoe2 Pppoe { get; set; }
    }

    public class Wan2
    {
        public bool Enabled { get; set; }
        public VlanTagging2 VlanTagging { get; set; }
        public Svis2 Svis { get; set; }
        public Pppoe2 Pppoe { get; set; }
    }

    public class Authentication2
    {
        public bool Enabled { get; set; }
        public string Username { get; set; }
    }

    public class Interfaces
    {
        public Wan1 Wan1 { get; set; }
        public Wan2 Wan2 { get; set; }
    }

    public class Pppoe2
    {
        public bool Enabled { get; set; }
        public Authentication2 Authentication { get; set; }
    }

    public class Svis2
    {
        public Ipv5 Ipv4 { get; set; }
        public Ipv7 Ipv6 { get; set; }
    }

    public class VlanTagging2
    {
        public bool Enabled { get; set; }
        public int VlanId { get; set; }
    }

    public class Nameservers2
    {
        public List<string> Addresses { get; set; }
    }
}