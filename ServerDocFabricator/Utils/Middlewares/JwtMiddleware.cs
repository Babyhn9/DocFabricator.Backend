using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ServerDocFabricator.DAL;
using ServerDocFabricator.DAL.Entities;
using ServerDocFabricator.Utils;

namespace ColoredLive.Service.Core.Middlewares
{
    public class JwtMiddleware
    {
        private RequestDelegate _next;
        private AppSettings _settings;

        public JwtMiddleware(RequestDelegate next, IOptions<AppSettings> settings)
        {
            _next = next;
            _settings = settings.Value;
        }

        public async Task Invoke(HttpContext context, IRepository<UserEntity> users)
        {
            var token = context.Request.Headers["token"].FirstOrDefault()?.Split(" ").Last();
            
            if (token != null)
                AttachUserToContext(context, users, token);

            await _next(context);
        }

        private void AttachUserToContext(HttpContext context, IRepository<UserEntity> users, string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes("ultrasekettarkowkey");
                
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero,
                }, out var validatedToken);

                var jwt = (JwtSecurityToken)validatedToken;
                var userId = Guid.Parse(jwt.Claims.First(x => x.Type == "id").Value);
                context.Items["User"] = users.Find(userId);
            }
            catch
            {

            }
        }
    }
}
