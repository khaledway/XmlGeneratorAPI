using XmlGeneratorAPI.Builders;
using XmlGeneratorAPI.Dtos;
using XmlGeneratorAPI.Enums;
using XmlGeneratorAPI.Requests;

namespace XmlGeneratorAPI.Strategies
{
    // 11. Destroy Strategy
    public class DestroyStrategy : BaseBizStepStrategy
    {
        public DestroyStrategy(IXmlBuilder builder) : base(builder) { }

        public override string GenerateXml(EpcisEventRequest request, EpcisPredefinedFieldsDto predefined, List<string> sgtinList)
        {
            _builder
                .Reset()
                .SetEventType(EventType.Object)
                .AddEpcisHeader(DateTime.UtcNow);

            AddCommonElements(request, predefined);

            _builder
                .AddEpcList(sgtinList)
                .AddAction(predefined.Action)
                .AddBizStep(predefined.BizStep)
                .AddDisposition(predefined.Disposition)
                .AddReadPoint(request.ReadPoint);

            return _builder.Build();
        }
    }
}
