using KameGameAPI.Interfaces;
using KameGameAPI.Models;
using Microsoft.EntityFrameworkCore;
using Nest;
using NuGet.Protocol.Core.Types;
using System.Diagnostics;
using System.Drawing.Printing;
using System;

namespace KameGameAPI.Services
{
    public class BaseService<T> : IBaseService<T> where T : BaseEntity
    {
        private readonly IElasticClient _elasticClient;
        protected readonly IBaseRepository<T> _context;

        public BaseService(IBaseRepository<T> context, IElasticClient elasticClient)
        {
            _context = context;
            _elasticClient = elasticClient;
        }

        public async Task<ISearchResponse<T>> SearchAsync(Func<SearchDescriptor<T>, ISearchRequest> selector) =>
               await _elasticClient.SearchAsync(selector);

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
        public async Task<(IEnumerable<Card> results, int totalCount)> FilterSearchAsyncService(string? searchTerm = null, string? type = null, string? attribute = null, string? race = null, int page = 1, int pageSize = 8)
        {
            return await _context.FilterSearchAsyncRepository(searchTerm, type, attribute, race, page, pageSize);
        }
    }
}
