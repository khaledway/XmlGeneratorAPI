using XmlGeneratorAPI.Builders;
using XmlGeneratorAPI.Dtos;
using XmlGeneratorAPI.Enums;
using XmlGeneratorAPI.Requests;

namespace XmlGeneratorAPI.Strategies
{
    public class CommissioningSSCCStrategy : BaseBizStepStrategy
    {
        public CommissioningSSCCStrategy(IXmlBuilder builder) : base(builder) { }

        public override string GenerateXml(EpcisEventRequest request, EpcisPredefinedFieldsDto predefined, List<string> ssccList)
        {
            _builder
                .Reset()
                .SetEventType(EventType.Object)
                .AddEpcisHeader(DateTime.UtcNow , includeCbvmdaNamespace: true);

            AddCommonElements(request, predefined);

            _builder
                .AddEpcList(ssccList)
                .AddAction(predefined.Action)
                .AddBizStep(predefined.BizStep)
                .AddDisposition(predefined.Disposition)
                .AddReadPoint(request.ReadPoint)
                .AddBizLocation(request.BizLocation)
                .AddIlmd(request.LotNumber, request.ItemExpirationDate.GetValueOrDefault());

            return _builder.Build();
        }
    }
}
