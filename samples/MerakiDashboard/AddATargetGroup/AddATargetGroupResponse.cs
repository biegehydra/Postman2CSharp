using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<AddATargetGroupResponse>(myJsonResponse);
namespace MerakiDashboard
{
    public class AddATargetGroupResponse
    {
        public string Name { get; set; }
        public string Scope { get; set; }
        public string Tags { get; set; }
        public string Type { get; set; }
    }
}