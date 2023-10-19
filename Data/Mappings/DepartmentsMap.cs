using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PrimeiraAPI.Model;

namespace PrimeiraAPI.Data.Mappings
{

    public class DepartmentsMap : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> builder) 
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
