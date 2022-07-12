using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ServerDocFabricator.BL.Models;
using ServerDocFabricator.BL.Services.Interfaces;
using ServerDocFabricator.Server.Controllers.Requests;
using ServerDocFabricator.Server.Controllers.Responces;
using ServerDocFabricator.Server.Responces;
using ServerDocFabricator.Utils;
using ServerDocFabricator.Utils.Attributes;
using Syncfusion.DocIO;
using Syncfusion.DocIO.DLS;

namespace ServerDocFabricator.Server.Controllers
{
    [JwtAuth]
    public class TemplatesController : ProjectControllerBase
    {
        private readonly ITemplateBl _templateBl;

        public TemplatesController(ITemplateBl templateBl)
        {
            _templateBl = templateBl;
        }

        [HttpPost("get")]
        public ActionResult<GetTemplateResponce> Get(GetTemplateRequest request)
        {
            var result = _templateBl.GetTemplate(request.TemplateId);

            return Ok(new GetTemplateResponce { Template = result, FlatText = _templateBl.GetFlatText(request.TemplateId) });
        }


        [HttpGet("all")]
        public ActionResult<GetAllTemplatesResponce> GetAllTemplates()
        {
            var result = _templateBl.GetUserTemplates(Identity.Id);
            return Ok(new GetAllTemplatesResponce { Templates = result });
        }

        [HttpPost("create")]
        public ActionResult<CreateTemplateResponce> CreateTemplate([FromForm] CreateTemplateRequest request)
        {
            var template = _templateBl.CreateTemplate(new CreateTemplateModel
            {
                UserId = Identity.Id,
                File = request.File.OpenReadStream(),
                TemplateName = request.Name
            }
            );
            return Ok(new CreateTemplateResponce { TemplateId = template.Id, TemplateName = template.Name });

            //var stringifyFields = request.Fields; 
            //var fields = new List<NewTemplateFieldModel>();
            //foreach (var field in stringifyFields)
            //{
            //    var deserialized = JsonSerializer.Deserialize<NewTemplateFieldModel>(field);
            //    if (deserialized != null)
            //        fields.Add(deserialized);
            //    else return BadRequest("Произошла ошибка при создании шаблона");

            //}

            //var result = _documentBl.CreateTemplate(new NewDocumentTemplateInfo(request.Name,
            //    request.File.OpenReadStream(),
            //     fields));

            //return Ok(new CreateTemplateResponce { TemplateId = result.Id.ToString() });
            //  return Ok(result.Id);
            return Ok();
        }
        [HttpPost("init")]
        public ActionResult InitTemplate(InitTemplateRequest request)
        {
            _templateBl.InitTemplate(request.TemplateId, request.Fields);
            return Ok();
        }


    }
}
