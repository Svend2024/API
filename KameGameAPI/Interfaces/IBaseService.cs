using KameGameAPI.Models;

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
    }
}
