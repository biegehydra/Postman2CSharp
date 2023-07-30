using System.Text.Json;

namespace Postman2CSharp.UI.Infrastructure
{
    public static class SerializerSettings
    {
        public static readonly JsonSerializerOptions Web = new(JsonSerializerDefaults.Web);
    }
}
