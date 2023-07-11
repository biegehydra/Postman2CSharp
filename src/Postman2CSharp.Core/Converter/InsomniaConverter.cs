using Postman2CSharp.Core.Models.Insomnia;
using Postman2CSharp.Core.Models.PostmanCollection;

namespace Postman2CSharp.Core.Converter;

public static class InsomniaConverter
{
    public static PostmanCollection Convert(InsomniaCollection insomniaCollection)
    {
        var info 
        return new PostmanCollection()
        {
            Auth = null,
            Info = null,
            Item = null,
            Variable = null
        };
    }

    private CollectionInfo GetInfo(InsomniaCollection collection)
    {
        var description = collection.
        return new CollectionInfo()
        {

        }
    }
}