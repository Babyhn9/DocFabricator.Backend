using Microsoft.AspNetCore.Mvc;
using ServerDocFabricator.BL.Interfaces;
using System.Linq;
namespace ServerDocFabricator.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PDFConvertController : ControllerBase
    {
        private readonly IPDFConverter _pdfConverter;

        public PDFConvertController(IPDFConverter pdfConverter)
        {
            _pdfConverter = pdfConverter;
        }

        [HttpPost("upload")]
        public async Task<byte[]> Base(IFormFile file)
        {
            //try
            //{
                var stream = await _pdfConverter.Convert(file.OpenReadStream());
            //    var result = new HttpResponseMessage(System.Net.HttpStatusCode.OK)
            //    {
            //        Content = new ByteArrayContent(stream.GetBuffer())

            //    };

            //    result.Content.Headers.ContentDisposition =
            //        new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment")
            //        {
            //            FileName = "document.pdf"
            //        };
            //    result.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/octet-stream");
            //    return result;

            //}
            //catch (Exception ex)
            //{
            //    var someText = ex.Message;
            //    return new HttpResponseMessage(System.Net.HttpStatusCode.InternalServerError);
            //}

            return stream.GetBuffer();
        }
    }
}
