using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<UpdateLayer3OSPFRoutingConfigurationResponse>(myJsonResponse);
namespace MerakiDashboard
{
    public class UpdateLayer3OSPFRoutingConfigurationResponse
    {
        public bool Enabled { get; set; }
        public int HelloTimerInSeconds { get; set; }
        public int DeadTimerInSeconds { get; set; }
        public List<Areas> Areas { get; set; }
        public V3 V3 { get; set; }
        public bool Md5AuthenticationEnabled { get; set; }
        public Md5AuthenticationKey Md5AuthenticationKey { get; set; }
    }
}