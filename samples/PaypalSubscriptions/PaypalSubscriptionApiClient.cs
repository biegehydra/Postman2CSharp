using System.Net.Http.Headers;
using System.Text;
using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net;
using OneOf;
using Microsoft.Extensions.Configuration;

// Generated using Postman2CSharp https://postman2csharp.com/Convert
namespace PaypalSubscription
{
    /// <summary>
    /// Use the `/billing/subscriptions` resource to create and manage subscriptions.
    /// </summary>
    public class PaypalSubscriptionApiClient : IPaypalSubscriptionApiClient
    {
        private readonly HttpClient _httpClient;
        private string _baseUrl = "api-m.sandbox.paypal.com";
        private string _paypalAuthAssertion = string.Empty;
        private string _paypalClientMetadataId = string.Empty;
        private string _paypalPartnerAttributionId = "TEST_ATTRIBUTION_ID";
        private string _preferRepresentationDetailed = "return=representation";
        private string _guid = string.Empty;
        private string _preferRepresentationMinimal = string.Empty;
        private readonly string _bearerToken;
        public PaypalSubscriptionApiClient(HttpClient httpClient, IConfiguration config)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri($"https://{_baseUrl}/v1/billing/subscriptions/");
    
            _bearerToken = config["Path:ToBearToken"];
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _bearerToken);
        }
    
        /// <summary>
        /// Creates a subscription.
        /// </summary>
        public async Task<OneOf<SubscriptionResponse, CreateSubscription, UnauthorizedResponse>> CreateSubscription(CreateSubscriptionRequest request)
        {
            var headers = new Dictionary<string, string>()
            {
                { $"PayPal-Request-Id", $"{_guid}" },
                { $"PayPal-Client-Metadata-Id", $"{_paypalClientMetadataId}" },
                { $"PayPal-Partner-Attribution-Id", $"{_paypalPartnerAttributionId}" },
                { $"PayPal-Auth-Assertion", $"{_paypalAuthAssertion}" },
                { $"Prefer", $"{_preferRepresentationDetailed}" }
            };
    
            var response = await _httpClient.PostAsJsonAsync($"", request, headers);
            if (response.StatusCode == HttpStatusCode.Created)
            {
                return await response.ReadJsonAsync<SubscriptionResponse>();
            }
            if (response.StatusCode is HttpStatusCode.BadRequest or HttpStatusCode.NotFound or HttpStatusCode.UnprocessableEntity)
            {
                return await response.ReadJsonAsync<CreateSubscription>();
            }
            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                return await response.ReadJsonAsync<UnauthorizedResponse>();
            }
            throw new Exception($"CreateSubscription: Unexpected response. Status Code: {response.StatusCode}. Content: {await response.Content.ReadAsStringAsync()}");
        }
    
        /// <summary>
        /// Shows details for a subscription, by ID.
        /// </summary>
        public async Task<OneOf<SubscriptionResponse, CreateSubscription, UnauthorizedResponse>> ShowSubscriptionDetails(ShowSubscriptionDetailsParameters queryParameters, string subscriptionId)
        {
            var headers = new Dictionary<string, string>()
            {
                { $"PayPal-Request-Id", $"{_guid}" },
                { $"PayPal-Client-Metadata-Id", $"{_paypalClientMetadataId}" },
                { $"PayPal-Partner-Attribution-Id", $"{_paypalPartnerAttributionId}" },
                { $"PayPal-Auth-Assertion", $"{_paypalAuthAssertion}" },
                { $"Prefer", $"{_preferRepresentationDetailed}" }
            };
    
            var parametersDict = queryParameters.ToDictionary();
            var queryString = QueryHelpers.AddQueryString($"{subscriptionId}", parametersDict);
            var response = await _httpClient.GetAsync(queryString, headers);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                return await response.ReadJsonAsync<SubscriptionResponse>();
            }
            if (response.StatusCode is HttpStatusCode.BadRequest or HttpStatusCode.NotFound)
            {
                return await response.ReadJsonAsync<CreateSubscription>();
            }
            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                return await response.ReadJsonAsync<UnauthorizedResponse>();
            }
            throw new Exception($"ShowSubscriptionDetails: Unexpected response. Status Code: {response.StatusCode}. Content: {await response.Content.ReadAsStringAsync()}");
        }
    
        /// <summary>
        /// Updates a subscription which could be in ACTIVE or SUSPENDED status. You can override plan level default 
        /// attributes by providing customised values for plan path in the patch request.
        /// You cannot update attributes that have already completed (Example - trial cycles can’t be updated if 
        /// completed).Once overridden, changes to plan resource will not impact subscription.Any price update will 
        /// not impact billing cycles within next 10 days (Applicable only for subscriptions funded by PayPal account). 
        /// Following are the fields eligible for patch.Attribute or objectOperationsbilling_info.outstanding_balancereplacecustom_idadd,replaceplan.billing_cycles[@sequence==n].
        /// pricing_scheme.fixed_priceadd,replaceplan.billing_cycles[@sequence==n].
        /// pricing_scheme.tiersreplaceplan.billing_cycles[@sequence==n].
        /// total_cyclesreplaceplan.payment_preferences.
        /// auto_bill_outstandingreplaceplan.payment_preferences.
        /// payment_failure_thresholdreplaceplan.taxes.inclusiveadd,replaceplan.taxes.percentageadd,replaceshipping_amountadd,replacestart_timereplacesubscriber.shipping_addressadd,replacesubscriber.payment_source 
        /// (for subscriptions funded
        /// by card payments)replace
        /// </summary>
        public async Task<OneOf<Stream, UnauthorizedResponse, CreateSubscription>> UpdateSubscription(List<UpdateSubscriptionRequest> request, string subscriptionId)
        {
            var headers = new Dictionary<string, string>()
            {
                { $"PayPal-Request-Id", $"{_guid}" },
                { $"PayPal-Client-Metadata-Id", $"{_paypalClientMetadataId}" },
                { $"PayPal-Partner-Attribution-Id", $"{_paypalPartnerAttributionId}" },
                { $"PayPal-Auth-Assertion", $"{_paypalAuthAssertion}" },
                { $"Prefer", $"{_preferRepresentationDetailed}" }
            };
    
            var response = await _httpClient.PatchAsJsonAsync($"{subscriptionId}", request, headers);
            if (response.StatusCode == HttpStatusCode.NoContent)
            {
                return await response.Content.ReadAsStreamAsync();
            }
            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                return await response.ReadJsonAsync<UnauthorizedResponse>();
            }
            if (response.StatusCode is HttpStatusCode.NotFound or HttpStatusCode.UnprocessableEntity)
            {
                return await response.ReadJsonAsync<CreateSubscription>();
            }
            throw new Exception($"UpdateSubscription: Unexpected response. Status Code: {response.StatusCode}. Content: {await response.Content.ReadAsStringAsync()}");
        }
    
        /// <summary>
        /// Updates the quantity of the product or service in a subscription. You can also use this method to switch 
        /// the plan and update the `shipping_amount`, `shipping_address` values for the subscription. This type 
        /// of update requires the buyer's consent.
        /// </summary>
        public async Task<OneOf<RevisePlanOrQuantityOfSubscriptionOKResponse, UnauthorizedResponse, CreateSubscription>> RevisePlanOrQuantityOfSubscription(RevisePlanOrQuantityOfSubscriptionRequest request, string subscriptionId)
        {
            var response = await _httpClient.PostAsJsonAsync($"{subscriptionId}/revise", request);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                return await response.ReadJsonAsync<RevisePlanOrQuantityOfSubscriptionOKResponse>();
            }
            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                return await response.ReadJsonAsync<UnauthorizedResponse>();
            }
            if (response.StatusCode is HttpStatusCode.NotFound or HttpStatusCode.UnprocessableEntity)
            {
                return await response.ReadJsonAsync<CreateSubscription>();
            }
            throw new Exception($"RevisePlanOrQuantityOfSubscription: Unexpected response. Status Code: {response.StatusCode}. Content: {await response.Content.ReadAsStringAsync()}");
        }
    
        /// <summary>
        /// Suspends the subscription.
        /// </summary>
        public async Task<OneOf<Stream, UnauthorizedResponse, CreateSubscription>> SuspendSubscription(ReasonRequest request, string subscriptionId)
        {
            var headers = new Dictionary<string, string>()
            {
                { $"PayPal-Request-Id", $"{_guid}" },
                { $"PayPal-Client-Metadata-Id", $"{_paypalClientMetadataId}" },
                { $"PayPal-Partner-Attribution-Id", $"{_paypalPartnerAttributionId}" },
                { $"PayPal-Auth-Assertion", $"{_paypalAuthAssertion}" },
                { $"Prefer", $"{_preferRepresentationDetailed}" }
            };
    
            var response = await _httpClient.PostAsJsonAsync($"{subscriptionId}/suspend", request, headers);
            if (response.StatusCode == HttpStatusCode.NoContent)
            {
                return await response.Content.ReadAsStreamAsync();
            }
            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                return await response.ReadJsonAsync<UnauthorizedResponse>();
            }
            if (response.StatusCode is HttpStatusCode.NotFound or HttpStatusCode.UnprocessableEntity)
            {
                return await response.ReadJsonAsync<CreateSubscription>();
            }
            throw new Exception($"SuspendSubscription: Unexpected response. Status Code: {response.StatusCode}. Content: {await response.Content.ReadAsStringAsync()}");
        }
    
        /// <summary>
        /// Activates the subscription.
        /// </summary>
        public async Task<OneOf<Stream, UnauthorizedResponse, CreateSubscription>> ActivateSubscription(ReasonRequest request, string subscriptionId)
        {
            var response = await _httpClient.PostAsJsonAsync($"{subscriptionId}/activate", request);
            if (response.StatusCode == HttpStatusCode.NoContent)
            {
                return await response.Content.ReadAsStreamAsync();
            }
            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                return await response.ReadJsonAsync<UnauthorizedResponse>();
            }
            if (response.StatusCode is HttpStatusCode.NotFound or HttpStatusCode.UnprocessableEntity)
            {
                return await response.ReadJsonAsync<CreateSubscription>();
            }
            throw new Exception($"ActivateSubscription: Unexpected response. Status Code: {response.StatusCode}. Content: {await response.Content.ReadAsStringAsync()}");
        }
    
        /// <summary>
        /// Cancels the subscription.
        /// </summary>
        public async Task<OneOf<Stream, UnauthorizedResponse, CreateSubscription>> CancelSubscription(ReasonRequest request, string subscriptionId)
        {
            var response = await _httpClient.PostAsJsonAsync($"{subscriptionId}/cancel", request);
            if (response.StatusCode == HttpStatusCode.NoContent)
            {
                return await response.Content.ReadAsStreamAsync();
            }
            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                return await response.ReadJsonAsync<UnauthorizedResponse>();
            }
            if (response.StatusCode is HttpStatusCode.NotFound or HttpStatusCode.UnprocessableEntity)
            {
                return await response.ReadJsonAsync<CreateSubscription>();
            }
            throw new Exception($"CancelSubscription: Unexpected response. Status Code: {response.StatusCode}. Content: {await response.Content.ReadAsStringAsync()}");
        }
    
        /// <summary>
        /// Captures an authorized payment from the subscriber on the subscription.
        /// </summary>
        public async Task<OneOf<UnauthorizedResponse, CreateSubscription, Stream>> CaptureAuthorizedPaymentOnSubscription(CaptureAuthorizedPaymentOnSubscriptionRequest request, string subscriptionId)
        {
            var headers = new Dictionary<string, string>()
            {
                { $"PayPal-Request-Id", $"{_guid}" },
                { $"PayPal-Client-Metadata-Id", $"{_paypalClientMetadataId}" },
                { $"PayPal-Partner-Attribution-Id", $"{_paypalPartnerAttributionId}" },
                { $"PayPal-Auth-Assertion", $"{_paypalAuthAssertion}" },
                { $"Prefer", $"{_preferRepresentationDetailed}" }
            };
    
            var response = await _httpClient.PostAsJsonAsync($"{subscriptionId}/capture", request, headers);
            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                return await response.ReadJsonAsync<UnauthorizedResponse>();
            }
            if (response.StatusCode is HttpStatusCode.NotFound or HttpStatusCode.UnprocessableEntity)
            {
                return await response.ReadJsonAsync<CreateSubscription>();
            }
            if (response.StatusCode == HttpStatusCode.OK)
            {
                return await response.Content.ReadAsStreamAsync();
            }
            throw new Exception($"CaptureAuthorizedPaymentOnSubscription: Unexpected response. Status Code: {response.StatusCode}. Content: {await response.Content.ReadAsStringAsync()}");
        }
    
        /// <summary>
        /// Lists transactions for a subscription.
        /// </summary>
        public async Task<OneOf<ListTransactionsForSubscriptionOKResponse, CreateSubscription, UnauthorizedResponse>> ListTransactionsForSubscription(ListTransactionsForSubscriptionParameters queryParameters, string subscriptionId)
        {
            var headers = new Dictionary<string, string>()
            {
                { $"PayPal-Request-Id", $"{_guid}" },
                { $"PayPal-Client-Metadata-Id", $"{_paypalClientMetadataId}" },
                { $"PayPal-Partner-Attribution-Id", $"{_paypalPartnerAttributionId}" },
                { $"PayPal-Auth-Assertion", $"{_paypalAuthAssertion}" },
                { $"Prefer", $"{_preferRepresentationDetailed}" }
            };
    
            var parametersDict = queryParameters.ToDictionary();
            var queryString = QueryHelpers.AddQueryString($"{subscriptionId}/transactions", parametersDict);
            var response = await _httpClient.GetAsync(queryString, headers);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                return await response.ReadJsonAsync<ListTransactionsForSubscriptionOKResponse>();
            }
            if (response.StatusCode is HttpStatusCode.BadRequest or HttpStatusCode.NotFound)
            {
                return await response.ReadJsonAsync<CreateSubscription>();
            }
            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                return await response.ReadJsonAsync<UnauthorizedResponse>();
            }
            throw new Exception($"ListTransactionsForSubscription: Unexpected response. Status Code: {response.StatusCode}. Content: {await response.Content.ReadAsStringAsync()}");
        }
    }
}