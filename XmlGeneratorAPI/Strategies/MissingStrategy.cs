using XmlGeneratorAPI.Builders;
using XmlGeneratorAPI.Dtos;
using XmlGeneratorAPI.Enums;
using XmlGeneratorAPI.Requests;

namespace XmlGeneratorAPI.Strategies
{
    // 10. Missing Strategy
    public class MissingStrategy : BaseBizStepStrategy
    {
        public MissingStrategy(IXmlBuilder builder) : base(builder) { }

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
                .AddBizStep("http://epcis.gs1eg.org/moh/bizstep/missing")
                .AddDisposition("urn:epcglobal:cbv:disp:stolen");

            return _builder.Build();
        }
    }
}
