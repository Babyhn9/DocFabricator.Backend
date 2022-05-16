using ServerDocFabricator.DAL.Entities;

namespace ServerDocFabricator.BL.Interfaces
{
    public interface ITokenCreationBl
    {
        string Generate(UserEntity userEntity);
    }
}
