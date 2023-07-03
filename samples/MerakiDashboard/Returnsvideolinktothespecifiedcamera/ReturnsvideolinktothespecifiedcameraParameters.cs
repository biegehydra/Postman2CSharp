using System;
using System.Collections.Generic;
using System.Text.Json;

// Root myDeserializedClass = JsonSerializer.Deserialize<ReturnsvideolinktothespecifiedcameraParameters>(myJsonResponse);
namespace MerakiDashboard
{
    public class ReturnsvideolinktothespecifiedcameraParameters : IQueryParameters
    {
        /// <summary>
        /// [optional] The video link will start at this time. The timestamp should be a string in ISO8601 format. If no timestamp is specified, we will assume current time.
        /// </summary>
        public string Timestamp { get; set; }

        public Dictionary<string, string?> ToDictionary()
        {
            return new Dictionary<string, string?>
            {
                {
                    "timestamp",
                    Timestamp
                }
            };
        }
    }
}