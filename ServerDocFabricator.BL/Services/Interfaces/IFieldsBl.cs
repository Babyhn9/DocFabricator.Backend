using ServerDocFabricator.BL.Models;

namespace ServerDocFabricator.BL.Services.Interfaces

{
    public interface IFieldsBl
    {
        void AddField(Guid templateId, CreateTemplateFieldModel info );
    }
}
