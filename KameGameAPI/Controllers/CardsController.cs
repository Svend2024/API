using KameGameAPI.Interfaces;
using KameGameAPI.Models;
using KameGameAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nest;

namespace KameGameAPI.Controllers
{
    //[Authorize]
    public class CardsController : BaseEntitiesController<Card>
    {
        IBaseService<Card> _context;
        IElasticClient _elasticClient;

        public CardsController(IBaseService<Card> context, IElasticClient elasticClient) : base(context)
        {
            _context = context;
            _elasticClient = elasticClient;
        }

        [HttpGet("paged")]
        public async Task<IActionResult> GetPagedEntities([FromQuery] int page = 1, [FromQuery] int pageSize = 4)
        {
            try
            {
                var (pagedEntities, totalCount) = await _context.GetPagedEntitiesService(page, pageSize);
                var paginatedEntitiesList = pagedEntities.ToList(); // Convert to list

                var result = new
                {
                    PagedEntities = paginatedEntitiesList,
                    TotalCount = totalCount
                };

                return Ok(result);
            }
            catch (Exception ex)
            {
                // Handle exceptions appropriately
                return StatusCode(500, "Internal server error");
            }
        }
        [HttpGet("filtered")]
        public async Task<IActionResult> GetFilteredEntities([FromQuery] string type = null, [FromQuery] string attribute = null, [FromQuery] string race = null, [FromQuery] int page = 1, [FromQuery] int pageSize = 4)
        {
            try
            {
                var (filteredEntities, totalCount) = await _context.GetFilteredEntitiesService(type, attribute, race, page, pageSize);

                var result = new
                {
                    PagedFilteredEntities = filteredEntities,
                    TotalCount = totalCount
                };

                return Ok(result);
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(500, "Internal server error");
            }
        }
        [HttpGet("search")]
        public async Task<IActionResult> SearchEntities([FromQuery] string searchTerm, [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            try
            {
                var searchRes = await _context.SearchAsync(s => s
                    .From((page - 1) * pageSize)
                    .Size(pageSize)
                    .Query(q => q
                        .MultiMatch(m => m
                            .Fields(f => f
                                .Field(p => p.name) // Assuming Name is the property to search
                                .Field(p => p.cardCode) // Assuming CardCode is the property to search
                            )
                            .Query(searchTerm)
                        )
                    )
                );

                // Return search results
                return Ok(searchRes.Documents);
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
