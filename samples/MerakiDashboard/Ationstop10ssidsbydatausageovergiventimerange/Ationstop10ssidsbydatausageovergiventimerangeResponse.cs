using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<List<Ationstop10ssidsbydatausageovergiventimerangeResponse>>(myJsonResponse);
namespace MerakiDashboard
{
    public class Ationstop10ssidsbydatausageovergiventimerangeResponse
    {
        public string Name { get; set; }
        public Usage Usage { get; set; }
        public Clients Clients { get; set; }
    }
}