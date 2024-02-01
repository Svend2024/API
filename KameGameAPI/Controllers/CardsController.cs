using KameGameAPI.Interfaces;
using KameGameAPI.Models;
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
    }
}
