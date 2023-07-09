using Postman2CSharp.Core.Utilities;

namespace Postman2CSharp.Core.Infrastructure;

public interface IFormData : ICsProperty
{
    public FormDataType FormDataType { get; }
    public string? Description { get; }
}
