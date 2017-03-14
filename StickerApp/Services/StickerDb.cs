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

            foreach (var entity in builder.Model.GetEntityTypes())
            {
                var lastDot = entity.Name.LastIndexOf(".");
                var tableName = entity.Name.Substring(lastDot + 1).ToLower();
                entity.Relational().TableName = tableName;
            }
        }
    }
}