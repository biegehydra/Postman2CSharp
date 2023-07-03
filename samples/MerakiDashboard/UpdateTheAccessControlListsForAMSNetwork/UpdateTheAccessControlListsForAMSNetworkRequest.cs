using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<UpdateTheAccessControlListsForAMSNetworkRequest>(myJsonResponse);
namespace MerakiDashboard
{
    public class UpdateTheAccessControlListsForAMSNetworkRequest
    {
        public List<Rules> Rules { get; set; }
    }
}