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
            modelBuilder.Entity<Customer>().HasOne(c => c.login).WithOne().HasForeignKey<Customer>(c => c.loginId).HasPrincipalKey<Login>(l => l.loginId).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<ProductManager>().HasOne(p => p.login).WithOne().OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Card>().HasOne(c => c.set).WithOne().HasForeignKey<Card>(s => s.setCode).HasPrincipalKey<Set>(s => s.setCode).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
