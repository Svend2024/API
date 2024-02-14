using KameGameAPI.DTO;
using KameGameAPI.Interfaces;
using KameGameAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KameGameAPI.Controllers
{
    public class CustomersController : BaseEntitiesController<Customer>
    {
        ILoginService _loginContext;
        public CustomersController(IBaseService<Customer> context, ILoginService loginContext) : base(context) 
        {
            _loginContext = loginContext;
        }        
    }
}
