using System.Collections.Generic;
using StickerApp.Models;

namespace StickerApp.Responses
{
    public class StickerListResponse : SuccessResponse
    {
        public List<Sticker> Stickers;
    }
}