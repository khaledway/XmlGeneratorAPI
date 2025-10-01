//namespace XmlGeneratorAPI.Strategies
//{
//    public class BizStepStrategies
//    {
//    }
//}

using XmlGeneratorAPI.Builders;
using XmlGeneratorAPI.Dtos;
using XmlGeneratorAPI.Enums;
using XmlGeneratorAPI.Requests;

namespace XmlGeneratorAPI.Strategies
{
    public interface IBizStepStrategy
    {
        string GenerateXml(EpcisEventRequest request, EpcisPredefinedFieldsDto predefined, List<string> sgtinList);
    }

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

    // 1. Commissioning Strategy
    public class CommissioningStrategy : BaseBizStepStrategy
    {
        public CommissioningStrategy(IXmlBuilder builder) : base(builder) { }

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
                .AddReadPoint(request.ReadPoint)
                .AddBizLocation(request.BizLocation)
                .AddIlmd(request.LotNumber, request.ItemExpirationDate.GetValueOrDefault());

            return _builder.Build();
        }
    }

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

    // 3. Shipping Strategy
    public class ShippingStrategy : BaseBizStepStrategy
    {
        public ShippingStrategy(IXmlBuilder builder) : base(builder) { }

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
                .AddReadPoint(request.ReadPoint)
                .AddSourceList(request.SourceType, request.ReadPoint)
                .AddDestinationList(request.DestinationType, request.BizLocation);

            return _builder.Build();
        }
    }

    // 4. Void Shipping Strategy
    public class VoidShippingStrategy : BaseBizStepStrategy
    {
        public VoidShippingStrategy(IXmlBuilder builder) : base(builder) { }

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
                .AddReadPoint(request.ReadPoint)
                .AddBizLocation(request.BizLocation)
                .AddSourceList(request.SourceType, request.ReadPoint)
                .AddDestinationList(request.DestinationType, request.BizLocation);

            return _builder.Build();
        }
    }

    // 5. Receiving Strategy
    public class ReceivingStrategy : BaseBizStepStrategy
    {
        public ReceivingStrategy(IXmlBuilder builder) : base(builder) { }

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
                .AddReadPoint(request.ReadPoint)
                .AddBizLocation(request.BizLocation)
                .AddSourceList(request.SourceType, request.ReadPoint)
                .AddDestinationList(request.DestinationType, request.BizLocation);

            return _builder.Build();
        }
    }

    // 6. Receiving Returning Strategy
    public class ReceivingReturningStrategy : BaseBizStepStrategy
    {
        public ReceivingReturningStrategy(IXmlBuilder builder) : base(builder) { }

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
                .AddReadPoint(request.ReadPoint)
                .AddBizLocation(request.BizLocation);

            return _builder.Build();
        }
    }

    // 7. Partial Receiving Returning Strategy
    public class PartialReceivingReturningStrategy : BaseBizStepStrategy
    {
        public PartialReceivingReturningStrategy(IXmlBuilder builder) : base(builder) { }

        public override string GenerateXml(EpcisEventRequest request, EpcisPredefinedFieldsDto predefined, List<string> sgtinList)
        {
            _builder
                .Reset()
                .SetEventType(EventType.Object)
                .AddEpcisHeader(DateTime.UtcNow);

            AddCommonElements(request, predefined);

            _builder
                .AddAction(predefined.Action)
                .AddBizStep(predefined.BizStep)
                .AddDisposition(predefined.Disposition)
                .AddReadPoint(request.ReadPoint)
                .AddBizLocation(request.BizLocation)
                .AddQuantityList(request.EPCClass, request.Quantity.GetValueOrDefault(), request.UnitOfMeasure);

            return _builder.Build();
        }
    }

    // 8. Unpacking Strategy
    public class UnpackingStrategy : BaseBizStepStrategy
    {
        public UnpackingStrategy(IXmlBuilder builder) : base(builder) { }

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

    // 9. Sampling Strategy
    public class SamplingStrategy : BaseBizStepStrategy
    {
        public SamplingStrategy(IXmlBuilder builder) : base(builder) { }

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
                .AddReadPoint(request.ReadPoint)
                .AddBizLocation(request.BizLocation);

            return _builder.Build();
        }
    }

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

    // Strategy Factory
    public class BizStepStrategyFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public BizStepStrategyFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IBizStepStrategy GetStrategy(BizStep bizStep)
        {
            var builder = new EpcisXmlBuilder();

            return bizStep switch
            {
                BizStep.Commissioning => new CommissioningStrategy(builder),
                BizStep.Packing => new PackingStrategy(builder),
                BizStep.Shipping => new ShippingStrategy(builder),
                BizStep.VoidShipping => new VoidShippingStrategy(builder),
                BizStep.Receiving => new ReceivingStrategy(builder),
                BizStep.ReceivingReturning => new ReceivingReturningStrategy(builder),
                BizStep.PartialReceivingReturning => new PartialReceivingReturningStrategy(builder),
                BizStep.Unpacking => new UnpackingStrategy(builder),
                BizStep.Sampling => new SamplingStrategy(builder),
                BizStep.Missing => new MissingStrategy(builder),
                BizStep.Destroy => new DestroyStrategy(builder),
                BizStep.ErrorDeclaration => new ErrorDeclarationStrategy(builder),
                _ => throw new ArgumentException($"Unsupported BizStep: {bizStep}")
            };
        }
    }
}
