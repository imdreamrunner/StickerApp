using System.ComponentModel.DataAnnotations;

namespace StickerApp.Models
{
    public partial class Collection
    {
        public Collection()
        {
        }

        public long CollectionId { get; set; }

        [Required, StringLength(10, MinimumLength = 5), MinLength(5)]
        public string Name { get; set; }

        public string Description { get; set; }

        public string Tags { get; set; }

        public string Author { get; set; }
    }
}