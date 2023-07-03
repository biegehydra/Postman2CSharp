using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<UpdateTheUplinkSettingsForAnMXApplianceRequest>(myJsonResponse);
namespace MerakiDashboard
{
    public class UpdateTheUplinkSettingsForAnMXApplianceRequest
    {
        public Interfaces Interfaces { get; set; }
    }

    public class Wan12
    {
        public string Enabled { get; set; }
        public VlanTagging3 VlanTagging { get; set; }
        public Svis Svis { get; set; }
        public Pppoe3 Pppoe { get; set; }
    }

    public class Wan22
    {
        public string Enabled { get; set; }
        public VlanTagging VlanTagging { get; set; }
        public Svis Svis { get; set; }
        public Pppoe Pppoe { get; set; }
    }

    public class Authentication4
    {
        public string Enabled { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class Pppoe3
    {
        public string Enabled { get; set; }
        public Authentication4 Authentication { get; set; }
    }

    public class VlanTagging3
    {
        public string Enabled { get; set; }
        public string VlanId { get; set; }
    }
}