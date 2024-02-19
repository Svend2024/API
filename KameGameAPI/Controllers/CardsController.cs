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

        public CardsController(IBaseService<Card> context) : base(context)
        {
            _context = context;
        }
        [HttpGet("FilterSearch")]
        public async Task<IActionResult> SearchAsync([FromQuery] string? searchTerm, [FromQuery] string? type, [FromQuery] string? attribute, [FromQuery] string? race, [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            try
            {
                var (searchRes, totalCount) = await _context.FilterSearchAsyncService(searchTerm, type, attribute, race, page, pageSize);
                var result = new
                {
                    SearchedEnities = searchRes,
                    TotalCount = totalCount
                };

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        public override async Task<IActionResult> UpdateEntity(int id, Card entity)
        {
            return await base.UpdateEntity(id, entity);
        }

        [Authorize]
        public override async Task<IActionResult> CreateEntity(Card entity)
        {
            return await base.CreateEntity(entity);
        }

        [Authorize]
        public override async Task<IActionResult> DeleteEntity(Card entity)
        {
            return await base.DeleteEntity(entity);
        }
    }
}
