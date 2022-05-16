using Microsoft.AspNetCore.Mvc;
using ServerDocFabricator.BL.Interfaces;
using ServerDocFabricator.DAL.Entities;
using ServerDocFabricator.DAL.Models.Requests;
using ServerDocFabricator.DAL.Models.Responces;
using ServerDocFabricator.Utils;
using ServerDocFabricator.Utils.Attributes;

namespace ServerDocFabricator.Controllers
{
    [Route("[controller]")]
    public class AuthController : ProjectControllerBase
    {
        private IAuthhorizeBl _authBl;
        private ITokenCreationBl _tokenBl;

        public AuthController(IAuthhorizeBl authBl, ITokenCreationBl tokenBl)
        {
            _authBl = authBl;
            _tokenBl = tokenBl;
        }

        [HttpPost("signin")]
        public ActionResult<AuthorizationResponce> Auth(LoginRequest request)
        {
            var user = _authBl.Authorize(request.Email, request.Password);
            if (!user.IsEmpty)
                return Ok(new AuthorizationResponce { Token = _tokenBl.Generate(user) });
            
            return BadRequest();
        }

        [HttpPost("signup")]
        public ActionResult<AuthorizationResponce> Register(RegisterRequest request)
        {
            if (_authBl.IsRegistered(request.Email))
                return BadRequest();

            var result = _authBl
                .Register(new UserEntity { Email = request.Email, Password = request.Password });

            return Ok(new AuthorizationResponce { Token = _tokenBl.Generate(result) });
        }

        [HttpGet("me")]
        [JwtAuth]
        public ActionResult Me()
        {
            return Ok($"{Identity.Id}:{Identity.Email}:{Identity.Password}");
        }
    }
}
