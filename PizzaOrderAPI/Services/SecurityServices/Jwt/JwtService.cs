using Microsoft.IdentityModel.Tokens;
using PizzaOrderAPI.Data.Services.ConfigurationServices;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace PizzaOrderAPI.Services.SecurityServices.Jwt
{
    public class JwtService : IJwtService
    {
        private readonly IConfiguration _configuration;
        public JwtService(IConfiguration configuration)

        {
            _configuration = configuration;
        }
        public string CreateToken(string role)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Role,role),
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_configuration["JwtConfig:Key"]));


            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

            var token = new JwtSecurityToken(
                audience: _configuration["JwtConfig:Audience"],
                issuer: _configuration["JwtConfig:Issuer"],
                claims: claims,
                expires: DateTime.Now.AddMonths(1),
                signingCredentials: creds
                );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }
    }
}
