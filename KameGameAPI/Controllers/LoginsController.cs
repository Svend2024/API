using KameGameAPI.Interfaces;
using KameGameAPI.Models;

namespace KameGameAPI.Controllers
{
    public class LoginsController : BaseEntitiesController<Login>
    {
        public LoginsController(IBaseService<Login> context) : base(context) { }
    }
}
