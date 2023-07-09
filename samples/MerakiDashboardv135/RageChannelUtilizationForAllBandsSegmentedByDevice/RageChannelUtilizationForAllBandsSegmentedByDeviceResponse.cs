using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<List<RageChannelUtilizationForAllBandsSegmentedByDeviceResponse>>(myJsonResponse);
namespace MerakiDashboardv135
{
    public class RageChannelUtilizationForAllBandsSegmentedByDeviceResponse
    {
        public DateTime StartTs { get; set; }
        public DateTime EndTs { get; set; }
        public string Serial { get; set; }
        public string Mac { get; set; }
        public Network Network { get; set; }
        public List<ByBand> ByBand { get; set; }
    }
}