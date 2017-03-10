using Microsoft.EntityFrameworkCore;
using StickerApp.Models;

namespace StickerApp.Services
{
    public class StickerDb : DbContext
    {
        public StickerDb(DbContextOptions options) : base(options) {}

        public DbSet<Sticker> Stickers { get; set; }
        public DbSet<Collection> Collections { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}