using Microsoft.AspNetCore.Mvc;
using ServerDocFabricator.BL;
using ServerDocFabricator.DAL.Entities;
using ServerDocFabricator.Utils.Attributes;

namespace ServerDocFabricator.Utils
{
    [Route("[controller]")]
    [ApiController]
    [UseIdentity]
    [BindBlToUser]
    public class ProjectControllerBase : ControllerBase, IAuthorizationController<UserEntity>
    {
        public UserEntity Identity { get; set; }
        public List<IBindable> Services { get; set; } = new List<IBindable>();
    }
}
