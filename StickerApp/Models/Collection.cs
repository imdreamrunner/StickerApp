namespace StickerApp.Models
{
    public partial class Collection
    {
        public Collection()
        {
        }

        public long CollectionId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Tags { get; set; }

        public string Author { get; set; }
    }
}