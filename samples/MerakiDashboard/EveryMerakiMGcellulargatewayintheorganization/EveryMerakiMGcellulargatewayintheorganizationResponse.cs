using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<List<EveryMerakiMGCellularGatewayInTheOrganizationResponse>>(myJsonResponse);
namespace MerakiDashboard
{
    public class EveryMerakiMGCellularGatewayInTheOrganizationResponse
    {
        public string NetworkId { get; set; }
        public string Serial { get; set; }
        public string Model { get; set; }
        public DateTime LastReportedAt { get; set; }
        public List<Uplinks3> Uplinks { get; set; }
    }

    public class Uplinks3
    {
        public string Interface { get; set; }
        public string Status { get; set; }
        public string Ip { get; set; }
        public string Provider { get; set; }
        public string PublicIp { get; set; }
        public string Model { get; set; }
        public SignalStat SignalStat { get; set; }
        public string ConnectionType { get; set; }
        public string Apn { get; set; }
        public string Gateway { get; set; }
        public string Dns1 { get; set; }
        public string Dns2 { get; set; }
        public string SignalType { get; set; }
        public string Iccid { get; set; }
    }
}