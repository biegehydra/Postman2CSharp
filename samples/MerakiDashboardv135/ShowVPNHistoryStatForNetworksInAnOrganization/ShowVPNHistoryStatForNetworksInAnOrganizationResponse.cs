using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<List<ShowVPNHistoryStatForNetworksInAnOrganizationResponse>>(myJsonResponse);
namespace MerakiDashboardv135
{
    public class ShowVPNHistoryStatForNetworksInAnOrganizationResponse
    {
        public string NetworkId { get; set; }
        public string NetworkName { get; set; }
        public List<MerakiVpnPeers> MerakiVpnPeers { get; set; }
    }

    public class MerakiVpnPeers
    {
        public string NetworkId { get; set; }
        public string NetworkName { get; set; }
        public UsageSummary UsageSummary { get; set; }
        public List<LatencySummaries> LatencySummaries { get; set; }
        public List<LossPercentageSummaries> LossPercentageSummaries { get; set; }
        public List<JitterSummaries> JitterSummaries { get; set; }
        public List<MosSummaries> MosSummaries { get; set; }
    }

    public class JitterSummaries
    {
        public string SenderUplink { get; set; }
        public string ReceiverUplink { get; set; }
        public double AvgJitter { get; set; }
        public int MinJitter { get; set; }
        public double MaxJitter { get; set; }
    }

    public class LatencySummaries
    {
        public string SenderUplink { get; set; }
        public string ReceiverUplink { get; set; }
        public int AvgLatencyMs { get; set; }
        public int MinLatencyMs { get; set; }
        public int MaxLatencyMs { get; set; }
    }

    public class LossPercentageSummaries
    {
        public string SenderUplink { get; set; }
        public string ReceiverUplink { get; set; }
        public int AvgLossPercentage { get; set; }
        public int MinLossPercentage { get; set; }
        public double MaxLossPercentage { get; set; }
    }

    public class MosSummaries
    {
        public string SenderUplink { get; set; }
        public string ReceiverUplink { get; set; }
        public double AvgMos { get; set; }
        public int MinMos { get; set; }
        public double MaxMos { get; set; }
    }

    public class UsageSummary
    {
        public int ReceivedInKilobytes { get; set; }
        public int SentInKilobytes { get; set; }
    }
}