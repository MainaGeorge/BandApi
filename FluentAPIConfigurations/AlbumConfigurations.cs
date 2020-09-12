using BandApi.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BandApi.FluentAPIConfigurations
{
    public class AlbumConfigurations: IEntityTypeConfiguration<Album>
    {
        public void Configure(EntityTypeBuilder<Album> builder)
        {
            builder.ToTable("Albums");

            builder.HasKey(p => p.Id);
            builder.Property(p => p.Title)
                .IsRequired()
                .HasColumnType("varchar")
                .HasMaxLength(50);
            builder.Property(p => p.Description)
                .HasMaxLength(300);

            builder.HasOne(p => p.Band)
                .WithMany(p => p.Albums)
                .HasForeignKey(p => p.BandId);
        }
    }
}
