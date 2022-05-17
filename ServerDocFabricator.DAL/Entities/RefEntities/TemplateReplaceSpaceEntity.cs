namespace ServerDocFabricator.DAL.Entities.RefEntities
{
    public class TemplateReplaceSpaceEntity : Entity
    {
        public Guid TemplateId { get; set; }
        public string TextToReplace { get; set; }

    }
}
