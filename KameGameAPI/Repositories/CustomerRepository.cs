using KameGameAPI.Database;
using KameGameAPI.Interfaces;
using KameGameAPI.Models;
using KameGameAPI.Security;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.EntityFrameworkCore;

namespace KameGameAPI.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly DatabaseContext _context;
        public readonly IDataProtector _dataProtector;

        public CustomerRepository(DatabaseContext context, IDataProtectionProvider dataProtectionProvider)
        {
            _context = context;
            _dataProtector = dataProtectionProvider.CreateProtector("InformationProtection");
        }
        public async Task<Customer> LoginCustomerRepository(string username, string password)
        {
            return await _context.customers.Where(c => c.login.username  == username && c.login.password == SALT.Hashing(password)).FirstOrDefaultAsync();
        }
    }
}
