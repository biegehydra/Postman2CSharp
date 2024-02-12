using System.IO;
using System.Threading.Tasks;
namespace NewRelicAlerts
{
    public interface INewRelicAlertsApiClient
    {
        Task<Stream> AlertsConditionDelete(DeleteGraphQLVariables graphQlVariables);
        Task<Stream> AlertsNotificationChannelsAddToPolicy(NotificationChannelsAddToPolicyGraphQLVariables graphQlVariables);
        Task<Stream> AlertsMutingRuleCreate(AlertsMutingRuleCreateGraphQLVariables graphQlVariables);
        Task<Stream> AlertsMutingRuleDelete(DeleteGraphQLVariables graphQlVariables);
        Task<Stream> AlertsMutingRuleUpdate(AlertsMutingRuleUpdateGraphQLVariables graphQlVariables);
        Task<Stream> AlertsNotificationChannelCreate(AlertsNotificationChannelCreateGraphQLVariables graphQlVariables);
        Task<Stream> AlertsNrqlConditionBaselineUpdate(UpdateGraphQLVariables graphQlVariables);
        Task<Stream> AlertsNrqlConditionOutlierUpdate(UpdateGraphQLVariables graphQlVariables);
        Task<Stream> AlertsNrqlConditionOutlierCreate(CreateGraphQLVariables graphQlVariables);
        Task<Stream> AlertsNrqlConditionBaselineCreate(CreateGraphQLVariables graphQlVariables);
        Task<Stream> AlertsNotificationChannelsRemoveFromPolicy(NotificationChannelsAddToPolicyGraphQLVariables graphQlVariables);
        Task<Stream> AlertsNrqlConditionStaticUpdate(UpdateGraphQLVariables graphQlVariables);
        Task<Stream> AlertsPolicyCreate(AlertsPolicyCreateGraphQLVariables graphQlVariables);
        Task<Stream> AlertsNrqlConditionStaticCreate(CreateGraphQLVariables graphQlVariables);
        Task<Stream> AlertsPolicyUpdate(AlertsPolicyUpdateGraphQLVariables graphQlVariables);
        Task<Stream> AlertsPolicyDelete(DeleteGraphQLVariables graphQlVariables);
        Task<Stream> AlertsNotificationChannelDelete(DeleteGraphQLVariables graphQlVariables);
        Task<Stream> AlertsNotificationChannelUpdate(AlertsNotificationChannelUpdateGraphQLVariables graphQlVariables);
    }
}