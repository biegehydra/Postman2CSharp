using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<OrganizationByCloningTheAddressedOrganizationRequest>(myJsonResponse);
namespace MerakiDashboard
{
    public class OrganizationByCloningTheAddressedOrganizationRequest
    {
        public string Name { get; set; }
    }
}