using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<UpdateTheAccessControlListsForAMSNetworkResponse>(myJsonResponse);
namespace MerakiDashboard
{
    public class UpdateTheAccessControlListsForAMSNetworkResponse
    {
        public List<Rules> Rules { get; set; }
    }
}