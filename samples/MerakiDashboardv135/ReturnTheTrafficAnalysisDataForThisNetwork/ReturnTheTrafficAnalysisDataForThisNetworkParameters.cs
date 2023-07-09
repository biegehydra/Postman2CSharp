using System;
using System.Collections.Generic;
using System.Text.Json;

// Root myDeserializedClass = JsonSerializer.Deserialize<ReturnTheTrafficAnalysisDataForThisNetworkParameters>(myJsonResponse);
namespace MerakiDashboardv135
{
    public class ReturnTheTrafficAnalysisDataForThisNetworkParameters : IQueryParameters
    {
        /// <summary>
        /// The beginning of the timespan for the data. The maximum lookback period is 30 days from today. 
        /// </summary>
        public string T0 { get; set; }
        
        /// <summary>
        /// The timespan for which the information will be fetched. If specifying timespan, do not specify 
        /// parameter t0. The value must be in seconds and be less than or equal to 30 days. 
        /// </summary>
        public string Timespan { get; set; }
        
        /// <summary>
        /// Filter the data by device type: 'combined', 'wireless', 'switch' or 'appliance'. Defaults to 
        /// 'combined'. When using 'combined', for each rule the data will come from the device type with the 
        /// most usage. 
        /// </summary>
        public string DeviceType { get; set; }

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
                },
                {
                    "deviceType",
                    DeviceType
                }
            };
        }
    }
}