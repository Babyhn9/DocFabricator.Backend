using ServerDocFabricator.DAL.Entities;

namespace ServerDocFabricator.BL.Services.Interfaces

{
    public interface ITokenCreationBl
    {
        string Generate(Guid userId);
    }
}
