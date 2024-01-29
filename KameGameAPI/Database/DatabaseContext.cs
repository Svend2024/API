using KameGameAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace KameGameAPI.Database
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
            
        }
        public DbSet<ZipCodeCity> zipCodeCities { get; set; }
        public DbSet<Login> logins { get; set; }
        public DbSet<Customer> customers { get; set; }
        public DbSet<ProductManager> productManagers { get; set; }
        public DbSet<Set> sets { get; set; }
        public DbSet<Card> cards { get; set; }
        public DbSet<TransactionHistory> transactionHistories { get; set; }

    }
}
