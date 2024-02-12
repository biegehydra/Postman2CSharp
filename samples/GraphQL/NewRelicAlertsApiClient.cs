using System.Net.Http.Headers;
using System.Text;
using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

// Generated using Postman2CSharp https://postman2csharp.com/Convert
namespace NewRelicAlerts
{
    public class NewRelicAlertsApiClient : INewRelicAlertsApiClient
    {
        private readonly HttpClient _httpClient;
        private string _url = string.Empty;
        private readonly string _apiKey;
        public NewRelicAlertsApiClient(HttpClient httpClient, IConfiguration config)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri($"https://{_url}/");
    
            _apiKey = config["Path:ToApiKey"];
            _httpClient.DefaultRequestHeaders.Add($"Api-Key", _apiKey);
        }
    
        public async Task<Stream> AlertsConditionDelete(DeleteGraphQLVariables graphQlVariables)
        {
            string query = NewRelicAlertsGraphQLQueries.AlertsConditionDelete;
            var graphQlRequest = new GraphQLRequest(query, graphQlVariables);
            var response = await _httpClient.PostAsJsonAsync($"", graphQlRequest);
            return await response.Content.ReadAsStreamAsync();
        }
    
        public async Task<Stream> AlertsNotificationChannelsAddToPolicy(NotificationChannelsAddToPolicyGraphQLVariables graphQlVariables)
        {
            string query = NewRelicAlertsGraphQLQueries.AlertsNotificationChannelsAddToPolicy;
            var graphQlRequest = new GraphQLRequest(query, graphQlVariables);
            var response = await _httpClient.PostAsJsonAsync($"", graphQlRequest);
            return await response.Content.ReadAsStreamAsync();
        }
    
        public async Task<Stream> AlertsMutingRuleCreate(AlertsMutingRuleCreateGraphQLVariables graphQlVariables)
        {
            string query = NewRelicAlertsGraphQLQueries.AlertsMutingRuleCreate;
            var graphQlRequest = new GraphQLRequest(query, graphQlVariables);
            var response = await _httpClient.PostAsJsonAsync($"", graphQlRequest);
            return await response.Content.ReadAsStreamAsync();
        }
    
        public async Task<Stream> AlertsMutingRuleDelete(DeleteGraphQLVariables graphQlVariables)
        {
            string query = NewRelicAlertsGraphQLQueries.AlertsMutingRuleDelete;
            var graphQlRequest = new GraphQLRequest(query, graphQlVariables);
            var response = await _httpClient.PostAsJsonAsync($"", graphQlRequest);
            return await response.Content.ReadAsStreamAsync();
        }
    
        public async Task<Stream> AlertsMutingRuleUpdate(AlertsMutingRuleUpdateGraphQLVariables graphQlVariables)
        {
            string query = NewRelicAlertsGraphQLQueries.AlertsMutingRuleUpdate;
            var graphQlRequest = new GraphQLRequest(query, graphQlVariables);
            var response = await _httpClient.PostAsJsonAsync($"", graphQlRequest);
            return await response.Content.ReadAsStreamAsync();
        }
    
        public async Task<Stream> AlertsNotificationChannelCreate(AlertsNotificationChannelCreateGraphQLVariables graphQlVariables)
        {
            string query = NewRelicAlertsGraphQLQueries.AlertsNotificationChannelCreate;
            var graphQlRequest = new GraphQLRequest(query, graphQlVariables);
            var response = await _httpClient.PostAsJsonAsync($"", graphQlRequest);
            return await response.Content.ReadAsStreamAsync();
        }
    
        public async Task<Stream> AlertsNrqlConditionBaselineUpdate(UpdateGraphQLVariables graphQlVariables)
        {
            string query = NewRelicAlertsGraphQLQueries.AlertsNrqlConditionBaselineUpdate;
            var graphQlRequest = new GraphQLRequest(query, graphQlVariables);
            var response = await _httpClient.PostAsJsonAsync($"", graphQlRequest);
            return await response.Content.ReadAsStreamAsync();
        }
    
        public async Task<Stream> AlertsNrqlConditionOutlierUpdate(UpdateGraphQLVariables graphQlVariables)
        {
            string query = NewRelicAlertsGraphQLQueries.AlertsNrqlConditionOutlierUpdate;
            var graphQlRequest = new GraphQLRequest(query, graphQlVariables);
            var response = await _httpClient.PostAsJsonAsync($"", graphQlRequest);
            return await response.Content.ReadAsStreamAsync();
        }
    
