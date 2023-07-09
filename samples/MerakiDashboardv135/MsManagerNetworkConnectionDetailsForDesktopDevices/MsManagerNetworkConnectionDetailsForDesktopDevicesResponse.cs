using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<List<MsManagerNetworkConnectionDetailsForDesktopDevicesResponse>>(myJsonResponse);
namespace MerakiDashboardv135
{
    public class MsManagerNetworkConnectionDetailsForDesktopDevicesResponse
    {
        public DateTime MeasuredAt { get; set; }
        public string User { get; set; }
        public string NetworkDevice { get; set; }
        public string NetworkDriver { get; set; }
        public string WifiChannel { get; set; }
        public string WifiAuth { get; set; }
        public string WifiBssid { get; set; }
        public string WifiSsid { get; set; }
        public string WifiRssi { get; set; }
        public string WifiNoise { get; set; }
        public string DhcpServer { get; set; }
        public string Ip { get; set; }
        public string NetworkMTU { get; set; }
        public string Subnet { get; set; }
        public string Gateway { get; set; }
        public string PublicIP { get; set; }
        public string DnsServer { get; set; }
        public DateTime Ts { get; set; }
    }
}