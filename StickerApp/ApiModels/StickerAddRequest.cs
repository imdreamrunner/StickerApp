using System.ComponentModel.DataAnnotations;

namespace StickerApp.ApiModels
{
    public class StickerAddRequest
    {
        [Required]
        public string Name;

        public string Description;

        public string Type;

        public string Tags;

        public string Author;
    }
}