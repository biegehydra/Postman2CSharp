using System.IO;
using System.Threading.Tasks;
namespace GooglePlace
{
    public interface IGooglePlaceApiClient
    {
        Task<PlaceDetailsResponse> PlaceDetails(PlaceDetailsParameters queryParameters);
        Task<FindPlaceFromTextResponse> FindPlaceFromText(FindPlaceFromTextParameters queryParameters);
        Task<NearbySearchResponse> NearbySearch(NearbySearchParameters queryParameters);
        Task<TextSearchResponse> TextSearch(TextSearchParameters queryParameters);
        Task<Stream> PlacePhoto(PlacePhotoParameters queryParameters);
        Task<QueryAutocompleteResponse> QueryAutocomplete(QueryAutocompleteParameters queryParameters);
        Task<QueryAutocompleteResponse> Autocomplete(AutocompleteParameters queryParameters);
    }
}