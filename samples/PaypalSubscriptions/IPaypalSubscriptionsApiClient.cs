using System.IO;
using System.Threading.Tasks;
namespace PaypalSubscriptions
{
    public interface IPaypalSubscriptionsApiClient
    {
        Task<Stream> Createsubscription(CreatesubscriptionRequest request);
        Task<ShowsubscriptiondetailsResponse> Showsubscriptiondetails(ShowsubscriptiondetailsParameters queryParameters, string subscriptionId);
        Task<Stream> Updatesubscription(UpdatesubscriptionRequest request, string subscriptionId);
        Task<ReviseplanorquantityofsubscriptionResponse> Reviseplanorquantityofsubscription(ReviseplanorquantityofsubscriptionRequest request, string subscriptionId);
        Task<Stream> Suspendsubscription(SuspendsubscriptionRequest request, string subscriptionId);
        Task<Stream> Activatesubscription(ActivatesubscriptionRequest request, string subscriptionId);
        Task<Stream> Cancelsubscription(CancelsubscriptionRequest request, string subscriptionId);
        Task<Stream> Captureauthorizedpaymentonsubscription(CaptureauthorizedpaymentonsubscriptionRequest request, string subscriptionId);
        Task<ListtransactionsforsubscriptionResponse> Listtransactionsforsubscription(ListtransactionsforsubscriptionParameters queryParameters, string subscriptionId);
    }
}