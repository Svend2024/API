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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>().HasOne(c => c.login).WithOne().HasForeignKey<Customer>(c => c.loginId);

            modelBuilder.Entity<Customer>().HasOne(c => c.zipCodeCity).WithOne().HasForeignKey<Customer>(c => c.zipCode);

            modelBuilder.Entity<ProductManager>().HasOne(p => p.login).WithOne().HasForeignKey<ProductManager>(p => p.loginId);

            modelBuilder.Entity<Card>().HasOne(c => c.set).WithOne().HasForeignKey<Card>(c => c.setId);

            modelBuilder.Entity<TransactionHistory>().HasOne(T => T.card).WithOne().HasForeignKey<TransactionHistory>(T => T.cardId);

            modelBuilder.Entity<TransactionHistory>().HasOne(T => T.customer).WithOne().HasForeignKey<TransactionHistory>(T => T.customerId);
        }
    }
}
