using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<CreatesNewRFProfileForThisNetwork2Request>(myJsonResponse);
namespace MerakiDashboardv135
{
    public class CreatesNewRFProfileForThisNetwork2Request
    {
        public string Name { get; set; }
        public string BandSelectionType { get; set; }
        public string ClientBalancingEnabled { get; set; }
        public string MinBitrateType { get; set; }
        public ApBandSettings2 ApBandSettings { get; set; }
        public TwoFourGhzSettings4 TwoFourGhzSettings { get; set; }
        public FiveGhzSettings4 FiveGhzSettings { get; set; }
        public SixGhzSettings2 SixGhzSettings { get; set; }
        public Transmission2 Transmission { get; set; }
        public PerSsidSettings2 PerSsidSettings { get; set; }
        public FlexRadios FlexRadios { get; set; }
    }

    public class FiveGhzSettings4
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

    public class TwoFourGhzSettings4
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

    public class ByModel
    {
        public string Model { get; set; }
        public List<string> Bands { get; set; }
    }

    public class FlexRadios
    {
        public List<ByModel> ByModel { get; set; }
    }

    public class Transmission2
    {
        public string Enabled { get; set; }
    }
}