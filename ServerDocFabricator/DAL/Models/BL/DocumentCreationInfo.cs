namespace ServerDocFabricator.DAL.Models.BL
{
    public class DocumentCreationInfo
    {

        public Guid TemplateId { get; set; }
        public List<BuildTemplateFieldModel> Fields { get; set; }

    }
}
