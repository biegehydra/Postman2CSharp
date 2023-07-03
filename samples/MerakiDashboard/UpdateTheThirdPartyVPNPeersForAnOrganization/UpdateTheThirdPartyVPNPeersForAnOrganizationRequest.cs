using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<UpdateTheThirdPartyVPNPeersForAnOrganizationRequest>(myJsonResponse);
namespace MerakiDashboard
{
    public class UpdateTheThirdPartyVPNPeersForAnOrganizationRequest
    {
        public List<Peers> Peers { get; set; }
    }

    public class IpsecPolicies2
    {
        public List<string> IkeCipherAlgo { get; set; }
        public List<string> IkeAuthAlgo { get; set; }
        public List<string> IkePrfAlgo { get; set; }
        public List<string> IkeDiffieHellmanGroup { get; set; }
        public string IkeLifetime { get; set; }
        public List<string> ChildCipherAlgo { get; set; }
        public List<string> ChildAuthAlgo { get; set; }
        public List<string> ChildPfsGroup { get; set; }
        public string ChildLifetime { get; set; }
    }
}