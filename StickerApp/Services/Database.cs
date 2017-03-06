using Microsoft.EntityFrameworkCore;
using StickerApp.Models;

namespace StickerApp.Services
{
    public class Database : DbContext
    {
        public Database(DbContextOptions options) : base(options) {}

        public DbSet<Sticker> Stickers { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}