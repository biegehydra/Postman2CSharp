using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Newtonsoft
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<FindPlaceFromTextResponse>(myJsonResponse);
    public class FindPlaceFromTextResponse
    {
        public List<Candidates> Candidates { get; set; }
        public string Status { get; set; }
    }

    public class Candidates
    {
        public string FormattedAddress { get; set; }
        public Geometry Geometry { get; set; }
        public string Name { get; set; }
        public OpeningHours OpeningHours { get; set; }
        public double Rating { get; set; }
    }
}