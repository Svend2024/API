using KameGameAPI.Interfaces;
using KameGameAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KameGameAPI.Controllers
{
    [Authorize]
    public class CardsController : BaseEntitiesController<Card>
    {
        public CardsController(IBaseService<Card> context) : base(context) { }
    }
}
