using System;
using System.Collections.Generic;
using System.Text.Json;

namespace MerakiDashboard
{
    // Root myDeserializedClass = JsonSerializer.Deserialize<List<SInAnSMNetworkWithVariousSpecifiedFieldsAndFiltersResponse>>(myJsonResponse);
    public class SInAnSMNetworkWithVariousSpecifiedFieldsAndFiltersResponse
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public string Username { get; set; }
        public bool HasPassword { get; set; }
        public string Tags { get; set; }
        public List<object> AdGroups { get; set; }
        public List<object> AzureAdGroups { get; set; }
        public List<object> SamlGroups { get; set; }
        public List<object> AsmGroups { get; set; }
        public bool IsExternal { get; set; }
        public string DisplayName { get; set; }
        public bool HasIdentityCertificate { get; set; }
        public string UserThumbnail { get; set; }
    }
}