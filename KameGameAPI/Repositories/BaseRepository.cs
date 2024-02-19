using KameGameAPI.Database;
using KameGameAPI.Interfaces;
using KameGameAPI.Models;
using KameGameAPI.Security;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.EntityFrameworkCore;
using Nest;

namespace KameGameAPI.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        private readonly DatabaseContext _context;
        public readonly IDataProtector _dataProtector;


        public BaseRepository(DatabaseContext context, IDataProtectionProvider dataProtectionProvider)
        {
            _context = context;
            _dataProtector = dataProtectionProvider.CreateProtector("InformationProtection");
        }

        public async Task<List<T>> GetEntitiesRepository()
        {
            List<T> entity = await _context.Set<T>().ToListAsync();
            if (entity is List<Card>)
            {
                List<Card> cards = await _context.cards.Include(c => c.set).ToListAsync();
                return (List<T>)(object)cards;
            }
            else return entity;
        }

        public async Task<T> GetEntityRepository(int id)
        {
            T entity = await _context.Set<T>().FindAsync(id);
            if (entity is Customer)
            {
                Customer customer = await _context.customers.Include(c => c.login).Where(c => c.id == id).FirstOrDefaultAsync();
                customer.fullname = _dataProtector.Unprotect(customer.fullname);
                customer.email = _dataProtector.Unprotect(customer.email);
                customer.address = _dataProtector.Unprotect(customer.address);
                return (T)(object)customer;
            }
            else if (entity is ProductManager)
            {
                ProductManager productManager = await _context.productManagers.Include(c => c.login).Where(c => c.id == id).FirstOrDefaultAsync();
                productManager.fullname = _dataProtector.Unprotect(productManager.fullname);
                return (T)(object)productManager;
            }
            else if (entity is Card)
            {
                Card card = await _context.cards.Include(c => c.set).Where(c => c.id == id).FirstOrDefaultAsync();
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
                _context.Entry(customer).State = EntityState.Modified;
            }
            else if (entity is ProductManager)
            {
                ProductManager productManager = (ProductManager)(object)entity;
                productManager.fullname = _dataProtector.Protect(productManager.fullname);
                _context.Entry(productManager).State = EntityState.Modified;
            }
            else if (entity is Login)
            {
                Login login = (Login)(object)entity;
                login.password = SALT.Hashing(login.password);
                _context.Entry(login).State = EntityState.Modified;
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
                customer.login.password = SALT.Hashing(customer.login.password);
                await _context.customers.AddAsync(customer);
            }
            else if (entity is ProductManager)
            {
                ProductManager productManager = (ProductManager)(object)entity;
                productManager.fullname = _dataProtector.Protect(productManager.fullname);
                productManager.login.password = SALT.Hashing(productManager.login.password);
                await _context.productManagers.AddAsync(productManager);
            }
            else
            {
                await _context.Set<T>().AddAsync(entity);
            }
            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteEntityRepository(T entity)
        {
            var localEntity = await _context.Set<T>().FindAsync(entity.id);
            if (localEntity == null)
            {
                return false;
            }
            else if (localEntity is Customer)
            {
                Customer customer = await _context.customers.Include(c => c.login).Where(c => c.id == entity.id).FirstOrDefaultAsync();
                _context.logins.Remove(customer.login);
                _context.customers.Remove(customer);

            }
            else if (localEntity is ProductManager)
            {
                ProductManager productManager = await _context.productManagers.Include(p => p.login).Where(c => c.id == entity.id).FirstOrDefaultAsync();
                _context.logins.Remove(productManager.login);
                _context.productManagers.Remove(productManager);
            }
            else if (localEntity is Card)
            {
                Card card = await _context.cards.Include(p => p.set).Where(c => c.id == entity.id).FirstOrDefaultAsync();
                _context.sets.Remove(card.set);
                _context.cards.Remove(card);
            }
            else _context.Set<T>().Remove(localEntity);
            await _context.SaveChangesAsync();

            return true;
        }

        private Task<bool> EntityExists(int id)
        {
            //return (_context.logins?.Any(e => e.loginId == id)).GetValueOrDefault();            
            return Task.FromResult((_context.Set<T>()?.Any(e => e.id == id)).GetValueOrDefault());
        }
        public async Task<int> GetTotalCountAsync()
        {
            return await _context.Set<T>().CountAsync();
        }
        public async Task<(IEnumerable<Card> results, int totalCount)> FilterSearchAsyncRepository(string? searchTerm = null, string? type = null, string? attribute = null, string? race = null, int page = 1, int pageSize = 4)
        {
            var query = _context.cards.AsQueryable();

            // Apply filters based on provided parameters
            if (!string.IsNullOrEmpty(type))
                query = query.Where(e => e.type == type);

            if (!string.IsNullOrEmpty(attribute))
                query = query.Where(e => e.attribute == attribute);

            if (!string.IsNullOrEmpty(race))
                query = query.Where(e => e.race == race);

            // Filter by name or code if searchTerm is provided
            if (!string.IsNullOrEmpty(searchTerm))
            {
                searchTerm = searchTerm.ToLower(); // Convert search term to lowercase for case-insensitive search
                query = query.Where(c => c.name.ToLower().Contains(searchTerm) || c.cardCode.ToLower().Contains(searchTerm));
            }

            // Count total number of matching records
            var totalCount = await query.CountAsync();

            // Pagination
            var startIndex = (page - 1) * pageSize;
            query = query.Skip(startIndex).Take(pageSize);

            // Execute the query
            var results = await query.ToListAsync();

            return (results, totalCount);
        }
    }
}
