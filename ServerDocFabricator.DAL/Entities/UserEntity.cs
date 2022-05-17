using ServerDocFabricator.DAL;

namespace ServerDocFabricator.DAL.Entities
{
 
    public class UserEntity : Entity
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
