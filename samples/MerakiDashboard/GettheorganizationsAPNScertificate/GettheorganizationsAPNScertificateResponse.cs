using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<GettheorganizationsAPNScertificateResponse>(myJsonResponse);
namespace MerakiDashboard
{
    public class GettheorganizationsAPNScertificateResponse
    {
        public string Certificate { get; set; }
    }
}