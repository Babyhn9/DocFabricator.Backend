namespace ServerDocFabricator.DAL.Models.Requests
{
    public class CreateDocumentRequest
    {
        public Guid TemplateId { get; set; }
        public List<BuildTemplateFieldModel> Fields { get; set;}
    }


}
