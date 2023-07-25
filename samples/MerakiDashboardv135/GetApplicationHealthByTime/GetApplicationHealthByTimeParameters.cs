using System;
using System.Collections.Generic;
using System.Text.Json;

namespace MerakiDashboard
{
    // Root myDeserializedClass = JsonSerializer.Deserialize<GetApplicationHealthByTimeParameters>(myJsonResponse);
    public class GetApplicationHealthByTimeParameters : IQueryParameters
    {
        /// <summary>
        /// The beginning of the timespan for the data. The maximum lookback period is 7 days from today. 
        /// </summary>
        public string T0 { get; set; }
        
        /// <summary>
        /// The end of the timespan for the data. t1 can be a maximum of 7 days after t0. 
        /// </summary>
        public string T1 { get; set; }
        
        /// <summary>
        /// The timespan for which the information will be fetched. If specifying timespan, do not specify 
        /// parameters t0 and t1. The value must be in seconds and be less than or equal to 7 days. The default 
        /// is 2 hours. 
        /// </summary>
        public string Timespan { get; set; }
        
        /// <summary>
        /// The time resolution in seconds for returned data. The valid resolutions are: 60, 300, 3600, 86400. 
        /// The default is 300. 
        /// </summary>
        public string Resolution { get; set; }

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
                }
            };
        }
    }
}