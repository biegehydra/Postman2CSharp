using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<UpdatesSpecifiedRFProfileForThisNetwork2Request>(myJsonResponse);
namespace MerakiDashboard
{
    public class UpdatesSpecifiedRFProfileForThisNetwork2Request
    {
        public string Name { get; set; }
        public string ClientBalancingEnabled { get; set; }
        public string MinBitrateType { get; set; }
        public string BandSelectionType { get; set; }
        public ApBandSettings ApBandSettings { get; set; }
        public TwoFourGhzSettings TwoFourGhzSettings { get; set; }
        public FiveGhzSettings FiveGhzSettings { get; set; }
        public SixGhzSettings SixGhzSettings { get; set; }
        public VlanTagging3 Transmission { get; set; }
        public PerSsidSettings PerSsidSettings { get; set; }
    }
}