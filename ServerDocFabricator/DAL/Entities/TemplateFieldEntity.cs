namespace ServerDocFabricator.DAL.Entities
{
    public class TemplateFieldEntity : Entity
    {
        public Guid TemplateID { get; set; }
        public string ToRename { get; set; }
        public string Label { get; set; }
        public string Description { get; set; }
        
    }
}
