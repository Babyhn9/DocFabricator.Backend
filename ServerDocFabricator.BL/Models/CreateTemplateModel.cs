
namespace ServerDocFabricator.BL.Models
{
    public class CreateTemplateModel
    {
        public Guid UserId { get; set; }
        public string TemplateName { get; set; }
        public Stream File  { get; set; }
    }
}
