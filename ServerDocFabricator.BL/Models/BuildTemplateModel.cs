using ServerDocFabricator.DAL.Entities;

namespace ServerDocFabricator.DAL.Models
{
    public class BuildTemplateModel : TemplateModel
    {
        public List<TemplateFieldEntity> Fields { get; set; }
    }
}
