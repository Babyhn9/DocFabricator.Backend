using ServerDocFabricator.BL.Attributes;
using ServerDocFabricator.BL.Models;
using ServerDocFabricator.DAL;
using ServerDocFabricator.DAL.Entities;

namespace ServerDocFabricator.BL.Mappers
{
    [Mapper]
    public class ModelMapper :
        IModelMapper<TemplateEntity, TemplateModel>,
        IModelMapper<TemplateFieldEntity, TemplateFieldModel>
    {
        private readonly IRepository<TemplateEntity> _templates;
        private readonly IRepository<TemplateFieldEntity> _fields;

        public ModelMapper(
            IRepository<TemplateEntity> templates,
            IRepository<TemplateFieldEntity> fields
            )
        {
            _templates = templates;
            _fields = fields;
        }
        public TemplateModel Map(TemplateEntity from)
        {
            var fieldsModels =  _fields
                .FindAll(el => el.TemplateID == from.Id)
                .Select(el => Map(el))
                .ToList();


            return new TemplateModel
            {
                Id = from.Id,
                Name = from.TemplateName,
                Fields = fieldsModels,
                Settings = new TemplateSettingsModel(),
            };
        }

        public TemplateFieldModel Map(TemplateFieldEntity from)
        {
            return new TemplateFieldModel
            {
                Id = from.Id,
                FieldDescription = from.Description,
                FieldName = from.FieldName,
            };
        }
    }
}
