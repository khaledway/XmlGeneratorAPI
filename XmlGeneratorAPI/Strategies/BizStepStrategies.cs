using XmlGeneratorAPI.Builders;
using XmlGeneratorAPI.Dtos;
using XmlGeneratorAPI.Enums;
using XmlGeneratorAPI.Requests;

namespace XmlGeneratorAPI.Strategies
{
    public abstract class BaseBizStepStrategy : IBizStepStrategy
    {
        protected readonly IXmlBuilder _builder;

        protected BaseBizStepStrategy(IXmlBuilder builder)
        {
            _builder = builder;
        }

        public abstract string GenerateXml(EpcisEventRequest request, EpcisPredefinedFieldsDto predefined, List<string> sgtinList);

        protected void AddCommonElements(EpcisEventRequest request, EpcisPredefinedFieldsDto predefined)
        {
            _builder
                .AddEventTime(request.EventTime)
                .AddRecordTime(predefined.RecordTime)
                .AddEventTimeZoneOffset(predefined.EventTimeZoneOffset);
        }
    }
}
