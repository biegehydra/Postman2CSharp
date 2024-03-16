using Postman2CSharp.Core.Models;

namespace Postman2CSharp.UI;

public record ExpandHttpCallEvent(HttpCall? HttpCall, bool Expanded);
public record ExpandApiClientEvent(ApiClient? ApiClient, bool Expanded);