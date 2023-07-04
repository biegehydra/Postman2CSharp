using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<UpdatesSpecifiedRFProfileForThisNetwork2Response>(myJsonResponse);
namespace MerakiDashboard
{
    public class UpdatesSpecifiedRFProfileForThisNetwork2Response
    {
        public string Id { get; set; }
        public string NetworkId { get; set; }
        public string Name { get; set; }
        public bool ClientBalancingEnabled { get; set; }
        public string MinBitrateType { get; set; }
        public string BandSelectionType { get; set; }
        public _9 ApBandSettings { get; set; }
        public TwoFourGhzSettings TwoFourGhzSettings { get; set; }
        public FiveGhzSettings FiveGhzSettings { get; set; }
        public SixGhzSettings SixGhzSettings { get; set; }
        public Wan2 Transmission { get; set; }
        public PerSsidSettings PerSsidSettings { get; set; }
    }
}