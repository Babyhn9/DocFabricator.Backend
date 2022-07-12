using ServerDocFabricator.BL.Services.Interfaces;
using ServerDocFabricator.BL.Utils.Attributes;
using ServerDocFabricator.DAL;
using ServerDocFabricator.DAL.Entities;

namespace ServerDocFabricator.BL.Services.Realizations
{
    [Buisness]
    public class AuthorizeBl : IAuthhorizeBl
    {
        private IRepository<UserEntity> _users;

        public AuthorizeBl(IRepository<UserEntity> users)
        {
            _users = users;
        }
        public Guid Authorize(string email, string password)
        {
            var user = _users.Find(x => x.Email == email);
            if (!user.IsEmpty && user.Password == password)
            {
                return user.Id;
            }
            return Guid.Empty;
        }

        public bool IsRegistered(string email)
        {
            var findedUser = _users.Find(x => x.Email == email);
            return !findedUser.IsEmpty;
        }
    
        public Guid Register(string email, string password)
        {
            if (!IsRegistered(email))
               return _users.Add(new UserEntity { Email = email, Password = password}).Id;
                
            return Guid.Empty;
        }
    }
}
