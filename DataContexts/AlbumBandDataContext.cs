using BandApi.Entities;
using BandApi.FluentAPIConfigurations;
using Microsoft.EntityFrameworkCore;

namespace BandApi.DataContexts
{
    public class AlbumBandDataContext: DbContext
    {
        public AlbumBandDataContext(DbContextOptions<AlbumBandDataContext> opt):base(opt)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new BandConfigurations());
            modelBuilder.ApplyConfiguration(new AlbumConfigurations());

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Album> Albums { get; set; }
        public DbSet<Band> Bands { get; set; }  
    }
}
