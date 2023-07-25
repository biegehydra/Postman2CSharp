using System;
using System.Collections.Generic;
using System.Text.Json;

namespace MerakiDashboard
{
    // Root myDeserializedClass = JsonSerializer.Deserialize<UpdateAnOrganizationRequest>(myJsonResponse);
    public class UpdateAnOrganizationRequest
    {
        public string Name { get; set; }
        public Management Management { get; set; }
        public DnsRewrite Api { get; set; }
    }
}