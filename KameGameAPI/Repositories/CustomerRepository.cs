using KameGameAPI.Database;
using KameGameAPI.Interfaces;
using KameGameAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace KameGameAPI.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly DatabaseContext _context;

        public CustomerRepository(DatabaseContext context)
        {
            _context = context;
        }
        public async Task<Customer> LoginCustomerRepository(string username, string password)
        {
            return await _context.customers.Where(c => c.login.username  == username && c.login.password == password).FirstOrDefaultAsync();
        }
    }
}
