using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<List<ESeriesOfAverageChannelUtilizationForAllBandsResponse>>(myJsonResponse);
namespace MerakiDashboard
{
    public class ESeriesOfAverageChannelUtilizationForAllBandsResponse
    {
        public DateTime StartTs { get; set; }
        public DateTime EndTs { get; set; }
        public Profile3 Network { get; set; }
        public List<ByBand> ByBand { get; set; }
    }
}