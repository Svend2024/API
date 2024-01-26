using KameGameAPI.Interfaces;
using KameGameAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KameGameAPI.Controllers
{
    public class ZipCodeCitiesController : BaseEntitiesController<ZipCodeCity>
    {
        public ZipCodeCitiesController(IBaseService<ZipCodeCity> context) : base(context) { }
    }
}
