using ServerDocFabricator.DAL.Models.BL;
using System.Text.Json.Serialization;

namespace ServerDocFabricator.DAL.Models.Requests
{
    public class CreateDocumentTemplateRequest
    {
        public string Name { get; set; }
        public IFormFile File { get; set; }
        public List<string> Fields { get; set; }

    }
}
