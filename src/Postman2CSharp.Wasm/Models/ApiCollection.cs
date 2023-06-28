using Postman2CSharp.Core.Models;
using Postman2CSharp.Core.Models.PostmanCollection;

namespace Postman2CSharp.Wasm.Models;

public class ApiCollection
{
    public string Id { get; init; } = Guid.NewGuid().ToString();
    public required CollectionInfo CollectionInfo { get; init; }
    public required List<ApiClient> ApiClients { get; init; }
}