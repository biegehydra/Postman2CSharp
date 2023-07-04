using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<List<HannelUtilizationForAllBandsSegmentedByDeviceResponse>>(myJsonResponse);
namespace MerakiDashboard
{
    public class HannelUtilizationForAllBandsSegmentedByDeviceResponse
    {
        public DateTime StartTs { get; set; }
        public DateTime EndTs { get; set; }
        public string Serial { get; set; }
        public string Mac { get; set; }
        public Profile3 Network { get; set; }
        public List<ByBand> ByBand { get; set; }
    }
}