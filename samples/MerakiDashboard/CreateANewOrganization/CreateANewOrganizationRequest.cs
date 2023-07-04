using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<CreateANewOrganizationRequest>(myJsonResponse);
namespace MerakiDashboard
{
    public class CreateANewOrganizationRequest
    {
        public string Name { get; set; }
        public Management Management { get; set; }
    }

    public class Management
    {
        public List<Parameters2> Details { get; set; }
    }
}