using KameGameAPI.Interfaces;
using KameGameAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KameGameAPI.Controllers
{
    public class SetsController : BaseEntitiesController<Set>
    {
        public SetsController(IBaseService<Set> context) : base(context) { }
    }
}
