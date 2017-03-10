using System.Collections.Generic;
using StickerApp.Models;

namespace StickerApp.ApiModels
{
    public class CollectionListResponse : SuccessResponse
    {
        public List<Collection> Collections;
    }
}