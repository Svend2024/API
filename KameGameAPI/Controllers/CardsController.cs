using KameGameAPI.Interfaces;
using KameGameAPI.Models;
using KameGameAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KameGameAPI.Controllers
{
    //[Authorize]
    public class CardsController : BaseEntitiesController<Card>
    {
        IBaseService<Card> _context;

        public CardsController(IBaseService<Card> context) : base(context)
        {
            _context = context;
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
    }
}
