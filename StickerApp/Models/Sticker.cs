using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using StickerApp.Misc;

namespace StickerApp.Models
{
    public partial class Sticker
    {
        public Sticker()
        {

        }

        public long StickerId { get; set; }

        [Required, StringLength(10, MinimumLength = 5)]
        public string Name { get; set; }

        [JsonIgnore, Required]
        public int Type { get; set; }

        [JsonIgnore, NotMapped]
        public Constants.StickerType StickerType
        {
            get
            {
                if (Enum.IsDefined(typeof(Constants.StickerType), this.Type))
                {
                    return (Constants.StickerType) this.Type;
                }
                else
                {
                    throw new StickerAppException("InvalidStickerType");
                }
            }
            set
            {
                if (Enum.IsDefined(typeof(Constants.StickerType), (int) value))
                {
                    Type = (int) value;
                }
                else
                {
                    throw new StickerAppException("InvalidStickerType");
                }
            }
        }

        [JsonProperty("type"), NotMapped]
        public string StickerTypeString
        {
            get
            {
                var type = this.StickerType;
                return type.ToString().ToUpper();
            }

            set
            {
                Constants.StickerType type;
                var parseSuccess = Enum.TryParse(value, true, out type);
                if (parseSuccess)
                {
                    this.StickerType = type;
                }
                else
                {
                    throw new StickerAppException("InvalidStickerType");
                }
            }
        }

        public string Description { get; set; }

        public string Tags { get; set; }

        public string Author { get; set; }
    }
}