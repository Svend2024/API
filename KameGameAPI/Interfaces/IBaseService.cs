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
        Task<(List<T> pagedEntities, int totalCount)> GetPagedEntitiesService(int page, int pageSize);
        Task<(List<T> filteredEntities, int totalCount)> GetFilteredEntitiesService(string type = null, string attribute = null, string race = null, int page = 1, int pageSize = 8);
        Task<ISearchResponse<T>> SearchAsync(Func<SearchDescriptor<T>, ISearchRequest> selector);
    }
}
