using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<List<EseriesofaveragechannelutilizationforallbandsResponse>>(myJsonResponse);
namespace MerakiDashboard
{
    public class EseriesofaveragechannelutilizationforallbandsResponse
    {
        public DateTime StartTs { get; set; }
        public DateTime EndTs { get; set; }
        public Network Network { get; set; }
        public List<ByBand> ByBand { get; set; }
    }
}