using System;
using System.Collections.Generic;
using System.Text.Json;

// Root myDeserializedClass = JsonSerializer.Deserialize<ReturnABluetoothClientParameters>(myJsonResponse);
namespace MerakiDashboardv135
{
    public class ReturnABluetoothClientParameters : IQueryParameters
    {
        /// <summary>
        /// Include the connectivity history for this client 
        /// </summary>
        public string IncludeConnectivityHistory { get; set; }
        
        /// <summary>
        /// The timespan, in seconds, for the connectivityHistory data. By default 1 day, 86400, will be used. 
        /// </summary>
        public string ConnectivityHistoryTimespan { get; set; }

        public Dictionary<string, string?> ToDictionary()
        {
            return new Dictionary<string, string?>
            {
                {
                    "includeConnectivityHistory",
                    IncludeConnectivityHistory
                },
                {
                    "connectivityHistoryTimespan",
                    ConnectivityHistoryTimespan
                }
            };
        }
    }
}