using KameGameAPI.DTO;
using KameGameAPI.Interfaces;
using KameGameAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KameGameAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginsController : ControllerBase
    {
        ILoginService _loginContext;
        IBaseService<Login> _context;
        public LoginsController(ILoginService loginContext, IBaseService<Login> context) 
        {
            _loginContext = loginContext;
            _context = context;
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

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateLogin(int id, Login login)
        {
            // bool
            if (id != login.id)
            {
                return BadRequest("id != entity.id");
            }
            if (await _context.UpdateEntityService(id, login))
            {
                return NoContent();
            }
            return NotFound("EntityExists(id) false");
        }
    }
}
