using System.Text.Json;

namespace Postman2CSharp.Wasm
{
    public static class SerializerSettings
    {
        public static readonly JsonSerializerOptions Web = new (JsonSerializerDefaults.Web);
    }
}
