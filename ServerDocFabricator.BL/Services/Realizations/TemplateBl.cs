using ServerDocFabricator.BL.DocumentsEditors;
using ServerDocFabricator.BL.Mappers;
using ServerDocFabricator.BL.Models;
using ServerDocFabricator.BL.Services.Interfaces;
using ServerDocFabricator.BL.Utils.Attributes;
using ServerDocFabricator.DAL;
using ServerDocFabricator.DAL.Entities;
using System.Linq;

namespace ServerDocFabricator.BL
{
    [Buisness]
    public class TemplateBl : ITemplateBl
    {
        private readonly IRepository<TemplateEntity> _templates;
        private readonly IRepository<TemplateFieldEntity> _templateFields;
        private readonly IModelMapper<TemplateEntity, TemplateModel> _templateMapper;
        private readonly IModelMapper<TemplateFieldEntity, TemplateFieldModel> _fieldMapper;

        public TemplateBl(
            IRepository<TemplateEntity> templates,
            IRepository<TemplateFieldEntity> templateFields,
            IModelMapper<TemplateEntity, TemplateModel> templateMapper,
            IModelMapper<TemplateFieldEntity, TemplateFieldModel> fieldMapper
            )
        {
            _templates = templates;
            _templateFields = templateFields;
            _templateMapper = templateMapper;
            _fieldMapper = fieldMapper;
        }
        public TemplateModel CreateTemplate(CreateTemplateModel info)
        {
            var documentEditor = new DockIoWordEditor();
            documentEditor.AttachFile(info.File);
            var pathToFile = documentEditor.SaveToDisk(); ;

            var template = _templates.Add(new TemplateEntity
            {
                CreatedUserId = info.UserId,
                TemplateName = info.TemplateName,
                PathToFile = pathToFile,
            });

            return _templateMapper.Map(template);
        }

        public string GetFlatText(Guid templateId)
        {
            var template = _templates.Find(templateId);

            if (!template.IsEmpty)
            {
                var doc = new DockIoWordEditor();
                doc.AttachFile(template.PathToFile);
                return doc.GetText();
            }

            return "";
        }

        public TemplateModel GetTemplate(Guid templateId) =>
            _templateMapper.Map(_templates.Find(templateId));

        public Stream GetTemplateFile(Guid templateId)
        {
            var template = _templates.Find(templateId);
            if (!template.IsEmpty)
                return File.OpenRead(template.PathToFile);

            return null;
        }

        public List<TemplateModel> GetUserTemplates(Guid userId)
        {
            var resultList = new List<TemplateModel>();
            var userTemplates = _templates
                    .FindAll(el => el.CreatedUserId == userId)
                    .ToList();

            foreach (var template in userTemplates)
                resultList.Add(_templateMapper.Map(template));

            return resultList;
        }

        public void InitTemplate(Guid templateId, List<CreateTemplateFieldModel> fields)
        {
            var template = _templates.Find(templateId);
            var templateFields = _templateFields.FindAll(el => el.TemplateID == templateId);

            if (template.IsEmpty) throw new Exception("Template now found");
            if (templateFields.Count != 0) throw new Exception("Template inited yet");

            var docEditor = new DockIoWordEditor();
            docEditor.AttachFile(template.PathToFile);


            foreach (var field in fields)
            {
                var fieldReplaceValue = docEditor.CreateField(field.Value, field.SkipCount);
                
                _templateFields.Add(new TemplateFieldEntity
                {
                    RenameValue = fieldReplaceValue,
                    Description = field.Description,
                    FieldName = field.Name,
                    TemplateID = template.Id,
                });
            }


            docEditor.Save(template.PathToFile);
        }
    }
}
