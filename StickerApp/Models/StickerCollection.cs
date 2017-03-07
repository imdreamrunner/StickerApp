namespace StickerApp.Models
{
    public partial class StickerCollection
    {
        public StickerCollection()
        {

        }

        public long StickerId { get; set; }

        public long CollectionId { get; set; }

        public int Sort { get; set; }
    }
}