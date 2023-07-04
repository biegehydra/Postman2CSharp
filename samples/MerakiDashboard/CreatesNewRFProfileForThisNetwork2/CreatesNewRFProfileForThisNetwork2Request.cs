using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<CreatesNewRFProfileForThisNetwork2Request>(myJsonResponse);
namespace MerakiDashboard
{
    public class CreatesNewRFProfileForThisNetwork2Request
    {
        public string Name { get; set; }
        public string BandSelectionType { get; set; }
        public string ClientBalancingEnabled { get; set; }
        public string MinBitrateType { get; set; }
        public ApBandSettings2 ApBandSettings { get; set; }
        public TwoFourGhzSettings14 TwoFourGhzSettings { get; set; }
        public FiveGhzSettings14 FiveGhzSettings { get; set; }
        public SixGhzSettings2 SixGhzSettings { get; set; }
        public VlanTagging3 Transmission { get; set; }
        public PerSsidSettings PerSsidSettings { get; set; }
    }

    public class FiveGhzSettings14
    {
        public string MaxPower { get; set; }
        public string MinPower { get; set; }
        public string MinBitrate { get; set; }
        public List<string> ValidAutoChannels { get; set; }
        public string ChannelWidth { get; set; }
        public string Rxsop { get; set; }
    }

    public class SixGhzSettings2
    {
        public string MaxPower { get; set; }
        public string MinPower { get; set; }
        public string MinBitrate { get; set; }
        public List<string> ValidAutoChannels { get; set; }
        public string ChannelWidth { get; set; }
        public string Rxsop { get; set; }
    }

    public class TwoFourGhzSettings14
    {
        public string MaxPower { get; set; }
        public string MinPower { get; set; }
        public string MinBitrate { get; set; }
        public List<string> ValidAutoChannels { get; set; }
        public string AxEnabled { get; set; }
        public string Rxsop { get; set; }
    }

    public class ApBandSettings2
    {
        public string BandOperationMode { get; set; }
        public string BandSteeringEnabled { get; set; }
    }
}