using XmlGeneratorAPI.Dtos;
using XmlGeneratorAPI.Enums;
using XmlGeneratorAPI.Requests;

namespace XmlGeneratorAPI.Services
{
    public interface IEventService
    {
        Task<string> CreateEvent(BizStep bizStep, EpcisEventRequest request, EpcisPredefinedFieldsDto predefinedFields, List<string> sgtinList);
        Task<List<string>> ExtractSgtinListFromFile(Guid fileId);
        Task<string> SaveGeneratedXml(string xml, string fileName);
    }
}