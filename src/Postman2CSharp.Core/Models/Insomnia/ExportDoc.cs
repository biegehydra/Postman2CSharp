using System.Collections.Generic;
using Newtonsoft.Json;

namespace Postman2CSharp.Core.Models.Insomnia
{
    public class ExportDoc
    {
        [JsonProperty("_type")]
        public string Type { get; set; }

        [JsonProperty("__export_format")]
        public int ExportFormat { get; set; }

        [JsonProperty("__export_source")]
        public string ExportSource { get; set; }

        public List<dynamic> Resources { get; set; }
    }
}
