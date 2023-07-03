using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<UpdateLayer3OSPFRoutingConfigurationRequest>(myJsonResponse);
namespace MerakiDashboard
{
    public class UpdateLayer3OSPFRoutingConfigurationRequest
    {
        public string Enabled { get; set; }
        public string HelloTimerInSeconds { get; set; }
        public string DeadTimerInSeconds { get; set; }
        public List<Areas> Areas { get; set; }
        public V3 V3 { get; set; }
        public string Md5AuthenticationEnabled { get; set; }
        public Md5AuthenticationKey2 Md5AuthenticationKey { get; set; }
    }

    public class V32
    {
        public string Enabled { get; set; }
        public string HelloTimerInSeconds { get; set; }
        public string DeadTimerInSeconds { get; set; }
        public List<Areas> Areas { get; set; }
    }

    public class Md5AuthenticationKey2
    {
        public string Id { get; set; }
        public string Passphrase { get; set; }
    }
}