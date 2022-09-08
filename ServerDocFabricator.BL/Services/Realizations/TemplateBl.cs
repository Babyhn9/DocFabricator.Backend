
using ServerDocFabricator.BL.DTO;

namespace ServerDocFabricator.BL.Services.Realizations;
    using AutoMapper;
    
    using ServerDocFabricator.BL.DocumentsEditors;
    using ServerDocFabricator.BL.Services.Interfaces;
    using ServerDocFabricator.BL.Utils.Attributes;
    using ServerDocFabricator.DAL;
    using ServerDocFabricator.DAL.Entities;

    
    ///<inheritdoc/>
    [Buisness]
    public class TemplateBl : ITemplateBl
    {
        private readonly IRepository<TemplateEntity> _templates;
        private readonly IRepository<TemplateFieldEntity> _templateFields;
        private readonly IMapper _mapper;

        public TemplateBl(
            IRepository<TemplateEntity> templates,
            IRepository<TemplateFieldEntity> templateFields,
            IMapper mapper
            )
        {
            _templates = templates;
            _templateFields = templateFields;
            _mapper = mapper;
        }
        public DisplayTemplateDto CreateTemplate(CreateTemplateDto info)
        {
            var documentEditor = new DockIoWordEditor();
            documentEditor.AttachFile(info.File);
            var pathToFile = documentEditor.SaveToDisk();

            var template = _templates.Add(new TemplateEntity
            {
                CreatedUserId = info.UserId,
                TemplateName = info.TemplateName,
                PathToFile = pathToFile,
            });

            return _mapper.Map<DisplayTemplateDto>(template);
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

        public TemplateDto GetTemplate(Guid templateId)
        {
            var result = _templates.Find(templateId);

            if (result != null || result.Id != Guid.Empty)
                return _mapper.Map<TemplateDto>(result);

            return new();
        }

        public Stream GetTemplateFile(Guid templateId)
        {
            var template = _templates.Find(templateId);
            
            if (!template.IsEmpty)
                return File.OpenRead(template.PathToFile);

            return null;
        }

        public List<DisplayTemplateDto> GetUserTemplates(Guid userId) =>
                    _templates
                        .FindAll(el => el.CreatedUserId == userId)
                        .Select(el => _mapper.Map<DisplayTemplateDto>(el))
                        .ToList();

        public void AddFields(Guid templateId, List<CreateTemplateFieldDto> fields)
        {
            var template = _templates.Find(templateId);
            
            if (template.IsEmpty) throw new Exception("Template now found");

            var docEditor = new DockIoWordEditor();
            docEditor.AttachFile(template.PathToFile);

            foreach (var field in fields)
            {
                var fieldReplaceValue = docEditor.CreateField(field.ReplaceableValue, field.SkipCount);
                
                _templateFields.Add(new TemplateFieldEntity
                {
                    ReplaceableValue = fieldReplaceValue,
                    Description = field.Description,
                    FieldName = field.FieldName,
                    TemplateID = template.Id,
                });
            }
    
            docEditor.Save(template.PathToFile);
        }
    }
