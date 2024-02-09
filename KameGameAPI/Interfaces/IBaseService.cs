using KameGameAPI.Models;
using Nest;

namespace KameGameAPI.Interfaces
{
    public interface IBaseService<T> where T : BaseEntity
    {
        Task<List<T>> GetEntitiesService();
        Task<T> GetEntityService(int id);
        Task<bool> UpdateEntityService(int id, T entity);
        Task CreateEntityService(T entity);
        Task<bool> DeleteEntityService(int id);
        Task<(IEnumerable<Card> results, int totalCount)> FilterSearchAsyncService(string? searchTerm = null, string? type = null, string? attribute = null, string? race = null, int page = 1, int pageSize = 8);
    }
}
