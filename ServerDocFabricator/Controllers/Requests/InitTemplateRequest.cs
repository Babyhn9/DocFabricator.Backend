using ServerDocFabricator.BL.Models;

namespace ServerDocFabricator.Server.Controllers.Requests
{
    public class AddFieldsRequest
    {
        public Guid TemplateId { get; set; }
        public List<CreateTemplateFieldModel> Fields { get; set; } = new List<CreateTemplateFieldModel>();
    }
}
