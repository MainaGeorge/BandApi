using BandApi.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BandApi.FluentAPIConfigurations
{
    public class BandConfigurations: IEntityTypeConfiguration<Band>
    {
        public void Configure(EntityTypeBuilder<Band> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnType("varchar");
            builder.Property(p => p.MainGenre)
                .HasMaxLength(50)
                .HasColumnType("varchar");
        }
    }
}
