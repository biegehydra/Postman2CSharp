using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<ReturnsvideolinktothespecifiedcameraResponse>(myJsonResponse);
namespace MerakiDashboard
{
    public class ReturnsvideolinktothespecifiedcameraResponse
    {
        public string Url { get; set; }
    }
}