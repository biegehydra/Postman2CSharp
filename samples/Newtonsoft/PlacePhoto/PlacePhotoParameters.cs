using System;
using System.Collections.Generic;
using System.Text.Json;

// Root myDeserializedClass = JsonSerializer.Deserialize<PlacePhotoParameters>(myJsonResponse);
namespace Newtonsoft
{
    public class PlacePhotoParameters : IQueryParameters
    {
        /// <summary>
        /// (Required) A string identifier that uniquely identifies a photo. Photo references are returned from either a Place Search or Place Details request.
        /// 
        /// </summary>
        public string PhotoReference { get; set; }
        
        /// <summary>
        /// Specifies the maximum desired height, in pixels, of the image. If the image is smaller than the values specified, the original image will be returned. If the image is larger in either dimension, it will be scaled to match the smaller of the two dimensions, restricted to its original aspect ratio. Both the `maxheight` and `maxwidth` properties accept an integer between `1` and `1600`.
        /// 
        /// </summary>
        public string Maxheight { get; set; }
        
        /// <summary>
        /// Specifies the maximum desired width, in pixels, of the image. If the image is smaller than the values specified, the original image will be returned. If the image is larger in either dimension, it will be scaled to match the smaller of the two dimensions, restricted to its original aspect ratio. Both the `maxheight` and `maxwidth` properties accept an integer between `1` and `1600`.
        /// 
        /// </summary>
        public string Maxwidth { get; set; }

        public Dictionary<string, string?> ToDictionary()
        {
            return new Dictionary<string, string?>
            {
                {
                    "photo_reference",
                    PhotoReference
                },
                {
                    "maxheight",
                    Maxheight
                },
                {
                    "maxwidth",
                    Maxwidth
                }
            };
        }
    }
}