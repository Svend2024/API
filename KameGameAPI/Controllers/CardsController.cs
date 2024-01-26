using KameGameAPI.Interfaces;
using KameGameAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KameGameAPI.Controllers
{
    public class CardsController : BaseEntitiesController<Card>
    {
        public CardsController(IBaseService<Card> context) : base(context) { }
    }
}
