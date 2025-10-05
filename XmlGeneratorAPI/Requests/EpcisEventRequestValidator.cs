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

            // ALWAYS REQUIRED FIELDS
            RuleFor(x => x.EventTime)
                .NotEmpty()
                .WithMessage(_stringLocalizer["err-msg-RequiredField"].Value)
                .GreaterThan(new DateTime(2000, 1, 1))
                .WithMessage(_stringLocalizer["err-msg-MinimalDate"].Value);

            RuleFor(x => x.BizStep)
                .NotEmpty()
                .IsInEnum()
                .WithMessage(_stringLocalizer["err-msg-InvalidBizStep"].Value);

            RuleFor(x => x.SGITNCsvFileID)
                .NotEmpty()
                .WithMessage(_stringLocalizer["err-msg-RequiredField"].Value);

            // CONDITIONAL REQUIRED FIELDS

            // ReadPoint: Required for most BizSteps EXCEPT Missing
            RuleFor(x => x.ReadPoint)
                .NotEmpty()
                .When(x => x.BizStep != BizStep.Missing)
                .WithMessage(_stringLocalizer["err-msg-RequiredField"].Value);

            // BizLocation: Required for specific BizSteps
            RuleFor(x => x.BizLocation)
                .NotEmpty()
                .When(x => x.BizStep is BizStep.CommissioningSGTIN
                    or BizStep.CommissioningSSCC
                    or BizStep.Packing
                    or BizStep.VoidShipping
                    or BizStep.Receiving
                    or BizStep.ReceivingReturning
                    or BizStep.PartialReceivingReturning
                    or BizStep.Unpacking
                    or BizStep.Sampling
                    or BizStep.ErrorDeclaration)
                .WithMessage(_stringLocalizer["err-msg-RequiredField"].Value);

            // ParentID: Required for Packing and Unpacking
            RuleFor(x => x.ParentID)
                .NotEmpty()
                .When(x => x.BizStep is BizStep.Packing or BizStep.Unpacking)
                .WithMessage(_stringLocalizer["err-msg-RequiredField"].Value);

            // SourceType and DestinationType: Required for Shipping, VoidShipping, and Receiving
            RuleFor(x => x.SourceType)
                .NotEmpty()
                .When(x => x.BizStep is BizStep.Shipping or BizStep.VoidShipping or BizStep.Receiving)
                .WithMessage(_stringLocalizer["err-msg-RequiredField"].Value);

            RuleFor(x => x.DestinationType)
                .NotEmpty()
                .When(x => x.BizStep is BizStep.Shipping or BizStep.VoidShipping or BizStep.Receiving)
                .WithMessage(_stringLocalizer["err-msg-RequiredField"].Value);

            // LotNumber: Required for Commissioning, ErrorDeclaration, and Sampling
            RuleFor(x => x.LotNumber)
                .NotEmpty()
                .When(x => x.BizStep is BizStep.CommissioningSSCC or BizStep.CommissioningSGTIN or BizStep.ErrorDeclaration or BizStep.Sampling)
                .WithMessage(_stringLocalizer["err-msg-RequiredField"].Value);

            // ItemExpirationDate: Required for Commissioning and ErrorDeclaration
            RuleFor(x => x.ItemExpirationDate)
                .NotEmpty()
                .When(x => x.BizStep is BizStep.CommissioningSSCC or BizStep.CommissioningSGTIN or BizStep.ErrorDeclaration)
                .WithMessage(_stringLocalizer["err-msg-RequiredField"].Value)
                .Must(date => date > new DateOnly(2000, 1, 1))
                .When(x => x.ItemExpirationDate != default)
                .WithMessage(_stringLocalizer["err-msg-MinimalDate"].Value);

            // Error Declaration specific fields
            RuleFor(x => x.DeclarationTime)
                .NotEmpty()
                .When(x => x.BizStep is BizStep.ErrorDeclaration)
                .WithMessage(_stringLocalizer["err-msg-RequiredField"].Value)
                .GreaterThan(new DateTime(2000, 1, 1))
                .When(x => x.DeclarationTime != default)
                .WithMessage(_stringLocalizer["err-msg-MinimalDate"].Value);

            RuleFor(x => x.Reason)
                .NotEmpty()
                .When(x => x.BizStep is BizStep.ErrorDeclaration)
                .WithMessage(_stringLocalizer["err-msg-RequiredField"].Value);

            RuleFor(x => x.CorrectiveEventID)
                .NotEmpty()
                .When(x => x.BizStep is BizStep.ErrorDeclaration)
                .WithMessage(_stringLocalizer["err-msg-RequiredField"].Value);

            // Partial Receiving Returning specific fields
            RuleFor(x => x.EPCClass)
                .NotEmpty()
                .When(x => x.BizStep is BizStep.PartialReceivingReturning)
                .WithMessage(_stringLocalizer["err-msg-RequiredField"].Value);

            RuleFor(x => x.Quantity)
                .GreaterThan(0)
                .When(x => x.BizStep is BizStep.PartialReceivingReturning)
                .WithMessage(_stringLocalizer["err-msg-RequiredField"].Value);

            RuleFor(x => x.UnitOfMeasure)
                .NotEmpty()
                .When(x => x.BizStep is BizStep.PartialReceivingReturning)
                .WithMessage(_stringLocalizer["err-msg-RequiredField"].Value);
        }
    }
}