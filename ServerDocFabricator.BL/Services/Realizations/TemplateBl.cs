
using ColoredLive.DAL;
using ServerDocFabricator.BL.DocumentEditors;
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
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IDocumentEditor _documentEditor;
        private readonly IPathBuilder _pathBuilder;

        public TemplateBl(
            AppDbContext context,
            IMapper mapper,
            IDocumentEditor documentEditor,
            IPathBuilder pathBuilder
            )
        {
            _context = context;
            _mapper = mapper;
            _documentEditor = documentEditor;
            _pathBuilder = pathBuilder;
        }
        public TemplateEntity CreateTemplate(CreateTemplateDto info)
        {
            var fileName = _pathBuilder.CreateFileName();

            var template = _context.Templates.Add(new()
            {
                CreatedUserId = info.UserId,
                TemplateName = info.TemplateName,
                Description = info.Description,
                FileName = fileName,
            }).Entity;
            
            _documentEditor.OpenTemplate(template);
            _documentEditor.Save();
            
            return template;
        }

        public TemplateEntity GetTemplate(Guid templateId)
        {
            throw new NotImplementedException();
        }

        public Stream GetTemplateFile(Guid templateId)
        {
            throw new NotImplementedException();
        }

        public List<DisplayTemplateDto> GetUserTemplates(UserEntity user)
        {
            throw new NotImplementedException();
        }

        public string GetFlatText(TemplateEntity templateEntity)
        {
            throw new NotImplementedException();
        }

        public void AddFields(TemplateEntity templateEntity, List<CreateTemplateFieldDto> fields)
        {
            throw new NotImplementedException();
        }
    }

