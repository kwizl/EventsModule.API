using EventsModule.API.Extensions;
using EventsModule.API.Interfaces;
using EventsModule.Data.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EventsModule.API.Services
{
    public class BearerToken : IBearerToken
    {
        private readonly SymmetricSecurityKey _key;

        public BearerToken()
        {
            string key = GetToken.Key();
            // Transforms the key to bytes
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
        }

        // Create Jwt Token
        public string CreateToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.NameId, user.UserName!)
            };

            var credentials = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddHours(5),
                SigningCredentials = credentials
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
