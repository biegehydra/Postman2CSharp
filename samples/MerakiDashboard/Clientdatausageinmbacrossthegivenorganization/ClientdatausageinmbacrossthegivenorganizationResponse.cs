using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<ClientdatausageinmbacrossthegivenorganizationResponse>(myJsonResponse);
namespace MerakiDashboard
{
    public class ClientdatausageinmbacrossthegivenorganizationResponse
    {
        public Usage4 Usage { get; set; }
        public Counts12 Counts { get; set; }
    }

    public class Overall
    {
        public int Total { get; set; }
        public int Downstream { get; set; }
        public int Upstream { get; set; }
    }

    public class Usage4
    {
        public Overall Overall { get; set; }
        public double Average { get; set; }
    }

    public class Counts12
    {
        public int Total { get; set; }
    }
}