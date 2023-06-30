using System.IO;
using System.Threading.Tasks;
namespace Postman2CSharp
{
    public interface IPostman2CSharpApiClient
    {
        Task<PlaceDetailsResponse> PlaceDetails(PlaceDetailsParameters queryParameters);
        Task<FindPlaceFromTextResponse> FindPlaceFromText(FindPlaceFromTextParameters queryParameters);
        Task<NearbySearchResponse> NearbySearch(NearbySearchParameters queryParameters);
        Task<TextSearchResponse> TextSearch(TextSearchParameters queryParameters);
        Task<Stream> PlacePhoto(PlacePhotoParameters queryParameters);
        Task<QueryAutocompleteResponse> QueryAutocomplete(QueryAutocompleteParameters queryParameters);
        Task<AutocompleteResponse> Autocomplete(AutocompleteParameters queryParameters);
    }
}