namespace ServerDocFabricator.DAL.Entities
{
    public class DocumentTemplateEntity : Entity
    {
        public Guid CreatedUserId { get; set; }
        public string TemplateName { get; set; }
        public string PathToFile { get; set; }
    }
}
