using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<List<VicemodelssortedbydatausageovergiventimerangeResponse>>(myJsonResponse);
namespace MerakiDashboard
{
    public class VicemodelssortedbydatausageovergiventimerangeResponse
    {
        public string Model { get; set; }
        public int Count { get; set; }
        public Usage8 Usage { get; set; }
    }

    public class Usage8
    {
        public int Total { get; set; }
        public int Average { get; set; }
    }
}