namespace ServerDocFabricator.DAL.Entities
{
    public class TemplateEntity : Entity
    {
        public Guid CreatedUserId { get; set; }
        public string TemplateName { get; set; }
        public string? Description { get; set; }
        public string FileName { get; set; }
        
        public List<TemplateFieldEntity> Fields { get; set; }
    }
}
