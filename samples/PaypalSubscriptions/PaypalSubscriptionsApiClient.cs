using System.Net.Http.Headers;
using System.Text;
using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
namespace PaypalSubscriptions
{
    /// <summary>
    /// Use the `/billing/subscriptions` resource to create and manage subscriptions.
    /// </summary>
    public class PaypalSubscriptionsApiClient : IPaypalSubscriptionsApiClient
    {
        private readonly HttpClient _httpClient;
        private string _baseUrl = "https://api-m.sandbox.paypal.com";
        private string _billingPlanId = string.Empty;
        private string _paypalAuthAssertion = string.Empty;
        private string _paypalClientMetadataId = string.Empty;
        private string _paypalPartnerAttributionId = "TEST_ATTRIBUTION_ID";
        private string _preferRepresentationDetailed = "return=representation";
        private string _tomorrow = string.Empty;
        private string _guid = string.Empty;
        private string _preferRepresentationMinimal = string.Empty;
        private readonly string _bearerToken;
        public PaypalSubscriptionsApiClient(HttpClient httpClient, IConfiguration config)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri($"https://{_baseUrl}/v1/billing/subscriptions/");
    
            _bearerToken = config["Path:ToBearToken"];
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _bearerToken);
        }
    
        /// <summary>
        /// Creates a subscription.
        /// </summary>
        public async Task<Stream> CreateSubscription(CreateSubscriptionRequest request)
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
            return await response.Content.ReadAsStreamAsync();
        }
    
        /// <summary>
        /// Shows details for a subscription, by ID.
        /// </summary>
        public async Task<ShowSubscriptionDetailsResponse> ShowSubscriptionDetails(ShowSubscriptionDetailsParameters queryParameters, string subscriptionId)
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
            return await _httpClient.GetFromJsonAsync<ShowSubscriptionDetailsResponse>(queryString, headers);
        }
    
        /// <summary>
        /// Updates a subscription which could be in <code>ACTIVE</code> or <code>SUSPENDED</code> status. You can override plan level default attributes by providing customised values for plan path in the patch request.<br /> <ul> <li>You cannot update attributes that have already completed (Example - trial cycles can’t be updated if completed).</li> <li>Once overridden, changes to plan resource will not impact subscription.</li> <li>Any price update will not impact billing cycles within next 10 days (Applicable only for subscriptions funded by PayPal account).</li> </ul> Following are the fields eligible for patch.<table><thead><tr><th>Attribute or object</th><th>Operations</th></tr></thead><tbody><tr><td><code>billing_info.outstanding_balance</code></td><td>replace</td></tr><tr><td><code>custom_id</code></td><td>add,replace</td></tr><tr><td><code>plan.billing_cycles[@sequence==n].<br/>pricing_scheme.fixed_price</code></td><td>add,replace</td></tr><tr><td><code>plan.billing_cycles[@sequence==n].<br/>pricing_scheme.tiers</code></td><td>replace</td></tr><tr><td><code>plan.billing_cycles[@sequence==n].<br/>total_cycles</code></td><td>replace</td></tr><tr><td><code>plan.payment_preferences.<br/>auto_bill_outstanding</code></td><td>replace</td></tr><tr><td><code>plan.payment_preferences.<br/>payment_failure_threshold</code></td><td>replace</td></tr><tr><td><code>plan.taxes.inclusive</code></td><td>add,replace</td></tr><tr><td><code>plan.taxes.percentage</code></td><td>add,replace</td></tr><tr><td><code>shipping_amount</code></td><td>add,replace</td></tr><tr><td><code>start_time</code></td><td>replace</td></tr><tr><td><code>subscriber.shipping_address</code></td><td>add,replace</td></tr><tr><td><code>subscriber.payment_source (for subscriptions funded<br/>by card payments)</code></td><td>replace</td></tr></tbody></table>
        /// </summary>
        public async Task<Stream> UpdateSubscription(UpdateSubscriptionRequest request, string subscriptionId)
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
            return await response.Content.ReadAsStreamAsync();
        }
    
        /// <summary>
        /// Updates the quantity of the product or service in a subscription. You can also use this method to switch the plan and update the `shipping_amount`, `shipping_address` values for the subscription. This type of update requires the buyer's consent.
        /// </summary>
        public async Task<RevisePlanOrQuantityOfSubscriptionResponse> RevisePlanOrQuantityOfSubscription(RevisePlanOrQuantityOfSubscriptionRequest request, string subscriptionId)
        {
            return await _httpClient.PostJsonAsync<RevisePlanOrQuantityOfSubscriptionResponse>($"{subscriptionId}/revise", request);
        }
    
        /// <summary>
        /// Suspends the subscription.
        /// </summary>
        public async Task<Stream> SuspendSubscription(SuspendSubscriptionRequest request, string subscriptionId)
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
            return await response.Content.ReadAsStreamAsync();
        }
    
        /// <summary>
        /// Activates the subscription.
        /// </summary>
        public async Task<Stream> ActivateSubscription(ActivateSubscriptionRequest request, string subscriptionId)
        {
            var response = await _httpClient.PostAsJsonAsync($"{subscriptionId}/activate", request);
            return await response.Content.ReadAsStreamAsync();
        }
    
        /// <summary>
        /// Cancels the subscription.
        /// </summary>
        public async Task<Stream> CancelSubscription(CancelSubscriptionRequest request, string subscriptionId)
        {
            var response = await _httpClient.PostAsJsonAsync($"{subscriptionId}/cancel", request);
            return await response.Content.ReadAsStreamAsync();
        }
    
        /// <summary>
        /// Captures an authorized payment from the subscriber on the subscription.
        /// </summary>
        public async Task<Stream> CaptureAuthorizedPaymentOnSubscription(CaptureAuthorizedPaymentOnSubscriptionRequest request, string subscriptionId)
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
            return await response.Content.ReadAsStreamAsync();
        }
    
        /// <summary>
        /// Lists transactions for a subscription.
        /// </summary>
        public async Task<ListTransactionsForSubscriptionResponse> ListTransactionsForSubscription(ListTransactionsForSubscriptionParameters queryParameters, string subscriptionId)
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
            return await _httpClient.GetFromJsonAsync<ListTransactionsForSubscriptionResponse>(queryString, headers);
        }
    }
}