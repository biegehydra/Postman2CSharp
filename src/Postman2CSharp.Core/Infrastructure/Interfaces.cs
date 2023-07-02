namespace Postman2CSharp.Core.Infrastructure;

public interface IFormData
{
    public string Key { get; }
    public string CsPropertyName { get; }
    public FormDataType FormDataType { get; }
}
