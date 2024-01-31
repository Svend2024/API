using KameGameAPI.Database;
using KameGameAPI.Interfaces;
using KameGameAPI.Models;
using KameGameAPI.Security;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.EntityFrameworkCore;

namespace KameGameAPI.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        private readonly DatabaseContext _context;
        private readonly IDataProtector _dataProtector;
        public BaseRepository(DatabaseContext context, IDataProtectionProvider dataProtectionProvider)
        {
            _context = context;
            _dataProtector = dataProtectionProvider.CreateProtector("InformationProtection");
        }

        public async Task<List<T>> GetEntitiesRepository()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<T> GetEntityRepository(int id)
        {
            T entity = await _context.Set<T>().FindAsync(id);
            if (entity is Customer)
            {
                Customer customer = await _context.customers.Include(c => c.login).Where(c => c.customerId == id).FirstOrDefaultAsync();
                customer.fullname = _dataProtector.Unprotect(customer.fullname);
                customer.email = _dataProtector.Unprotect(customer.email);
                customer.address = _dataProtector.Unprotect(customer.address);
                customer.login.username = _dataProtector.Unprotect(customer.login.username);
                customer.login.password = customer.login.password;
                return (T)(object)customer;
            }
            else if (entity is ProductManager)
            {
                ProductManager productManager = await _context.productManagers.Include(c => c.login).Where(c => c.productManagerId == id).FirstOrDefaultAsync();
                productManager.fullname = _dataProtector.Unprotect(productManager.fullname);
                productManager.login.username = _dataProtector.Unprotect(productManager.login.username);
                productManager.login.password = productManager.login.password;
                return (T)(object)productManager;
            }
            else if (entity is Card)
            {
                Card card = await _context.cards.Include(c => c.set).Where(c => c.cardId == id).FirstOrDefaultAsync();
                return (T)(object)card;
            }
            else return entity;
        }

        public async Task<bool> UpdateEntityRepository(int id, T entity)
        {
            if (!await EntityExists(id))
            {
                return false;
            }

            if (entity is Customer)
            {
                Customer customer = (Customer)(object)entity;
                customer.fullname = _dataProtector.Protect(customer.fullname);
                customer.email = _dataProtector.Protect(customer.email);
                customer.address = _dataProtector.Protect(customer.address);
                customer.login.username = _dataProtector.Protect(customer.login.username);
                customer.login.password = SALT.Hashing(customer.login.password);
                _context.Entry(customer).State = EntityState.Modified;
            }
            else if (entity is ProductManager)
            {
                ProductManager productManager = (ProductManager)(object)entity;
                productManager.fullname = _dataProtector.Protect(productManager.fullname);
                productManager.login.username = _dataProtector.Protect(productManager.login.username);
                productManager.login.password = SALT.Hashing(productManager.login.password);
                _context.Entry(productManager).State = EntityState.Modified;
            } 
            else _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task CreateEntityRepository(T entity)
        {

            if (entity is Customer)
            {
                Customer customer = (Customer)(object)entity;
                customer.fullname = _dataProtector.Protect(customer.fullname);
                customer.email = _dataProtector.Protect(customer.email);
                customer.address = _dataProtector.Protect(customer.address);
                customer.login.username = _dataProtector.Protect(customer.login.username);
                customer.login.password = SALT.Hashing(customer.login.password);
                await _context.customers.AddAsync(customer);
            }
            else if (entity is ProductManager)
            {
                ProductManager productManager = (ProductManager)(object)entity;
                productManager.fullname = _dataProtector.Protect(productManager.fullname);
                productManager.login.username = _dataProtector.Protect(productManager.login.username);
                productManager.login.password = SALT.Hashing(productManager.login.password);
                await _context.productManagers.AddAsync(productManager);
            }
            else
            {
                await _context.Set<T>().AddAsync(entity);
            }
            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteEntityRepository(int id)
        {
            var entity = await _context.Set<T>().FindAsync(id);
            if (entity == null)
            {
                return false;
            }
            //else if(entity is Customer)
            //{
            //    Customer customer = await _context.customers.Include(c => c.login).Where(c => c.customerId == id).FirstOrDefaultAsync();
            //    _context.customers.Remove(customer);

            //}
            //else if(entity is ProductManager)
            //{

            //} 
            //else if(entity is Card)
            //{

            //}
            //else _context.Set<T>().Remove(entity);
            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();

            return true;
        }

        private Task<bool> EntityExists(int id)
        {
            //return (_context.logins?.Any(e => e.loginId == id)).GetValueOrDefault();            
            return Task.FromResult((_context.Set<T>()?.Any(e => e.id == id)).GetValueOrDefault());
        }
    }
}
