using XmlGeneratorAPI.Dtos;
using XmlGeneratorAPI.Enums;
using XmlGeneratorAPI.Requests;

namespace XmlGeneratorAPI.Services
{
    public class EventService : IEventService
    {
        public async Task<string> CreateEvent(BizStep BizStep, EpcisEventRequest request, EpcisPredefinedFieldsDto predefinedFields, List<string> sgtinList)
        {
            return "Test";
        }
    }
}
