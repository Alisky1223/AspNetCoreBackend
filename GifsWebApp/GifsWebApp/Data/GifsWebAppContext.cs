using Microsoft.EntityFrameworkCore;
using GifsWebApp.Models;

namespace GifsWebApp.Data
{
    public class GifsWebAppContext : DbContext
    {
        public GifsWebAppContext(DbContextOptions<GifsWebAppContext> options)
            : base(options)
        {
        }

        public DbSet<Gif> Gif { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Gif>()
                .Property(g => g.Id)
                .ValueGeneratedOnAdd(); // Ensure Id is auto-generated

            base.OnModelCreating(modelBuilder);
        }
    }
}
