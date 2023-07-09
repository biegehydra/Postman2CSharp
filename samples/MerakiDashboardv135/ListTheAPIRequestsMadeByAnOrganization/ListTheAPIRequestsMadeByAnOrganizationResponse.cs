using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<List<ListTheAPIRequestsMadeByAnOrganizationResponse>>(myJsonResponse);
namespace MerakiDashboardv135
{
    public class ListTheAPIRequestsMadeByAnOrganizationResponse
    {
        public string AdminId { get; set; }
        public string Method { get; set; }
        public string Host { get; set; }
        public string Path { get; set; }
        public string QueryString { get; set; }
        public string UserAgent { get; set; }
        public DateTime Ts { get; set; }
        public int ResponseCode { get; set; }
        public string SourceIp { get; set; }
        public int Version { get; set; }
        public string OperationId { get; set; }
    }
}