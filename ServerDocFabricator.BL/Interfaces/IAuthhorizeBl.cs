using ServerDocFabricator.DAL.Entities;

namespace ServerDocFabricator.BL.Interfaces
{
    public interface IAuthhorizeBl
    {
        UserEntity Authorize(string email, string password);
        UserEntity Register(UserEntity user);
        bool IsRegistered(string email);
    }
}
