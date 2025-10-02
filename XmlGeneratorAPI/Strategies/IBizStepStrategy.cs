using XmlGeneratorAPI.Dtos;
using XmlGeneratorAPI.Requests;

namespace XmlGeneratorAPI.Strategies
{
    public interface IBizStepStrategy
    {
        string GenerateXml(EpcisEventRequest request, EpcisPredefinedFieldsDto predefined, List<string> sgtinList);
    }
}
