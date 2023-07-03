using System;
using System.Collections.Generic;
using System.Text.Json;

// Root myDeserializedClass = JsonSerializer.Deserialize<ListTheAPIRequestsMadeByAnOrganizationParameters>(myJsonResponse);
namespace MerakiDashboard
{
    public class ListTheAPIRequestsMadeByAnOrganizationParameters : IQueryParameters
    {
        /// <summary>
        /// The beginning of the timespan for the data. The maximum lookback period is 31 days from today.
        /// </summary>
        public string T0 { get; set; }
        
        /// <summary>
        /// The end of the timespan for the data. t1 can be a maximum of 31 days after t0.
        /// </summary>
        public string T1 { get; set; }
        
        /// <summary>
        /// The timespan for which the information will be fetched. If specifying timespan, do not specify parameters t0 and t1. The value must be in seconds and be less than or equal to 31 days. The default is 31 days.
        /// </summary>
        public string Timespan { get; set; }
        
        /// <summary>
        /// The number of entries per page returned. Acceptable range is 3 - 1000. Default is 50.
        /// </summary>
        public string PerPage { get; set; }
        
        /// <summary>
        /// A token used by the server to indicate the start of the page. Often this is a timestamp or an ID but it is not limited to those. This parameter should not be defined by client applications. The link for the first, last, prev, or next page in the HTTP Link header should define it.
        /// </summary>
        public string StartingAfter { get; set; }
        
        /// <summary>
        /// A token used by the server to indicate the end of the page. Often this is a timestamp or an ID but it is not limited to those. This parameter should not be defined by client applications. The link for the first, last, prev, or next page in the HTTP Link header should define it.
        /// </summary>
        public string EndingBefore { get; set; }
        
        /// <summary>
        /// Filter the results by the ID of the admin who made the API requests
        /// </summary>
        public string AdminId { get; set; }
        
        /// <summary>
        /// Filter the results by the path of the API requests
        /// </summary>
        public string Path { get; set; }
        
        /// <summary>
        /// Filter the results by the method of the API requests (must be 'GET', 'PUT', 'POST' or 'DELETE')
        /// </summary>
        public string Method { get; set; }
        
        /// <summary>
        /// Filter the results by the response code of the API requests
        /// </summary>
        public string ResponseCode { get; set; }
        
        /// <summary>
        /// Filter the results by the IP address of the originating API request
        /// </summary>
        public string SourceIp { get; set; }
        
        /// <summary>
        /// Filter the results by the user agent string of the API request
        /// </summary>
        public string UserAgent { get; set; }
        
        /// <summary>
        /// Filter the results by the API version of the API request
        /// </summary>
        public string Version { get; set; }
        
        /// <summary>
        /// Filter the results by one or more operation IDs for the API request
        /// </summary>
        public string OperationIds { get; set; }

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
                    "perPage",
                    PerPage
                },
                {
                    "startingAfter",
                    StartingAfter
                },
                {
                    "endingBefore",
                    EndingBefore
                },
                {
                    "adminId",
                    AdminId
                },
                {
                    "path",
                    Path
                },
                {
                    "method",
                    Method
                },
                {
                    "responseCode",
                    ResponseCode
                },
                {
                    "sourceIp",
                    SourceIp
                },
                {
                    "userAgent",
                    UserAgent
                },
                {
                    "version",
                    Version
                },
                {
                    "operationIds",
                    OperationIds
                }
            };
        }
    }
}