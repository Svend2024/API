using KameGameAPI.Database;
using KameGameAPI.Interfaces;
using KameGameAPI.Models;
using KameGameAPI.Security;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.EntityFrameworkCore;

namespace KameGameAPI.Repositories
{
    public class LoginRepository : ILoginRepository
    {
        private readonly DatabaseContext _context;
        public readonly IDataProtector _dataProtector;

        public LoginRepository(DatabaseContext context, IDataProtectionProvider dataProtectionProvider)
        {
            _context = context;
            _dataProtector = dataProtectionProvider.CreateProtector("InformationProtection");
        }
        public async Task<object> LoginActionRepository(string username, string password)
        {
            Customer customer =  await _context.customers.Where(c => c.login.username == username && c.login.password == SALT.Hashing(password)).FirstOrDefaultAsync();
            if (customer != null) return customer;

            ProductManager productManager = await _context.productManagers.Where(p => p.login.username == username && p.login.password == SALT.Hashing(password)).FirstOrDefaultAsync();
            if(productManager != null) return productManager;

            return null;
        }
    }
}
