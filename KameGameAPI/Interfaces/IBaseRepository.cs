using KameGameAPI.Models;
using Microsoft.CodeAnalysis.Elfie.Serialization;
using Nest;

namespace KameGameAPI.Interfaces
{
    public interface IBaseRepository<T> where T : BaseEntity
    {
        Task<List<T>> GetEntitiesRepository();
        Task<T> GetEntityRepository(int id);
        Task<bool> UpdateEntityRepository(int id, T entity);
        Task CreateEntityRepository(T entity);
        Task<bool> DeleteEntityRepository(int id);
        Task<IEnumerable<T>> GetPagedAsync(int startIndex, int pageSize);
        Task<int> GetTotalCountAsync();
        Task<(List<T> filteredEntities, int totalCount)> GetFilteredEntitiesRepository(string type, string attribute, string race, int page, int pageSize);
        Task<List<Card>> SearchEntities(string searchTerm, int page, int pageSize);
    }
}
