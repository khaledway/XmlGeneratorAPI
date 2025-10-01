using XmlGeneratorAPI.Builders;
using XmlGeneratorAPI.Dtos;
using XmlGeneratorAPI.Enums;
using XmlGeneratorAPI.Requests;

namespace XmlGeneratorAPI.Strategies
{
    // 12. Error Declaration Strategy
    public class ErrorDeclarationStrategy : BaseBizStepStrategy
    {
        public ErrorDeclarationStrategy(IXmlBuilder builder) : base(builder) { }

        public override string GenerateXml(EpcisEventRequest request, EpcisPredefinedFieldsDto predefined, List<string> sgtinList)
        {
            _builder
                .Reset()
                .SetEventType(EventType.Object)
                .AddEpcisHeader(DateTime.UtcNow);

            AddCommonElements(request, predefined);

            _builder
                .AddErrorDeclaration(request.DeclarationTime.GetValueOrDefault(), request.Reason, request.CorrectiveEventID)
                .AddEpcList(sgtinList)
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
