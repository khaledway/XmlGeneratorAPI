using XmlGeneratorAPI.Builders;
using XmlGeneratorAPI.Dtos;
using XmlGeneratorAPI.Enums;
using XmlGeneratorAPI.Requests;

namespace XmlGeneratorAPI.Strategies
{
    // 2. Packing Strategy
    public class PackingStrategy : BaseBizStepStrategy
    {
        public PackingStrategy(IXmlBuilder builder) : base(builder) { }

        public override string GenerateXml(EpcisEventRequest request, EpcisPredefinedFieldsDto predefined, List<string> sgtinList)
        {
            _builder
                .Reset()
                .SetEventType(EventType.Aggregation)
                .AddEpcisHeader(DateTime.UtcNow);

            AddCommonElements(request, predefined);

            _builder
                .AddParentId(request.ParentID)
                .AddChildEPCs(sgtinList)
                .AddAction(predefined.Action)
                .AddBizStep(predefined.BizStep)
                .AddDisposition(predefined.Disposition)
                .AddReadPoint(request.ReadPoint)
                .AddBizLocation(request.BizLocation);

            return _builder.Build();
        }
    }
}
