using System.Text.Json.Serialization;

namespace NewRelicAlerts
{
    public class GraphQLRequest
    {
        [JsonPropertyName("query")]
        public string Query { get; set; }
    
        // Using 'object' to allow for various input types, including structured objects or JSON strings.
        [JsonPropertyName("variables")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public object Variables { get; set; }
    
        [JsonPropertyName("operationName")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string OperationName { get; set; }
    
        public GraphQLRequest(string query, object variables = null, string operationName = null)
        {
            Query = query;
            Variables = variables;
            OperationName = operationName;
        }
    }
}