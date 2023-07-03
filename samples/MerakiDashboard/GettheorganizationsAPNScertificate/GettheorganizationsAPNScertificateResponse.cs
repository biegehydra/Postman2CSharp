using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<GetTheOrganizationsAPNSCertificateResponse>(myJsonResponse);
namespace MerakiDashboard
{
    public class GetTheOrganizationsAPNSCertificateResponse
    {
        public string Certificate { get; set; }
    }
}