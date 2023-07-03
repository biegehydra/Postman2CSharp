using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<List<EryMXInTheOrganizationFromAtLatest2MinutesAgoResponse>>(myJsonResponse);
namespace MerakiDashboard
{
    public class EryMXInTheOrganizationFromAtLatest2MinutesAgoResponse
    {
        public string NetworkId { get; set; }
        public string Serial { get; set; }
        public string Uplink { get; set; }
        public string Ip { get; set; }
        public List<TimeSeries> TimeSeries { get; set; }
    }

    public class TimeSeries
    {
        public DateTime Ts { get; set; }
        public double LossPercent { get; set; }
        public double LatencyMs { get; set; }
    }
}