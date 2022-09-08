using ServerDocFabricator.BL.DTO;
using ServerDocFabricator.BL.Services.Interfaces;
using ServerDocFabricator.DAL;
using ServerDocFabricator.DAL.Entities;

namespace ServerDocFabricator.BL.Services.Realizations
{
    public class FieldBl : IFieldsBl
    {
        private readonly IRepository<TemplateFieldEntity> _fields;
        private readonly IRepository<TemplateEntity> _templates;

        public FieldBl(IRepository<TemplateFieldEntity> fields, IRepository<TemplateEntity> templates)
        {
            _fields = fields;
            _templates = templates;
        }

        public void AddField(Guid templateId, CreateTemplateFieldDto info)
        {
            var template = _templates.Find(templateId);
            if(!template.IsEmpty)
            {
                _fields.Add(new TemplateFieldEntity
                {
                    TemplateID = templateId,
                    FieldName = info.FieldName,
                    Description = info.Description,
                });
            }
        }

    }
}
