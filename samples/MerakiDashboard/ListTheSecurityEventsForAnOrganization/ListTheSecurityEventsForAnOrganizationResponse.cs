using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<List<ListTheSecurityEventsForAnOrganizationResponse>>(myJsonResponse);
namespace MerakiDashboard
{
    public class ListTheSecurityEventsForAnOrganizationResponse
    {
        public DateTime Ts { get; set; }
        public string EventType { get; set; }
        public string ClientName { get; set; }
        public string ClientMac { get; set; }
        public string ClientIp { get; set; }
        public string SrcIp { get; set; }
        public string DestIp { get; set; }
        public string Protocol { get; set; }
        public string Uri { get; set; }
        public string CanonicalName { get; set; }
        public int DestinationPort { get; set; }
        public string FileHash { get; set; }
        public string FileType { get; set; }
        public int FileSizeBytes { get; set; }
        public string Disposition { get; set; }
        public string Action { get; set; }
        public string DeviceMac { get; set; }
        public string Priority { get; set; }
        public string Classification { get; set; }
        public bool? Blocked { get; set; }
        public string Message { get; set; }
        public string Signature { get; set; }
        public string SigSource { get; set; }
        public string RuleId { get; set; }
    }
}