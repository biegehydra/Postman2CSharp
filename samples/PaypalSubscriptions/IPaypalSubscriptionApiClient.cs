using OneOf;
using System.IO;
using System.Threading.Tasks;
namespace PaypalSubscription
{
    public interface IPaypalSubscriptionApiClient
    {
        Task<OneOf<SubscriptionResponse, CreateSubscription, UnauthorizedResponse>> CreateSubscription(CreateSubscriptionRequest request);
        Task<OneOf<SubscriptionResponse, CreateSubscription, UnauthorizedResponse>> ShowSubscriptionDetails(ShowSubscriptionDetailsParameters queryParameters, string subscriptionId);
        Task<OneOf<Stream, UnauthorizedResponse, CreateSubscription>> UpdateSubscription(List<UpdateSubscriptionRequest> request, string subscriptionId);
        Task<OneOf<RevisePlanOrQuantityOfSubscriptionOKResponse, UnauthorizedResponse, CreateSubscription>> RevisePlanOrQuantityOfSubscription(RevisePlanOrQuantityOfSubscriptionRequest request, string subscriptionId);
        Task<OneOf<Stream, UnauthorizedResponse, CreateSubscription>> SuspendSubscription(ReasonRequest request, string subscriptionId);
        Task<OneOf<Stream, UnauthorizedResponse, CreateSubscription>> ActivateSubscription(ReasonRequest request, string subscriptionId);
        Task<OneOf<Stream, UnauthorizedResponse, CreateSubscription>> CancelSubscription(ReasonRequest request, string subscriptionId);
        Task<OneOf<UnauthorizedResponse, CreateSubscription, Stream>> CaptureAuthorizedPaymentOnSubscription(CaptureAuthorizedPaymentOnSubscriptionRequest request, string subscriptionId);
        Task<OneOf<ListTransactionsForSubscriptionOKResponse, CreateSubscription, UnauthorizedResponse>> ListTransactionsForSubscription(ListTransactionsForSubscriptionParameters queryParameters, string subscriptionId);
    }
}