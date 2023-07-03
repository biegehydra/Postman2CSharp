using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<UpdateAnOrganizationRequest>(myJsonResponse);
namespace MerakiDashboard
{
    public class UpdateAnOrganizationRequest
    {
        public string Name { get; set; }
        public Management Management { get; set; }
        public Api4 Api { get; set; }
    }

    public class Api4
    {
        public string Enabled { get; set; }
    }
}