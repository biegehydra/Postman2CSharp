using System;
using System.Collections.Generic;
using System.Text.Json;

namespace MerakiDashboard
{
    // Root myDeserializedClass = JsonSerializer.Deserialize<List<ListTheMRSSIDsInANetworkResponse>>(myJsonResponse);
    public class ListTheMRSSIDsInANetworkResponse
    {
        public int Number { get; set; }
        public string Name { get; set; }
        public bool Enabled { get; set; }
        public string SplashPage { get; set; }
        public bool SsidAdminAccessible { get; set; }
        public string AuthMode { get; set; }
        public string EncryptionMode { get; set; }
        public string WpaEncryptionMode { get; set; }
        public List<RadiusAccountingServers> RadiusServers { get; set; }
        public List<RadiusAccountingServers> RadiusAccountingServers { get; set; }
        public bool RadiusAccountingEnabled { get; set; }
        public bool RadiusEnabled { get; set; }
        public string RadiusAttributeForGroupPolicies { get; set; }
        public string RadiusFailoverPolicy { get; set; }
        public string RadiusLoadBalancingPolicy { get; set; }
        public string IpAssignmentMode { get; set; }
        public string AdminSplashUrl { get; set; }
        public string SplashTimeout { get; set; }
        public bool WalledGardenEnabled { get; set; }
        public List<string> WalledGardenRanges { get; set; }
        public int MinBitrate { get; set; }
        public string BandSelection { get; set; }
        public int PerClientBandwidthLimitUp { get; set; }
        public int PerClientBandwidthLimitDown { get; set; }
        public bool Visible { get; set; }
        public bool AvailableOnAllAps { get; set; }
        public List<string> AvailabilityTags { get; set; }
        public int PerSsidBandwidthLimitUp { get; set; }
        public int PerSsidBandwidthLimitDown { get; set; }
        public bool MandatoryDhcpEnabled { get; set; }
    }

    public class RadiusAccountingServers
    {
        public string Host { get; set; }
        public int Port { get; set; }
        public int OpenRoamingCertificateId { get; set; }
        public string CaCertificate { get; set; }
    }
}