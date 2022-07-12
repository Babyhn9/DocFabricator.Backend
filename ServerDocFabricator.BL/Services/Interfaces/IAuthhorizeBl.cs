using ServerDocFabricator.DAL.Entities;

namespace ServerDocFabricator.BL.Services.Interfaces
{
    public interface IAuthhorizeBl
    {
        Guid Authorize(string email, string password);
        Guid Register(string email, string password);
        bool IsRegistered(string email);
    }
}
