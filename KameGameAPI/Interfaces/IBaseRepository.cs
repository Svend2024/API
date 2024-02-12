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
        Task<int> GetTotalCountAsync();
        Task<(IEnumerable<Card> results, int totalCount)> FilterSearchAsyncRepository(string? searchTerm = null, string? type = null, string? attribute = null, string? race = null, int page = 1, int pageSize = 8);
    }
}
