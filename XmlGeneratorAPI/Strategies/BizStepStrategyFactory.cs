using XmlGeneratorAPI.Builders;
using XmlGeneratorAPI.Enums;

namespace XmlGeneratorAPI.Strategies
{
    // Strategy Factory
    public class BizStepStrategyFactory
    {
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
