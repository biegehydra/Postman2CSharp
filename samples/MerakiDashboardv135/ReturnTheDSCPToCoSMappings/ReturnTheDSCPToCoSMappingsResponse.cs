using System;
using System.Collections.Generic;
using System.Text.Json;

namespace MerakiDashboard
{
    // Root myDeserializedClass = JsonSerializer.Deserialize<ReturnTheDSCPToCoSMappingsResponse>(myJsonResponse);
    public class ReturnTheDSCPToCoSMappingsResponse
    {
        public List<Mappings> Mappings { get; set; }
    }

    public class Mappings
    {
        public int Dscp { get; set; }
        public int Cos { get; set; }
        public string Title { get; set; }
    }
}