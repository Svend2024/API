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
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _context;
        private readonly JwtConfig _jwtSettings;
        
        public CustomerService(ICustomerRepository context, IOptions<JwtConfig> jwtSettings)
        {
            _context = context;
            _jwtSettings = jwtSettings.Value;
        }

        public async Task<LoginResponse> LoginCustomerService(string username, string password)
        {
            Customer customer = await _context.LoginCustomerRepository(username, password);
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSettings.Secret);     // encode secretkey
            var tokenDescriptor = new SecurityTokenDescriptor // token attributter
            {
                // payload
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("id", customer.customerId.ToString()),
                }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return new LoginResponse() { token = tokenHandler.WriteToken(token) };
        }
    }
}
