using KameGameAPI.Interfaces;
using KameGameAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KameGameAPI.Controllers
{
    public class CustomersController : BaseEntitiesController<Customer>
    {
        public CustomersController(IBaseService<Customer> context) : base(context) { }
    }
}
