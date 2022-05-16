using Microsoft.AspNetCore.Mvc;
using ServerDocFabricator.BL.Interfaces;
using ServerDocFabricator.DAL.Models;
using ServerDocFabricator.DAL.Models.BL;
using ServerDocFabricator.DAL.Models.Requests;
using ServerDocFabricator.DAL.Models.Responces;
using ServerDocFabricator.Utils;
using ServerDocFabricator.Utils.Attributes;
using System.Text.Json;
using Xceed.Words.NET;

namespace ServerDocFabricator.Controllers
{
    [JwtAuth]
    public class DocumentsController : ProjectControllerBase
    {
        private readonly IDocumentBl _documentBl;

        public DocumentsController(IDocumentBl templateBl)
        {
            _documentBl = templateBl;
            Services.Add(templateBl);
        }

        [HttpPost("templates/create")]
        public ActionResult<CreateDocumentResponce> CreateTemplate([FromForm] CreateDocumentTemplateRequest request)
        {
            var stringifyFields = request.Fields;
            var fields = new List<NewTemplateFieldModel>();
            foreach (var field in   stringifyFields)
            {
                var deserialized = JsonSerializer.Deserialize<NewTemplateFieldModel>(field);
                if (deserialized != null)
                    fields.Add(deserialized);
                else return BadRequest("Произошла ошибка при создании шаблона");
                    
            }

            var result = _documentBl.CreateTemplate(new NewDocumentTemplateInfo(request.Name,
                request.File.OpenReadStream(),
                 fields));

            return Ok(new CreateTemplateResponce { TemplateId = result.Id.ToString() });
            //  return Ok(result.Id);
        }

        [HttpGet("{id}")]
        public ActionResult GetDocument(Guid id)
        {
            var document = _documentBl.GetDocument(id);

            if (document.Text == "")
                return NotFound("Такой документ не был найден");

            return Ok(document);
        }

        [HttpGet("all")]
        public ActionResult GetUserDocuments()
        {
            return Ok(_documentBl.GetDocuments());
        }

        [HttpGet("templates/all")]
        public ActionResult<GetTemplatesResponce> GetUserTemplates()
        {
            return new GetTemplatesResponce
            {
                Templates = _documentBl.GetTemplates()
                    .Select(el => new TemplateModel
                    {
                        Id = el.Id,
                        TemplateName = el.TemplateName
                    })
                    .ToList()
            };
        }

        [HttpGet("templates/{templateId}")]
        public ActionResult<GetTemplateResponce> GetUserTemplate(Guid templateId)
        {
            var template = _documentBl.GetTemplate(templateId);
            if (template == null || templateId == Guid.Empty) return NotFound();
            return Ok(new GetTemplateResponce { Template = template});
        }

        [HttpPost("create")]
        public ActionResult<CreateDocumentResponce> CreateDocument(CreateDocumentRequest request)
        {
            var memoriedStream = _documentBl.CreateDocument(new() { Fields = request.Fields, TemplateId = request.TemplateId });
            using var reader = new BinaryReader(memoriedStream);
            reader.BaseStream.Seek(0,SeekOrigin.Begin);


            var bytes = reader.ReadBytes((int)reader.BaseStream.Length);
            var base64 = Convert.ToBase64String(bytes, 0, bytes.Length);
            return Ok(new CreateDocumentResponce { BytesOfDocument = base64 });
        }

        [HttpPost("text")] 
        public ActionResult GetFileText(IFormFile request)
        {
            using var document = DocX.Load(request.OpenReadStream());
            return Ok(document.Text);   
        }
    }
}
