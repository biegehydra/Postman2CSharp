using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<UpdateAHubBGPConfigurationRequest>(myJsonResponse);
namespace MerakiDashboardv135
{
    public class UpdateAHubBGPConfigurationRequest
    {
        public string Enabled { get; set; }
        public string AsNumber { get; set; }
        public string IbgpHoldTimer { get; set; }
        public List<Neighbors2> Neighbors { get; set; }
    }

    public class Neighbors2
    {
        public string RemoteAsNumber { get; set; }
        public string EbgpHoldTimer { get; set; }
        public string EbgpMultihop { get; set; }
        public string Ip { get; set; }
        public Ipv7 Ipv6 { get; set; }
        public string ReceiveLimit { get; set; }
        public string AllowTransit { get; set; }
        public string SourceInterface { get; set; }
        public string NextHopIp { get; set; }
        public TtlSecurity TtlSecurity { get; set; }
        public Authentication3 Authentication { get; set; }
    }

    public class Authentication3
    {
        public string Password { get; set; }
    }

    public class TtlSecurity
    {
        public string Enabled { get; set; }
    }
}