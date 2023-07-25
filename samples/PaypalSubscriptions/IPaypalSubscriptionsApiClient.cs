using System.IO;
using System.Threading.Tasks;
namespace PaypalSubscriptions
{
    public interface IPaypalSubscriptionsApiClient
    {
        Task<CreateSubscriptionResponse> CreateSubscription(CreateSubscriptionRequest request);
        Task<CreateSubscriptionResponse> ShowSubscriptionDetails(ShowSubscriptionDetailsParameters queryParameters, string subscriptionId);
        Task<Stream> UpdateSubscription(List<UpdateSubscriptionRequest> request, string subscriptionId);
        Task<RevisePlanOrQuantityOfSubscriptionResponse> RevisePlanOrQuantityOfSubscription(RevisePlanOrQuantityOfSubscriptionRequest request, string subscriptionId);
        Task<Stream> SuspendSubscription(SuspendSubscriptionRequest request, string subscriptionId);
        Task<Stream> ActivateSubscription(SuspendSubscriptionRequest request, string subscriptionId);
        Task<Stream> CancelSubscription(SuspendSubscriptionRequest request, string subscriptionId);
        Task<Stream> CaptureAuthorizedPaymentOnSubscription(CaptureAuthorizedPaymentOnSubscriptionRequest request, string subscriptionId);
        Task<ListTransactionsForSubscriptionResponse> ListTransactionsForSubscription(ListTransactionsForSubscriptionParameters queryParameters, string subscriptionId);
    }
}