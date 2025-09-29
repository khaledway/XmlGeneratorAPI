using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;
using XmlGeneratorAPI.Data;
using XmlGeneratorAPI.Dtos;
using XmlGeneratorAPI.Enums;
using XmlGeneratorAPI.Mappers;
using XmlGeneratorAPI.Options;
using XmlGeneratorAPI.Requests;
using XmlGeneratorAPI.Services;

namespace XmlGeneratorAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class XMLHandlingController : ControllerBase
    {

        IEventService _eventService;

        public XMLHandlingController(IEventService eventService)
        {
            _eventService = eventService;
        }
        public async Task<string> GenerateXML(BizStep BizStep, EpcisEventRequest request)
        {

            // implement xml here 

            var preDefined= BizStepDefaults.Resolve(BizStep);

            IFormFile cvsFile = await GetCvsFile(request.SGITNCsvFileID); // please get file content and

            List<string> sgtinList = ExtractSGTINList(cvsFile);


            var result = _eventService.CreateEvent(BizStep, request, preDefined, sgtinList);

            return "done";
        }

        private async Task<IFormFile> GetCvsFile(Guid sGITNCsvFileID)
        {
            throw new NotImplementedException();
        }

        private List<string> ExtractSGTINList(IFormFile cvsFile)
        {
            throw new NotImplementedException();
        }
    }
}
