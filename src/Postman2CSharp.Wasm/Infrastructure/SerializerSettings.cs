using System.Text.Json;

namespace Postman2CSharp.Wasm.Infrastructure
{
    public static class SerializerSettings
    {
        public static readonly JsonSerializerOptions Web = new(JsonSerializerDefaults.Web);
    }
}
