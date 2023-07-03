using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<List<ListTheStaticRoutesForAnMXOrTeleworkerNetworkResponse>>(myJsonResponse);
namespace MerakiDashboard
{
    public class ListTheStaticRoutesForAnMXOrTeleworkerNetworkResponse
    {
        public string Id { get; set; }
        public int IpVersion { get; set; }
        public string NetworkId { get; set; }
        public bool Enabled { get; set; }
        public string Name { get; set; }
        public string Subnet { get; set; }
        public string GatewayIp { get; set; }
        public FixedIpAssignments FixedIpAssignments { get; set; }
        public List<ReservedIpRanges> ReservedIpRanges { get; set; }
    }

    public class ReservedIpRanges
    {
        public string Start { get; set; }
        public string End { get; set; }
        public string Comment { get; set; }
    }

    public class _223344556677
    {
        public string Ip { get; set; }
        public string Name { get; set; }
    }

    public class FixedIpAssignments
    {
        [JsonPropertyName("22:33:44:55:66:77")]
        public _223344556677 _223344556677 { get; set; }
    }
}