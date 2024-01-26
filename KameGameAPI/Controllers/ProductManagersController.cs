using KameGameAPI.Interfaces;
using KameGameAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KameGameAPI.Controllers
{
    public class ProductManagersController : BaseEntitiesController<ProductManager>
    {
        public ProductManagersController(IBaseService<ProductManager> context) : base(context) { }
    }
}
