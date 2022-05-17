namespace ServerDocFabricator.DAL.Entities
{
    public class TemplateFieldEntity : Entity
    {
        public Guid TemplateID { get; set; }
        public string RenameValue { get; set; }
        public string FieldName { get; set; }
        public string Description { get; set; }
        
    }
}
