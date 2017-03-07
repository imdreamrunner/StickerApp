using System;
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

        public string Name { get; set; }

        [JsonIgnore]
        public int Type { get; set; }

        [JsonIgnore]
        [NotMapped]
        public Constants.StickerType StickerType
        {
            get
            {
                return (Constants.StickerType) this.Type;
            }
            set
            {
                Type = (int) value;
            }
        }

        [JsonProperty("type")]
        [NotMapped]
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
                    this.Type = (int) type;
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