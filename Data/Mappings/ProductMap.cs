using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PrimeiraAPI.Model;

namespace PrimeiraAPI.Data.Mappings
{
    public class ProductMap : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(p => p.Name)
            .HasMaxLength(100)
            .IsUnicode(false);

            builder.Property(p => p.Description)
            .HasMaxLength(200)
            .IsUnicode(false);



        }
    }
}
