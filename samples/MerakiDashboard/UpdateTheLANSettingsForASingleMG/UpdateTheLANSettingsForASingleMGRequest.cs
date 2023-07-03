using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<UpdateTheLANSettingsForASingleMGRequest>(myJsonResponse);
namespace MerakiDashboard
{
    public class UpdateTheLANSettingsForASingleMGRequest
    {
        public List<ReservedIpRanges> ReservedIpRanges { get; set; }
        public List<FixedIpAssignments> FixedIpAssignments { get; set; }
    }
}