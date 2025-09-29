using XmlGeneratorAPI.Requests;

namespace XmlGeneratorAPI.Services
{
    public interface IEventService
    {
        Task<string> CreateEvent(EpcisEventRequest request, IFormFile csvFile);
    }
}
