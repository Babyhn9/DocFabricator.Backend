using ServerDocFabricator.BL.Models;

namespace ServerDocFabricator.Server.Responces
{
    public class GetTemplateResponce
    {
        public TemplateModel Template { get; set; }
        public string FlatText { get; set; }
    }
}
