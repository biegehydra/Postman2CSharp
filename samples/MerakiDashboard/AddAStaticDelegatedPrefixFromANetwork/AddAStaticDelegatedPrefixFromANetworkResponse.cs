using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<AddAStaticDelegatedPrefixFromANetworkResponse>(myJsonResponse);
namespace MerakiDashboard
{
    public class AddAStaticDelegatedPrefixFromANetworkResponse
    {
        public string StaticDelegatedPrefixId { get; set; }
        public string Prefix { get; set; }
        public Origin Origin { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}