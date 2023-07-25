using System;
using System.Collections.Generic;
using System.Text.Json;

namespace MerakiDashboard
{
    // Root myDeserializedClass = JsonSerializer.Deserialize<GetTheOrganizationsAPNSCertificateResponse>(myJsonResponse);
    public class GetTheOrganizationsAPNSCertificateResponse
    {
        public string Certificate { get; set; }
    }
}