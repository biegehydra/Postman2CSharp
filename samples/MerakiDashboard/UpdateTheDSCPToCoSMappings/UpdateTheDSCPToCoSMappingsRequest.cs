using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<UpdateTheDSCPToCoSMappingsRequest>(myJsonResponse);
namespace MerakiDashboard
{
    public class UpdateTheDSCPToCoSMappingsRequest
    {
        public List<Mappings2> Mappings { get; set; }
    }

    public class Mappings2
    {
        public string Dscp { get; set; }
        public string Cos { get; set; }
        public string Title { get; set; }
    }
}