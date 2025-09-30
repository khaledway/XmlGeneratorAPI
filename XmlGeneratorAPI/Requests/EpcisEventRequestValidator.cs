
using FluentValidation;
using Microsoft.Extensions.Localization;
using XmlGeneratorAPI.Enums;

namespace XmlGeneratorAPI.Requests
{
    public class EpcisEventRequestValidator : AbstractValidator<EpcisEventRequest>
    {
        private readonly IStringLocalizer _stringLocalizer;
        public EpcisEventRequestValidator(IStringLocalizer stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
            // Core event attributes
            RuleFor(x => x.EventTime)
           .NotEmpty()
           .WithMessage(_stringLocalizer["err-msg-RequiredField"].Value);

            RuleFor(x => x.EventTime)
           .GreaterThan(new DateTime(2000, 1, 1))
           .WithMessage(_stringLocalizer["err-msg-MinimalDate"].Value);


            RuleFor(x => x.BizStep)
                .NotEmpty().NotNull().WithMessage(_stringLocalizer["err-msg-RequiredField"].Value);

            RuleFor(x => x.BizStep)
                .IsInEnum().WithMessage(_stringLocalizer["err-msg-InvalidBizStep"].Value);


            RuleFor(x => x.ReadPoint)
                .NotEmpty().WithMessage(_stringLocalizer["err-msg-RequiredField"].Value);


            RuleFor(x => x.BizLocation)
                .NotEmpty().WithMessage(_stringLocalizer["err-msg-RequiredField"].Value);

            RuleFor(x => x.ParentID)
                .NotEmpty()
                .When(x => x.BizStep is BizStep.Packing or BizStep.Unpacking)
                .WithMessage(_stringLocalizer["err-msg-RequiredField"].Value);


            RuleFor(x => x.SourceType)
                .NotEmpty()
                .NotNull()
                .When(x => x.BizStep is BizStep.Shipping or BizStep.VoidShipping or BizStep.Receiving)
                .WithMessage(_stringLocalizer["err-msg-RequiredField"].Value);

            RuleFor(x => x.DestinationType)
                .NotEmpty()
                .NotNull()
                .When(x => x.BizStep is BizStep.Shipping or BizStep.VoidShipping or BizStep.Receiving)
                .WithMessage(_stringLocalizer["err-msg-RequiredField"].Value);

            RuleFor(x => x.ItemExpirationDate)
                .NotEmpty()
                .NotNull()
                .When(x => x.BizStep is BizStep.Commissioning or BizStep.ErrorDeclaration)
                .WithMessage(_stringLocalizer["err-msg-RequiredField"].Value);


            RuleFor(x => x.LotNumber)
               .NotEmpty()
               .NotNull()
               .When(x => x.BizStep is BizStep.Commissioning or BizStep.ErrorDeclaration or BizStep.Sampling)
               .WithMessage(_stringLocalizer["err-msg-RequiredField"].Value);

            RuleFor(x => x.ItemExpirationDate)
                .GreaterThan(new DateOnly(2000, 1, 1))
                .WithMessage(_stringLocalizer["err-msg-MinimalDate"].Value);

            RuleFor(x => x.DeclarationTime)
                .NotEmpty()
                .NotNull()
                .When(x => x.BizStep is BizStep.ErrorDeclaration)
                .WithMessage(_stringLocalizer["err-msg-RequiredField"].Value)
                .GreaterThan(new DateTime(2000, 1, 1))
                .WithMessage(_stringLocalizer["err-msg-MinimalDate"].Value);

            RuleFor(x => x.Reason)
                .NotEmpty()
                .NotNull()
                .When(x => x.BizStep is BizStep.ErrorDeclaration)
                .WithMessage(_stringLocalizer["err-msg-RequiredField"].Value);

            RuleFor(x => x.CorrectiveEventID)
                .NotEmpty()
                .NotNull()
                .When(x => x.BizStep is BizStep.ErrorDeclaration)
                .WithMessage(_stringLocalizer["err-msg-RequiredField"].Value);

            RuleFor(x => x.UnitOfMeasure)
               .NotEmpty()
               .NotNull()
               .When(x => x.BizStep is BizStep.PartialReceivingReturning)
               .WithMessage(_stringLocalizer["err-msg-RequiredField"].Value);

            RuleFor(x => x.Quantity)
               .NotEmpty()
               .NotNull()
               .When(x => x.BizStep is BizStep.PartialReceivingReturning)
               .WithMessage(_stringLocalizer["err-msg-RequiredField"].Value);

            RuleFor(x => x.EPCClass)
               .NotEmpty()
               .NotNull()
               .When(x => x.BizStep is BizStep.PartialReceivingReturning)
               .WithMessage(_stringLocalizer["err-msg-RequiredField"].Value);
        }

    }

}