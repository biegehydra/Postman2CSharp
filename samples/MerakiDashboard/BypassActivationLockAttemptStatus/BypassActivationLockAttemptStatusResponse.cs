using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<BypassActivationLockAttemptStatusResponse>(myJsonResponse);
namespace MerakiDashboard
{
    public class BypassActivationLockAttemptStatusResponse
    {
        public string Id { get; set; }
        public string Status { get; set; }
        public Data2 Data { get; set; }
    }

    public class Data2
    {
        [JsonPropertyName("2090938209")]
        public _2090938209 _2090938209 { get; set; }

        [JsonPropertyName("38290139892")]
        public _38290139892 _38290139892 { get; set; }
    }

    public class _2090938209
    {
        public bool Success { get; set; }
        public List<string> Errors { get; set; }
    }

    public class _38290139892
    {
        public bool Success { get; set; }
    }
}