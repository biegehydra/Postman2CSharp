using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<List<AcrossallbandsforallnetworksintheorganizationResponse>>(myJsonResponse);
namespace MerakiDashboard
{
    public class AcrossallbandsforallnetworksintheorganizationResponse
    {
        public Network Network { get; set; }
        public List<ByBand> ByBand { get; set; }
    }
}