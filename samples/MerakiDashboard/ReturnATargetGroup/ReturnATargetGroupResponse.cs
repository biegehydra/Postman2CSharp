using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<ReturnATargetGroupResponse>(myJsonResponse);
namespace MerakiDashboard
{
    public class ReturnATargetGroupResponse
    {
        public string Name { get; set; }
        public string Scope { get; set; }
        public string Tags { get; set; }
        public string Type { get; set; }
    }
}