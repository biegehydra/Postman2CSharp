using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<List<ReturnthetrafficanalysisdataforthisnetworkResponse>>(myJsonResponse);
namespace MerakiDashboard
{
    public class ReturnthetrafficanalysisdataforthisnetworkResponse
    {
        public string Application { get; set; }
        public object Destination { get; set; }
        public string Protocol { get; set; }
        public int Port { get; set; }
        public int Sent { get; set; }
        public int Recv { get; set; }
        public int NumClients { get; set; }
        public int ActiveTime { get; set; }
        public int Flows { get; set; }
    }
}