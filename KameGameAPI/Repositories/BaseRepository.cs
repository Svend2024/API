using KameGameAPI.Database;
using KameGameAPI.Interfaces;
using KameGameAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace KameGameAPI.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        private readonly DatabaseContext _context;
        public BaseRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<List<T>> GetEntitiesRepository()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<T> GetEntityRepository(int id)
        {
            return await _context.Set<T>().FindAsync(id);

        }

        public async Task<bool> UpdateEntityRepository(int id, T entity)
        {
            if (!await EntityExists(id))
            {
                return false;
            }

            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task CreateEntityRepository(T entity)
        {
            await _context.Set<T>().AddAsync(entity); // Intellisence nonsense (T)
            await _context.SaveChangesAsync();
            // HMM, what to do here?
        }

        public async Task<bool> DeleteEntityRepository(int id)
        {
            var entity = await _context.Set<T>().FindAsync(id);

            if (entity == null)
            {
                return false;
            }

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
