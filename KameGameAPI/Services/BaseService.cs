using KameGameAPI.Interfaces;
using KameGameAPI.Models;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Core.Types;

namespace KameGameAPI.Services
{
    public class BaseService<T> : IBaseService<T> where T : BaseEntity
    {
        protected readonly IBaseRepository<T> _context;

        public BaseService(IBaseRepository<T> context)
        {
            _context = context;
        }

        public async Task<List<T>> GetEntitiesService()
        {
            return await _context.GetEntitiesRepository();
        }

        public async Task<T> GetEntityService(int id)
        {
            return await _context.GetEntityRepository(id);
        }

        public async Task<bool> UpdateEntityService(int id, T entity)
        {
            return await _context.UpdateEntityRepository(id, entity);
        }

        public async Task CreateEntityService(T entity)
        {
            await _context.CreateEntityRepository(entity);
        }

        public async Task<bool> DeleteEntityService(int id)
        {
            return await _context.DeleteEntityRepository(id);
        }
        public async Task<(List<T> pagedEntities, int totalCount)> GetPagedEntitiesService(int page, int pageSize)
        {
            var startIndex = (page - 1) * pageSize;
            var pagedEntities = (await _context.GetPagedAsync(startIndex, pageSize)).ToList();
            var totalCount = await _context.GetTotalCountAsync();

            return (pagedEntities, totalCount);
        }
        public async Task<(List<T> filteredEntities, int totalCount)> GetFilteredEntitiesService(string type = null, string attribute = null, string race = null, int page = 1, int pageSize = 8)
        {
            return await _context.GetFilteredEntitiesRepository(type, attribute, race, page, pageSize);
        }
    }
}
