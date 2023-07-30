using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace Postman2CSharp.UI.Services
{
    internal class ServerHostEnvironment : IWebAssemblyHostEnvironment
    {
        public string Environment { get; } = "Server";
        public string BaseAddress => throw new NotImplementedException();
    }
}
