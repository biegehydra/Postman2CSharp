using System;
using System.Collections.Generic;
using System.Text.Json;

// Root myDeserializedClass = JsonSerializer.Deserialize<DputInKilobitsPerSecondForAWiredNetworkDeviceParameters>(myJsonResponse);
namespace MerakiDashboard
{
    public class DputInKilobitsPerSecondForAWiredNetworkDeviceParameters : IQueryParameters
    {
        /// <summary>
        /// The beginning of the timespan for the data. The maximum lookback period is 60 days from today.
        /// </summary>
        public string T0 { get; set; }
        
        /// <summary>
        /// The end of the timespan for the data. t1 can be a maximum of 31 days after t0.
        /// </summary>
        public string T1 { get; set; }
        
        /// <summary>
        /// The timespan for which the information will be fetched. If specifying timespan, do not specify parameters t0 and t1. The value must be in seconds and be less than or equal to 31 days. The default is 1 day.
        /// </summary>
        public string Timespan { get; set; }
        
        /// <summary>
        /// The time resolution in seconds for returned data. The valid resolutions are: 60, 600, 3600, 86400. The default is 60.
        /// </summary>
        public string Resolution { get; set; }
        
        /// <summary>
        /// The WAN uplink used to obtain the requested stats. Valid uplinks are wan1, wan2, wan3, cellular. The default is wan1.
        /// </summary>
        public string Uplink { get; set; }
        
        /// <summary>
        /// (Required) The destination IP used to obtain the requested stats. This is required.
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
                    "resolution",
                    Resolution
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