using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

// Root myDeserializedClass = JsonConvert.DeserializeObject<FindPlaceFromTextResponse>(myJsonResponse);
namespace Postman2CSharp
{
    public class Candidates
    {
        public string FormattedAddress { get; set; }
        public Geometry Geometry { get; set; }
        public string Name { get; set; }
        public OpeningHours2 OpeningHours { get; set; }
        public double Rating { get; set; }
    }

    public class FindPlaceFromTextResponse
    {
        public List<Candidates> Candidates { get; set; }
        public string Status { get; set; }
    }

    public class OpeningHours2
    {
        public bool OpenNow { get; set; }
    }
}