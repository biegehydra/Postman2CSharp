using OneOf;
using System.IO;
using System.Threading.Tasks;
namespace PaypalSubscription
{
    public interface IPaypalSubscriptionApiClient
    {
        Task<OneOf<SubscriptionResponse, BadRequestResponse, UnauthorizedResponse>> CreateSubscription(CreateSubscriptionRequest request);
        Task<OneOf<SubscriptionResponse, BadRequestResponse, UnauthorizedResponse>> ShowSubscriptionDetails(ShowSubscriptionDetailsParameters queryParameters, string subscriptionId);
        Task<OneOf<Stream, UnauthorizedResponse, BadRequestResponse>> UpdateSubscription(List<UpdateSubscriptionRequest> request, string subscriptionId);
        Task<OneOf<RevisePlanOrQuantityOfSubscriptionOKResponse, UnauthorizedResponse, BadRequestResponse>> RevisePlanOrQuantityOfSubscription(RevisePlanOrQuantityOfSubscriptionRequest request, string subscriptionId);
        Task<OneOf<Stream, UnauthorizedResponse, BadRequestResponse>> SuspendSubscription(ReasonRequest request, string subscriptionId);
        Task<OneOf<Stream, UnauthorizedResponse, BadRequestResponse>> ActivateSubscription(ReasonRequest request, string subscriptionId);
        Task<OneOf<Stream, UnauthorizedResponse, BadRequestResponse>> CancelSubscription(ReasonRequest request, string subscriptionId);
        Task<OneOf<UnauthorizedResponse, BadRequestResponse, Stream>> CaptureAuthorizedPaymentOnSubscription(CaptureAuthorizedPaymentOnSubscriptionRequest request, string subscriptionId);
        Task<OneOf<ListTransactionsForSubscriptionOKResponse, BadRequestResponse, UnauthorizedResponse>> ListTransactionsForSubscription(ListTransactionsForSubscriptionParameters queryParameters, string subscriptionId);
    }
}