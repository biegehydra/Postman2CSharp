using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<List<IsttheclientsofadeviceuptoamaximumofamonthagoResponse>>(myJsonResponse);
namespace MerakiDashboard
{
    public class IsttheclientsofadeviceuptoamaximumofamonthagoResponse
    {
        public Usage Usage { get; set; }
        public string Id { get; set; }
        public string Description { get; set; }
        public string Mac { get; set; }
        public string Ip { get; set; }
        public string User { get; set; }
        public string Vlan { get; set; }
        public string NamedVlan { get; set; }
        public object Switchport { get; set; }
        public object AdaptivePolicyGroup { get; set; }
        public string MdnsName { get; set; }
        public string DhcpHostname { get; set; }
    }
}