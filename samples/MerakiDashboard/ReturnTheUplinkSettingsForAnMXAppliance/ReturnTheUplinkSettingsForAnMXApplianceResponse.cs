using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<ReturnTheUplinkSettingsForAnMXApplianceResponse>(myJsonResponse);
namespace MerakiDashboard
{
    public class ReturnTheUplinkSettingsForAnMXApplianceResponse
    {
        public Interfaces Interfaces { get; set; }
    }

    public class Ipv4
    {
        public string AssignmentMode { get; set; }
        public string Address { get; set; }
        public string Gateway { get; set; }
        public Nameservers Nameservers { get; set; }
    }

    public class Ipv6
    {
        public string AssignmentMode { get; set; }
        public string Address { get; set; }
        public string Gateway { get; set; }
        public Nameservers Nameservers { get; set; }
    }

    public class Wan1
    {
        public bool Enabled { get; set; }
        public VlanTagging VlanTagging { get; set; }
        public Svis Svis { get; set; }
        public Pppoe Pppoe { get; set; }
    }

    public class Wan2
    {
        public bool Enabled { get; set; }
        public VlanTagging VlanTagging { get; set; }
        public Svis Svis { get; set; }
        public Pppoe Pppoe { get; set; }
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

    public class Pppoe
    {
        public bool Enabled { get; set; }
        public Authentication2 Authentication { get; set; }
    }

    public class Svis
    {
        public Ipv4 Ipv4 { get; set; }
        public Ipv6 Ipv6 { get; set; }
    }

    public class VlanTagging
    {
        public bool Enabled { get; set; }
        public int VlanId { get; set; }
    }

    public class Nameservers
    {
        public List<string> Addresses { get; set; }
    }
}