
namespace ServerDocFabricator.BL.DTO
{
    public class CreateTemplateDto
    {
        public Guid UserId { get; set; }
        public string TemplateName { get; set; }
        public Stream File  { get; set; }
        public string Description { get; set; }
    }
}
