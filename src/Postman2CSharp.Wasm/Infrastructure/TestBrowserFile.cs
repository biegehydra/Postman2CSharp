using Microsoft.AspNetCore.Components.Forms;

namespace Postman2CSharp.Wasm.Infrastructure
{
    public class TestBrowserFile : IBrowserFile
    {
        private Stream Stream { get; set; }
        public string Name { get; set; }
        public TestBrowserFile(Stream stream, string name)
        {
            Stream = stream;
            Name = name;
        }
        public Stream OpenReadStream(long maxAllowedSize = 512000, CancellationToken cancellationToken = new CancellationToken())
        {
            return Stream;
        }
        public DateTimeOffset LastModified => throw new NotImplementedException();
        public long Size => throw new NotImplementedException();
        public string ContentType => throw new NotImplementedException();
    }
}
