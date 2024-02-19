using KameGameAPI.Interfaces;
using KameGameAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KameGameAPI.Controllers
{
    public class ProductManagersController : BaseEntitiesController<ProductManager>
    {
        public ProductManagersController(IBaseService<ProductManager> context) : base(context) { }

        [Authorize]
        public override async Task<IActionResult> UpdateEntity(int id, ProductManager entity)
        {
            return await base.UpdateEntity(id, entity);
        }
    }
}
