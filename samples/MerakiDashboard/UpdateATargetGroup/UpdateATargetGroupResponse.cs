using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<UpdateATargetGroupResponse>(myJsonResponse);
namespace MerakiDashboard
{
    public class UpdateATargetGroupResponse
    {
        public string Name { get; set; }
        public string Scope { get; set; }
        public string Tags { get; set; }
        public string Type { get; set; }
    }
}