using System;
using System.Collections.Generic;
using System.Text.Json;

// Root myDeserializedClass = JsonSerializer.Deserialize<ListAirMarshalscanresultsfromanetworkParameters>(myJsonResponse);
namespace MerakiDashboard
{
    public class ListAirMarshalscanresultsfromanetworkParameters : IQueryParameters
    {
        /// <summary>
        /// The beginning of the timespan for the data. The maximum lookback period is 31 days from today.
        /// </summary>
        public string T0 { get; set; }
        
        /// <summary>
        /// The timespan for which the information will be fetched. If specifying timespan, do not specify parameter t0. The value must be in seconds and be less than or equal to 31 days. The default is 7 days.
        /// </summary>
        public string Timespan { get; set; }

        public Dictionary<string, string?> ToDictionary()
        {
            return new Dictionary<string, string?>
            {
                {
                    "t0",
                    T0
                },
                {
                    "timespan",
                    Timespan
                }
            };
        }
    }
}