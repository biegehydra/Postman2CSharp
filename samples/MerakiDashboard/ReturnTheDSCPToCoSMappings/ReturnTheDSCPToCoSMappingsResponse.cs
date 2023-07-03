using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<ReturnTheDSCPToCoSMappingsResponse>(myJsonResponse);
namespace MerakiDashboard
{
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