using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ServerDocFabricator.BL.DTO;
using ServerDocFabricator.BL.Services.Interfaces;
using ServerDocFabricator.Server.Controllers.Requests;
using ServerDocFabricator.Server.Controllers.Responses;
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
        public ActionResult<GetAllTemplatesResponse> GetAllTemplates()
        {
            var result = _templateBl.GetUserTemplates(Identity.Id);
            return Ok(new GetAllTemplatesResponse { Templates = result });
        }

        [HttpPost("create")]
        public ActionResult<DisplayTemplateDto> CreateTemplate([FromForm] CreateTemplateRequest request)
        {
            var memo = new MemoryStream();
            var stream = request.File.OpenReadStream();
            stream.CopyTo(memo);
            stream.Close();
            
            var createdTemplate = _templateBl.CreateTemplate(new CreateTemplateDto
            {
                UserId = Identity.Id,
                File = memo,
                TemplateName = request.Name
            });
            
            return Ok(createdTemplate);
        }

        [HttpPost("fields/add")]
        public ActionResult AddFieldsToTemplate(AddFieldsRequest request)
        {
            _templateBl.AddFields(request.TemplateId, request.Fields);
            return Ok();
        }


    }
}
