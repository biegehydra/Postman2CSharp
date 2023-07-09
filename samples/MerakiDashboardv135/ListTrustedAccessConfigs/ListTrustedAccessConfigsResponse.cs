using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<List<ListTrustedAccessConfigsResponse>>(myJsonResponse);
namespace MerakiDashboardv135
{
    public class ListTrustedAccessConfigsResponse
    {
        public string Id { get; set; }
        public string SsidName { get; set; }
        public string Name { get; set; }
        public string Scope { get; set; }
        public List<string> Tags { get; set; }
        public string TimeboundType { get; set; }
        public bool SendExpirationEmails { get; set; }
        public int NotifyTimeBeforeAccessEnds { get; set; }
        public string AdditionalEmailText { get; set; }
        public DateTime AccessStartAt { get; set; }
        public DateTime AccessEndAt { get; set; }
    }
}