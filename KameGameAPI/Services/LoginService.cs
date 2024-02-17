using KameGameAPI.DTO;
using KameGameAPI.Interfaces;
using KameGameAPI.Models;
using KameGameAPI.Security;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace KameGameAPI.Services
{
    public class LoginService : ILoginService
    {
        private readonly ILoginRepository _context;
        private readonly JwtConfig _jwtSettings;

        public LoginService(ILoginRepository context, IOptions<JwtConfig> jwtSettings)
        {
            _context = context;
            _jwtSettings = jwtSettings.Value;
        }

        public async Task<LoginResponse> LoginActionService(string username, string password)
        {
            object loginResult = await _context.LoginActionRepository(username, password);            
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSettings.Secret);     // encode secretkey
            var tokenDescriptor = new SecurityTokenDescriptor // token attributter
            {
                // payload
                Subject = new ClaimsIdentity(new Claim[]
                {
                    loginResult is Customer customer ? new Claim("id", customer.id.ToString()) :

                    loginResult is ProductManager productManager ? new Claim("id", productManager.id.ToString()) :
                    throw new InvalidOperationException("kan ikke ramme det her?")
                }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return new LoginResponse() { token = tokenHandler.WriteToken(token), rolle = loginResult is Customer ? "Customer" : "ProductManager" };
        }
    }
}
