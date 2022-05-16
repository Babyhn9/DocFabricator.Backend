using ServerDocFabricator.BL.Interfaces;
using ServerDocFabricator.DAL;
using ServerDocFabricator.DAL.Entities;
using ServerDocFabricator.Utils.Attributes;

namespace ServerDocFabricator.BL.Realizations
{
    [Buisness]
    public class AuthorizeBl : IAuthhorizeBl
    {
        private IRepository<UserEntity> _users;

        public AuthorizeBl(IRepository<UserEntity> users)
        {
            _users = users;
        }
        public UserEntity Authorize(string email, string password)
        {
            var user = _users.Find(x => x.Email == email);
            if (!user.IsEmpty && user.Password == password)
            {
                return user;
            }
            return new UserEntity();
        }

        public bool IsRegistered(string email)
        {
            var findedUser = _users.Find(x => x.Email == email);
            return !findedUser.IsEmpty;
        }
    
        public UserEntity Register(UserEntity user)
        {
            if (!IsRegistered(user.Email))
               return _users.Add(user);
                
            return new UserEntity();
        }
    }
}
