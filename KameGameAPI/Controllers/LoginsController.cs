using KameGameAPI.DTO;
using KameGameAPI.Interfaces;
using KameGameAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KameGameAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginsController : ControllerBase
    {
        ILoginService _loginContext;
        public LoginsController(ILoginService loginContext) 
        {
            _loginContext = loginContext;
        }

        [HttpPost("Login")]
        public async Task<ActionResult<LoginResponse>> Login([FromBody] Login login)
        {
            try
            {
                if (login.username == "" || login.password == "") return BadRequest();

                return Ok(await _loginContext.LoginActionService(login.username, login.password));

            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
    }
}
