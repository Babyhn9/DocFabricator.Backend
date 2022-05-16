using Microsoft.Extensions.Options;
using ServerDocFabricator.BL.DocumentEditors;
using ServerDocFabricator.BL.Interfaces;
using ServerDocFabricator.DAL;
using ServerDocFabricator.DAL.Entities;
using ServerDocFabricator.DAL.Models;
using ServerDocFabricator.DAL.Models.BL;
using ServerDocFabricator.Utils;
using ServerDocFabricator.Utils.Attributes;
using Syncfusion.DocIO;
using Syncfusion.DocIO.DLS;

namespace ServerDocFabricator.BL.Realizations
{
    [Buisness]
    public class DocumentBl : IDocumentBl, IBindable
    {
        public Guid UserId { get; set; }

        private readonly IRepository<DocumentTemplateEntity> _templates;
        private readonly IRepository<TemplateFieldEntity> _fields;
        private readonly IRepository<DocumentEntity> _documents;
        private readonly IDocumentEditor _documentEditor;


        public DocumentBl(
            IRepository<DocumentTemplateEntity> templates,
            IRepository<TemplateFieldEntity> fields,
            IRepository<DocumentEntity> documents,
            IDocumentEditor documentEditor
            )
        {
            _templates = templates;
            _fields = fields;
            _documents = documents;
            _documentEditor = documentEditor;
        }

        public List<DocumentEntity> GetDocuments() => _documents.FindAll(el => el.CreatedUserId == UserId);
        public List<TemplateFieldEntity> GetFields(DocumentTemplateEntity document) => _fields.FindAll(el => el.TemplateID == document.Id).ToList();
        public List<DocumentTemplateEntity> GetTemplates() => _templates.FindAll(el => el.CreatedUserId == UserId).ToList();
        public DocumentTemplateEntity CreateTemplate(NewDocumentTemplateInfo info)
        {
            try
            {
                _documentEditor.AttachFile(info.File);

                foreach (var replace in info.Fields)
                    _documentEditor.CreateField(replace.Word, replace.SkipCount); //change template

                var modifiedTemplate = _documentEditor.GetModifiedFile();
                var pathToFile = _documentEditor.SaveToDisk();

                var createdTemplate = _templates
                   .Add(new DocumentTemplateEntity
                   {
                       CreatedUserId = UserId,
                       PathToFile = pathToFile,
                       TemplateName = info.Name
                   });

                for(var i = 0; i < modifiedTemplate.Fields.Count; i++)
                {
                    var templateField = modifiedTemplate.Fields[i];
                    var fieldInfo = info.Fields[i];

                    _fields.Add(new TemplateFieldEntity
                     {
                         TemplateID = createdTemplate.Id,
                         ToRename = templateField.To,
                         Label = fieldInfo.Label,
                         Description = fieldInfo.Description
                     });
                }

                return  createdTemplate;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new DocumentTemplateEntity();
            }
        }

        public Stream CreateDocument(DocumentCreationInfo info)
        {
            var template = _templates.Find(info.TemplateId);
            var fields = info.Fields;

            _documentEditor.AttachFile(template.PathToFile);
            foreach(var field in fields)
            {
                var fieldEntity = _fields.Find(field.FieldId);
                _documentEditor.SetFieldValue(fieldEntity.ToRename, field.Value);
            }

            return _documentEditor.GetModifiedFile().File;
        }

        public BuildTemplateModel GetTemplate(Guid guid)
        {
            var template = _templates.Find(guid);
            var fields = _fields.FindAll(el => el.TemplateID == template.Id);
            return new()
            {
                Fields = fields,
                TemplateName = template.TemplateName,
                Id = template.Id
            };
        }

        public DocumentModel GetDocument(Guid documentId)
        {
            var document = _documents.Find(el => el.Id == documentId && el.CreatedUserId == UserId);
            if (document.IsEmpty) return new();

            var text = File.ReadAllText(document.PathToFile);

            return new()
            {
                DocumentId = document.Id,
                Text = text,
                Name = document.Name
            };
        }

        
    }
}
