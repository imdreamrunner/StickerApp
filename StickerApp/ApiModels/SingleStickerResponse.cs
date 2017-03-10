using StickerApp.Models;

namespace StickerApp.ApiModels
{
    public class SingleStickerResponse : SuccessResponse
    {
        public SingleStickerResponse(Sticker sticker)
        {
            this.Sticker = sticker;
        }

        public Sticker Sticker;
    }
}