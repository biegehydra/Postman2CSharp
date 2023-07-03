using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<UpdateTheDSCPToCoSMappingsResponse>(myJsonResponse);
namespace MerakiDashboard
{
    public class UpdateTheDSCPToCoSMappingsResponse
    {
        public List<Mappings> Mappings { get; set; }
    }
}