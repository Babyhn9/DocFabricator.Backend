namespace ServerDocFabricator.DAL.Entities
{
    public class DocumentEntity : Entity
    {
        public Guid CreatedUserId { get; set; }
        public string PathToFile { get; set; } 
        public string Name { get; set; }
    }
}
