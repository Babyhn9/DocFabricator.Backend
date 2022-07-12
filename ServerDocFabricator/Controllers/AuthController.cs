using Microsoft.AspNetCore.Mvc;
using ServerDocFabricator.BL.Services.Interfaces;
using ServerDocFabricator.DAL.Models.Requests;
using ServerDocFabricator.Server.Responces;
using ServerDocFabricator.Utils;

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
            var id = _authBl.Authorize(request.Email, request.Password);
            if (id != Guid.Empty)
                return Ok(new AuthorizationResponce { Token = _tokenBl.Generate(id) });

            return BadRequest();
        }

        [HttpPost("signup")]
        public ActionResult<AuthorizationResponce> Register(RegisterRequest request)
        {
            if (_authBl.IsRegistered(request.Email))
                return BadRequest();

            var result = _authBl
                .Register(request.Email, request.Password);

            return Ok(new AuthorizationResponce { Token = _tokenBl.Generate(result) });
        }

        //[HttpGet("me")]
        //[JwtAuth]
        //public ActionResult Me()
        //{
        //    return Ok($"{Identity.Id}:{Identity.Email}:{Identity.Password}");
        //}
    }
}
