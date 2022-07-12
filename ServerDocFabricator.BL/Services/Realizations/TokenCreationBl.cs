
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ServerDocFabricator.BL.Services.Interfaces;
using ServerDocFabricator.BL.Utils.Attributes;
using ServerDocFabricator.DAL.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ServerDocFabricator.BL.Services.Realizations
{
    [Buisness]
    public class TokenCreationBl : ITokenCreationBl
    {
        private AppSettings _settings;

        public TokenCreationBl( )
        {
        }

        public string Generate(Guid id) => GenerateToken(id);
       
        private string GenerateToken(Guid id)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("ultrasekettarkowkey");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                    {
                        new Claim("id", id.ToString()),
                    }
                ),
                Expires = DateTime.UtcNow.AddHours(3),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}   
    