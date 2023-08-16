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
         
        public DbSet<Product> Products { get; set; }
        public  DbSet<Departament> Departaments { get; set;}
        public DbSet<Category> Categories { get; set; }
        public  DbSet<Employee> Employees { get; set; }
    }
}
