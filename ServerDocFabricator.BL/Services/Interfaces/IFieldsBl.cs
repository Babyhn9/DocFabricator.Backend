using ServerDocFabricator.BL.DTO;

namespace ServerDocFabricator.BL.Services.Interfaces

{
    public interface IFieldsBl
    {
        void AddField(Guid templateId, CreateTemplateFieldDto info );
    }
}
