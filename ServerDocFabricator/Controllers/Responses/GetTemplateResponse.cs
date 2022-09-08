using ServerDocFabricator.BL.Models;

namespace ServerDocFabricator.Server.Responses
{
    public class GetTemplateResponce
    {
        public TemplateDTO Template { get; set; }
        public string FlatText { get; set; }
    }
}
