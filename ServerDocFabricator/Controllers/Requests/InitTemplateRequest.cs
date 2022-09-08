using ServerDocFabricator.BL.DTO;

namespace ServerDocFabricator.Server.Controllers.Requests
{
    public class AddFieldsRequest
    {
        public Guid TemplateId { get; set; }
        public List<CreateTemplateFieldDto> Fields { get; set; } = new List<CreateTemplateFieldDto>();
    }
}
