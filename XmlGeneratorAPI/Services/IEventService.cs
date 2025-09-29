using XmlGeneratorAPI.Dtos;
using XmlGeneratorAPI.Enums;
using XmlGeneratorAPI.Requests;

namespace XmlGeneratorAPI.Services
{
    public interface IEventService
    {
        Task<string> CreateEvent(BizStep BizStep, EpcisEventRequest request, EpcisPredefinedFieldsDto predefinedFields, List<string> sgtinList);
    }
}
