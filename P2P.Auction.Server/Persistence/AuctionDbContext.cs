using Microsoft.EntityFrameworkCore;
using P2P.Auction.Server.Model;

namespace P2P.Auction.Server.Persistence
{
    public class AuctionDbContext : DbContext
    {
        public DbSet<Item> Items { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<Offer> Offers { get; set; }

        public string DbPath { get; }

        public AuctionDbContext()
        {
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            DbPath = Path.Join(path, "auction.db");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options) => options.UseSqlite($"Data Source={DbPath}");
    }
}
