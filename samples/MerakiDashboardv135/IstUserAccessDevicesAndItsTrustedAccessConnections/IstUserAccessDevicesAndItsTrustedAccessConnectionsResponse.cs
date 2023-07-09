using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<List<IstUserAccessDevicesAndItsTrustedAccessConnectionsResponse>>(myJsonResponse);
namespace MerakiDashboardv135
{
    public class IstUserAccessDevicesAndItsTrustedAccessConnectionsResponse
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string SystemType { get; set; }
        public string Mac { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public List<string> Tags { get; set; }
        public List<TrustedAccessConnections> TrustedAccessConnections { get; set; }
    }

    public class TrustedAccessConnections
    {
        public string TrustedAccessConfigId { get; set; }
        public DateTime DownloadedAt { get; set; }
        public DateTime ScepCompletedAt { get; set; }
        public DateTime LastConnectedAt { get; set; }
    }
}