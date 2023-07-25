using System;
using System.Collections.Generic;
using System.Text.Json;

namespace MerakiDashboard
{
    // Root myDeserializedClass = JsonSerializer.Deserialize<ReturnTheRadioSettingsOfAnApplianceResponse>(myJsonResponse);
    public class ReturnTheRadioSettingsOfAnApplianceResponse
    {
        public string Serial { get; set; }
        public string RfProfileId { get; set; }
        public FiveGhzSettings TwoFourGhzSettings { get; set; }
        public FiveGhzSettings FiveGhzSettings { get; set; }
    }

    public class FiveGhzSettings
    {
        public int Channel { get; set; }
        public int ChannelWidth { get; set; }
        public int TargetPower { get; set; }
    }
}