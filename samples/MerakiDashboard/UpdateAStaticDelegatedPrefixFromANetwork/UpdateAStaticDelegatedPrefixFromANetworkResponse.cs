using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<UpdateAStaticDelegatedPrefixFromANetworkResponse>(myJsonResponse);
namespace MerakiDashboard
{
    public class UpdateAStaticDelegatedPrefixFromANetworkResponse
    {
        public string StaticDelegatedPrefixId { get; set; }
        public string Prefix { get; set; }
        public Origin Origin { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}