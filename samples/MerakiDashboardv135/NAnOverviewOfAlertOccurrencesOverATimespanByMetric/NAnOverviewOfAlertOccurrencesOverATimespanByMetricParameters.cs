using System;
using System.Collections.Generic;
using System.Text.Json;

// Root myDeserializedClass = JsonSerializer.Deserialize<NAnOverviewOfAlertOccurrencesOverATimespanByMetricParameters>(myJsonResponse);
namespace MerakiDashboardv135
{
    public class NAnOverviewOfAlertOccurrencesOverATimespanByMetricParameters : IQueryParameters
    {
        /// <summary>
        /// The beginning of the timespan for the data. The maximum lookback period is 365 days from today. 
        /// </summary>
        public string T0 { get; set; }
        
        /// <summary>
        /// The end of the timespan for the data. t1 can be a maximum of 31 days after t0. 
        /// </summary>
        public string T1 { get; set; }
        
        /// <summary>
        /// The timespan for which the information will be fetched. If specifying timespan, do not specify 
        /// parameters t0 and t1. The value must be in seconds and be less than or equal to 31 days. The default 
        /// is 7 days. 
        /// </summary>
        public string Timespan { get; set; }
        
        /// <summary>
        /// The time interval in seconds for returned data. The valid intervals are: 86400, 604800. The default 
        /// is 604800. 
        /// </summary>
        public string Interval { get; set; }

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
                    "interval",
                    Interval
                }
            };
        }
    }
}