using System;
using System.Collections.Generic;
using System.Text.Json;

namespace MerakiDashboard
{
    // Root myDeserializedClass = JsonSerializer.Deserialize<CreateANewOrganizationRequest>(myJsonResponse);
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