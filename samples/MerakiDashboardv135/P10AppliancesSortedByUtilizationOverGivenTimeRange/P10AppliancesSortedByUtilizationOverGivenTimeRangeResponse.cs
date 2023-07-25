using System;
using System.Collections.Generic;
using System.Text.Json;

namespace MerakiDashboard
{
    // Root myDeserializedClass = JsonSerializer.Deserialize<List<P10AppliancesSortedByUtilizationOverGivenTimeRangeResponse>>(myJsonResponse);
    public class P10AppliancesSortedByUtilizationOverGivenTimeRangeResponse
    {
        public ListSwitchPortSchedulesResponse Network { get; set; }
        public string Name { get; set; }
        public string Mac { get; set; }
        public string Serial { get; set; }
        public string Model { get; set; }
        public Utilization Utilization { get; set; }
    }

    public class Utilization
    {
        public NonWifi Average { get; set; }
    }
}