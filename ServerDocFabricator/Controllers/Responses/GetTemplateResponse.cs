using ServerDocFabricator.BL.DTO;

namespace ServerDocFabricator.Server.Controllers.Responses
{
    public class GetTemplateResponce
    {
        public TemplateDto Template { get; set; }
        public string FlatText { get; set; }
    }
}