        public async Task<Stream> AlertsNrqlConditionOutlierCreate(CreateGraphQLVariables graphQlVariables)
        {
            string query = NewRelicAlertsGraphQLQueries.AlertsNrqlConditionOutlierCreate;
            var graphQlRequest = new GraphQLRequest(query, graphQlVariables);
            var response = await _httpClient.PostAsJsonAsync($"", graphQlRequest);
            return await response.Content.ReadAsStreamAsync();
        }
    
        public async Task<Stream> AlertsNrqlConditionBaselineCreate(CreateGraphQLVariables graphQlVariables)
        {
            string query = NewRelicAlertsGraphQLQueries.AlertsNrqlConditionBaselineCreate;
            var graphQlRequest = new GraphQLRequest(query, graphQlVariables);
            var response = await _httpClient.PostAsJsonAsync($"", graphQlRequest);
            return await response.Content.ReadAsStreamAsync();
        }
    
        public async Task<Stream> AlertsNotificationChannelsRemoveFromPolicy(NotificationChannelsAddToPolicyGraphQLVariables graphQlVariables)
        {
            string query = NewRelicAlertsGraphQLQueries.AlertsNotificationChannelsRemoveFromPolicy;
            var graphQlRequest = new GraphQLRequest(query, graphQlVariables);
            var response = await _httpClient.PostAsJsonAsync($"", graphQlRequest);
            return await response.Content.ReadAsStreamAsync();
        }
    
        public async Task<Stream> AlertsNrqlConditionStaticUpdate(UpdateGraphQLVariables graphQlVariables)
        {
            string query = NewRelicAlertsGraphQLQueries.AlertsNrqlConditionStaticUpdate;
            var graphQlRequest = new GraphQLRequest(query, graphQlVariables);
            var response = await _httpClient.PostAsJsonAsync($"", graphQlRequest);
            return await response.Content.ReadAsStreamAsync();
        }
    
        public async Task<Stream> AlertsPolicyCreate(AlertsPolicyCreateGraphQLVariables graphQlVariables)
        {
            string query = NewRelicAlertsGraphQLQueries.AlertsPolicyCreate;
            var graphQlRequest = new GraphQLRequest(query, graphQlVariables);
            var response = await _httpClient.PostAsJsonAsync($"", graphQlRequest);
            return await response.Content.ReadAsStreamAsync();
        }
    
        public async Task<Stream> AlertsNrqlConditionStaticCreate(CreateGraphQLVariables graphQlVariables)
        {
            string query = NewRelicAlertsGraphQLQueries.AlertsNrqlConditionStaticCreate;
            var graphQlRequest = new GraphQLRequest(query, graphQlVariables);
            var response = await _httpClient.PostAsJsonAsync($"", graphQlRequest);
            return await response.Content.ReadAsStreamAsync();
        }
    
        public async Task<Stream> AlertsPolicyUpdate(AlertsPolicyUpdateGraphQLVariables graphQlVariables)
        {
            string query = NewRelicAlertsGraphQLQueries.AlertsPolicyUpdate;
            var graphQlRequest = new GraphQLRequest(query, graphQlVariables);
            var response = await _httpClient.PostAsJsonAsync($"", graphQlRequest);
            return await response.Content.ReadAsStreamAsync();
        }
    
        public async Task<Stream> AlertsPolicyDelete(DeleteGraphQLVariables graphQlVariables)
        {
            string query = NewRelicAlertsGraphQLQueries.AlertsPolicyDelete;
            var graphQlRequest = new GraphQLRequest(query, graphQlVariables);
            var response = await _httpClient.PostAsJsonAsync($"", graphQlRequest);
            return await response.Content.ReadAsStreamAsync();
        }
    
        public async Task<Stream> AlertsNotificationChannelDelete(DeleteGraphQLVariables graphQlVariables)
        {
            string query = NewRelicAlertsGraphQLQueries.AlertsNotificationChannelDelete;
            var graphQlRequest = new GraphQLRequest(query, graphQlVariables);
            var response = await _httpClient.PostAsJsonAsync($"", graphQlRequest);
            return await response.Content.ReadAsStreamAsync();
        }
    
        public async Task<Stream> AlertsNotificationChannelUpdate(AlertsNotificationChannelUpdateGraphQLVariables graphQlVariables)
        {
            string query = NewRelicAlertsGraphQLQueries.AlertsNotificationChannelUpdate;
            var graphQlRequest = new GraphQLRequest(query, graphQlVariables);
            var response = await _httpClient.PostAsJsonAsync($"", graphQlRequest);
            return await response.Content.ReadAsStreamAsync();
        }
    }
}