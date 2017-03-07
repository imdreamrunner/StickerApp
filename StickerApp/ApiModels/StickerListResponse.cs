using System.Collections.Generic;
using StickerApp.Models;

namespace StickerApp.ApiModels
{
    public class StickerListResponse : SuccessResponse
    {
        public List<Sticker> Stickers;
    }
}