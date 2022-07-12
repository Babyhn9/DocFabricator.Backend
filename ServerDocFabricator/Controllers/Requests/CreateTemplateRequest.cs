namespace ServerDocFabricator.Server.Controllers.Requests
{
    public class CreateTemplateRequest
    {
        public string Name { get; set; }
        public IFormFile File { get; set; }
    }
}
