using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<CreateANewOrganizationRequest>(myJsonResponse);
namespace MerakiDashboardv135
{
    public class CreateANewOrganizationRequest
    {
        public string Name { get; set; }
        public Management Management { get; set; }
    }

    public class Details
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }

    public class Management
    {
        public List<Details> Details { get; set; }
    }
}