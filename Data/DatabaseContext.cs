using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PrimeiraAPI.Model;

namespace PrimeiraAPI.Data
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            :base(options)
        {

        }

        //protected override void Onmodelcreating(ModelBuilder modelbuilder)
        //{
        //    modelbuilder.Entity<Employee>()
        //        .HasOne(e => e.Departament)
        //        .WithMany()
        //        .OnDelete(DeleteBehavior.ClientSetNull);

        //    base.OnModelCreating(modelbuilder);
        //}

        public DbSet<Product> Products { get; set; }
        public  DbSet<Department> Departaments { get; set;}
        public DbSet<Category> Categories { get; set; }
        public  DbSet<Employee> Employees { get; set; }
    }
}
