using OneOf;
using System.IO;
using System.Threading.Tasks;
namespace PaypalSubscriptions
{
    public interface IPaypalSubscriptionsApiClient
    {
        Task<OneOf<SubscriptionResponse, BadRequestResponse, ErrorResponse>> CreateSubscription(CreateSubscriptionRequest request);
        Task<OneOf<SubscriptionResponse, BadRequestResponse, ErrorResponse>> ShowSubscriptionDetails(ShowSubscriptionDetailsParameters queryParameters, string subscriptionId);
        Task<OneOf<Stream, ErrorResponse, BadRequestResponse>> UpdateSubscription(List<UpdateSubscriptionRequest> request, string subscriptionId);
        Task<OneOf<RevisePlanOrQuantityOfSubscriptionOKResponse, ErrorResponse, BadRequestResponse>> RevisePlanOrQuantityOfSubscription(RevisePlanOrQuantityOfSubscriptionRequest request, string subscriptionId);
        Task<OneOf<Stream, ErrorResponse, BadRequestResponse>> SuspendSubscription(ReasonRequest request, string subscriptionId);
        Task<OneOf<Stream, ErrorResponse, BadRequestResponse>> ActivateSubscription(ReasonRequest request, string subscriptionId);
        Task<OneOf<Stream, ErrorResponse, BadRequestResponse>> CancelSubscription(ReasonRequest request, string subscriptionId);
        Task<OneOf<ErrorResponse, BadRequestResponse, Stream>> CaptureAuthorizedPaymentOnSubscription(CaptureAuthorizedPaymentOnSubscriptionRequest request, string subscriptionId);
        Task<OneOf<ListTransactionsForSubscriptionOKResponse, BadRequestResponse, ErrorResponse>> ListTransactionsForSubscription(ListTransactionsForSubscriptionParameters queryParameters, string subscriptionId);
    }
}