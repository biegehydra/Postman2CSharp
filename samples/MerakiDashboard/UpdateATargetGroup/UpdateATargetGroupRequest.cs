using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<UpdateATargetGroupRequest>(myJsonResponse);
namespace MerakiDashboard
{
    public class UpdateATargetGroupRequest
    {
        public string Name { get; set; }
        public string Scope { get; set; }
    }
}