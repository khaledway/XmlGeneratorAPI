using XmlGeneratorAPI.Requests;

namespace XmlGeneratorAPI.Services
{
    public class EventService : IEventService
    {
        public async Task<string> CreateEvent(EpcisEventRequest request, IFormFile csvFile)
        {
            return "Test";
        }
    }
}
