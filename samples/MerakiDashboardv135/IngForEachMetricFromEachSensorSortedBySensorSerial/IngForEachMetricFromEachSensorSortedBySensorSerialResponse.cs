using System;
using System.Collections.Generic;
using System.Text.Json;

namespace MerakiDashboard
{
    // Root myDeserializedClass = JsonSerializer.Deserialize<List<IngForEachMetricFromEachSensorSortedBySensorSerialResponse>>(myJsonResponse);
    public class IngForEachMetricFromEachSensorSortedBySensorSerialResponse
    {
        public string Serial { get; set; }
        public ListTheOrganizationsResponse Network { get; set; }
        public List<AdingsFromSensorsInAGivenTimespanSortedByTimestampResponse> Readings { get; set; }
    }
}