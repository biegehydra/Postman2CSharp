using System;
using System.Collections.Generic;
using System.Text.Json;

namespace MerakiDashboard
{
    // Root myDeserializedClass = JsonSerializer.Deserialize<UpdateTheAttributesOfAnMXSSIDRequest>(myJsonResponse);
    public class UpdateTheAttributesOfAnMXSSIDRequest
    {
        public string Name { get; set; }
        public string Enabled { get; set; }
        public string DefaultVlanId { get; set; }
        public string AuthMode { get; set; }
        public string Psk { get; set; }
        public List<RadiusServers2> RadiusServers { get; set; }
        public string EncryptionMode { get; set; }
        public string WpaEncryptionMode { get; set; }
        public string Visible { get; set; }
        public Dot11w DhcpEnforcedDeauthentication { get; set; }
        public Dot11w Dot11w { get; set; }
    }

    public class RadiusServers2
    {
        public string Host { get; set; }
        public string Port { get; set; }
        public string Secret { get; set; }
    }

    public class Dot11w
    {
        public string Enabled { get; set; }
        public string Required { get; set; }
    }
}