using DataAccessLayer.EntityModels;
using System.Data.Entity;

namespace DataAccessLayer.ContextModels
{
    public class SalesDBContext : DbContext
    {
        public SalesDBContext() : base("DBConnection")
        {
           
        }

        public DbSet<Manager> Managers { get; set; }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<Sale> Sales { get; set; }

    }
}
