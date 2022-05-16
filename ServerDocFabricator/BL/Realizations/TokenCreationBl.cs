
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ServerDocFabricator.BL.Interfaces;
using ServerDocFabricator.DAL.Entities;
using ServerDocFabricator.Utils;
using ServerDocFabricator.Utils.Attributes;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ServerDocFabricator.BL.Realizations
{
    [Buisness]
    public class TokenCreationBl : ITokenCreationBl
    {
        private AppSettings _settings;

        public TokenCreationBl(IOptions<AppSettings> settings)
        {
            _settings = settings.Value;
        }

        public string Generate(UserEntity user) => GenerateToken(user.Id);
       
        private string GenerateToken(Guid id)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_settings.SecretKey);
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
    