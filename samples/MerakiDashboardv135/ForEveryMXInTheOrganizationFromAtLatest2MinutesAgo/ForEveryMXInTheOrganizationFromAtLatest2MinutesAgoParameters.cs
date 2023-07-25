using System;
using System.Collections.Generic;
using System.Text.Json;

namespace MerakiDashboard
{
    // Root myDeserializedClass = JsonSerializer.Deserialize<ForEveryMXInTheOrganizationFromAtLatest2MinutesAgoParameters>(myJsonResponse);
    public class ForEveryMXInTheOrganizationFromAtLatest2MinutesAgoParameters : IQueryParameters
    {
        /// <summary>
        /// The beginning of the timespan for the data. The maximum lookback period is 60 days from today. 
        /// </summary>
        public string T0 { get; set; }
        
        /// <summary>
        /// The end of the timespan for the data. t1 can be a maximum of 5 minutes after t0. The latest possible 
        /// time that t1 can be is 2 minutes into the past. 
        /// </summary>
        public string T1 { get; set; }
        
        /// <summary>
        /// The timespan for which the information will be fetched. If specifying timespan, do not specify 
        /// parameters t0 and t1. The value must be in seconds and be less than or equal to 5 minutes. The 
        /// default is 5 minutes. 
        /// </summary>
        public string Timespan { get; set; }
        
        /// <summary>
        /// Optional filter for a specific WAN uplink. Valid uplinks are wan1, wan2, wan3, cellular. Default 
        /// will return all uplinks. 
        /// </summary>
        public string Uplink { get; set; }
        
        /// <summary>
        /// Optional filter for a specific destination IP. Default will return all destination IPs. 
        /// </summary>
        public string Ip { get; set; }

        public Dictionary<string, string?> ToDictionary()
        {
            return new Dictionary<string, string?>
            {
                {
                    "t0",
                    T0
                },
                {
                    "t1",
                    T1
                },
                {
                    "timespan",
                    Timespan
                },
                {
                    "uplink",
                    Uplink
                },
                {
                    "ip",
                    Ip
                }
            };
        }
    }
}