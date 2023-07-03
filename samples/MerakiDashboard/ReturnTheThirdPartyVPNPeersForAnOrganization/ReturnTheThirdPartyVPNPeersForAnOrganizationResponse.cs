using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<ReturnTheThirdPartyVPNPeersForAnOrganizationResponse>(myJsonResponse);
namespace MerakiDashboard
{
    public class ReturnTheThirdPartyVPNPeersForAnOrganizationResponse
    {
        public List<Peers> Peers { get; set; }
    }

    public class Peers
    {
        public string Name { get; set; }
        public string PublicIp { get; set; }
        public string RemoteId { get; set; }
        public string LocalId { get; set; }
        public string Secret { get; set; }
        public List<string> PrivateSubnets { get; set; }
        public IpsecPolicies IpsecPolicies { get; set; }
        public string IpsecPoliciesPreset { get; set; }
        public string IkeVersion { get; set; }
        public List<string> NetworkTags { get; set; }
    }

    public class IpsecPolicies
    {
        public List<string> IkeCipherAlgo { get; set; }
        public List<string> IkeAuthAlgo { get; set; }
        public List<string> IkePrfAlgo { get; set; }
        public List<string> IkeDiffieHellmanGroup { get; set; }
        public int IkeLifetime { get; set; }
        public List<string> ChildCipherAlgo { get; set; }
        public List<string> ChildAuthAlgo { get; set; }
        public List<string> ChildPfsGroup { get; set; }
        public int ChildLifetime { get; set; }
    }
}