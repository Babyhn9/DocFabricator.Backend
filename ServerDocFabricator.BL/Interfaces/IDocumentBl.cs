using ServerDocFabricator.DAL.Entities;
using ServerDocFabricator.DAL.Entities.RefEntities;
using ServerDocFabricator.DAL.Models;
using ServerDocFabricator.DAL.Models.BL;

namespace ServerDocFabricator.BL.Interfaces
{
    public interface IDocumentBl : IBindable
    {
        TempalteEntity CreateTemplate(NewDocumentTemplateInfo info);
        Stream CreateDocument(DocumentCreationInfo info);
        List<TempalteEntity> GetTemplates();
        BuildTemplateModel GetTemplate(Guid guid);
        /// <summary>
        /// return created users documents
        /// </summary>
        /// <returns></returns>
        List<DocumentEntity> GetDocuments();
        /// <summary>
        /// return inner info about document aka text
        /// </summary>
        /// <param name="documentId"></param>
        /// <returns></returns>
        DocumentModel GetDocument(Guid documentId);
        
        List<TemplateFieldEntity> GetFields(TempalteEntity document);

    }
}
