using KameGameAPI.DTO;
using KameGameAPI.Interfaces;
using KameGameAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KameGameAPI.Controllers
{
    public class CustomersController : BaseEntitiesController<Customer>
    {
        ICustomerService _loginContext;
        public CustomersController(IBaseService<Customer> context, ICustomerService loginContext) : base(context) 
        {
            _loginContext = loginContext;
        }        

        [HttpPost("Login")]
        public async Task<ActionResult<LoginResponse>> Login([FromBody] Login login)
        {
            try
            {
                if (login.username == "" || login.password == "") return BadRequest();

                return Ok(await _loginContext.LoginCustomerService(login.username, login.password));

            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
    }
}
