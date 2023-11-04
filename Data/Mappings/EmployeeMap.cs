using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PrimeiraAPI.Model;

namespace PrimeiraAPI.Data.Mappings
{
    public class EmployeeMap : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.Property(p => p.Cpf)
            .HasMaxLength(15)
            .IsUnicode(false);

            builder.Property(p => p.Name)
            .HasMaxLength(100)
            .IsUnicode(false);

            builder.HasOne(e => e.Departament)
                   .WithMany(p=> p.Employees)
                   .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
