using System;
using System.Collections.Generic;
using XmlGeneratorAPI.Dtos;
using XmlGeneratorAPI.Enums;
using XmlGeneratorAPI.Requests;

namespace XmlGeneratorAPI.Mappers
{
    public static class BizStepDefaults
    {
        private record BizStepConfig(string Action, string BizStepUrn, string Disposition);

        private static readonly Dictionary<BizStep, BizStepConfig> _configs = new()
        { 
            [BizStep.CommissioningSGTIN] = new("ADD", "urn:epcglobal:cbv:bizstep:commissioning", "urn:epcglobal:cbv:disp:active"),
            [BizStep.CommissioningSSCC] = new("ADD", "urn:epcglobal:cbv:bizstep:commissioning", "urn:epcglobal:cbv:disp:active"),
            [BizStep.Packing] = new("ADD", "urn:epcglobal:cbv:bizstep:packing", "urn:epcglobal:cbv:disp:active"),
            [BizStep.Shipping] = new("OBSERVE", "urn:epcglobal:cbv:bizstep:shipping", "urn:epcglobal:cbv:disp:in_transit"),
            [BizStep.VoidShipping] = new("DELETE", "urn:epcglobal:cbv:bizstep:void_shipping", "urn:epcglobal:cbv:disp:inactive"),
            [BizStep.Receiving] = new("OBSERVE", "urn:epcglobal:cbv:bizstep:receiving", "urn:epcglobal:cbv:disp:active"),
            [BizStep.ReceivingReturning] = new("OBSERVE", "urn:epcglobal:cbv:bizstep:receiving", "urn:epcglobal:cbv:disp:returned"),
            [BizStep.PartialReceivingReturning] = new("OBSERVE", "urn:epcglobal:cbv:bizstep:receiving", "urn:epcglobal:cbv:disp:returned"),
            [BizStep.Unpacking] = new("DELETE", "urn:epcglobal:cbv:bizstep:unpacking", "urn:epcglobal:cbv:disp:active"),
            [BizStep.Sampling] = new("OBSERVE", "urn:epcglobal:cbv:bizstep:sampling", "urn:epcglobal:cbv:disp:active"),
            [BizStep.Missing] = new("OBSERVE", "urn:epcglobal:cbv:bizstep:missing", "urn:epcglobal:cbv:disp:missing"),
            [BizStep.Destroy] = new("DELETE", "urn:epcglobal:cbv:bizstep:destroying", "urn:epcglobal:cbv:disp:destroyed"),
            [BizStep.ErrorDeclaration] = new("OBSERVE", "urn:epcglobal:cbv:bizstep:error_declaration", "urn:epcglobal:cbv:disp:error")
        };

        /// <summary>
        /// Resolves predefined fields based on BizStep and request context
        /// </summary>
        /// <param name="step">The BizStep type</param>
        /// <param name="request">The full request for conditional logic</param>
        /// <returns>DTO with predefined field values</returns>
        public static EpcisPredefinedFieldsDto Resolve(BizStep step, EpcisEventRequest request)
        {
            // Create DTO with dynamic values (calculated each time this method is called)
            var dto = new EpcisPredefinedFieldsDto
            {
                RecordTime = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ssZ"),
                EventTimeZoneOffset = DateTimeOffset.Now.ToString("zzz")
            };

            // Check if custom logic should be applied based on request conditions
            if (ShouldApplyCustomLogic(step, request))
            {
                // Apply custom predefined values instead of dictionary lookup
                ApplyCustomPredefinedValues(dto, step, request);
            }
            else
            {
                // Use standard dictionary configuration
                if (_configs.TryGetValue(step, out var config))
                {
                    dto.Action = config.Action;
                    dto.BizStep = config.BizStepUrn;
                    dto.Disposition = config.Disposition;
                }
            }

            return dto;
        }

        /// <summary>
        /// Determines if custom logic should be applied instead of dictionary lookup
        /// </summary>
        /// <param name="step">The BizStep type</param>
        /// <param name="request">The request to evaluate</param>
        /// <returns>True if custom logic should apply, false for standard dictionary lookup</returns>
        private static bool ShouldApplyCustomLogic(BizStep step, EpcisEventRequest request)
        {
            // TODO: Add your custom validation logic here
            // Examples of conditions you might check:

            // Example 1: Special handling for specific source types
            // if (step == BizStep.Shipping && request.SourceType?.Contains("custom") == true)
            //     return true;

            // Example 2: Special handling based on lot number patterns
            // if (!string.IsNullOrEmpty(request.LotNumber) && request.LotNumber.StartsWith("SPECIAL-"))
            //     return true;

            // Example 3: Custom logic for specific date ranges
            // if (request.ItemExpirationDate.Year < 2025)
            //     return true;

            // Example 4: Special handling for specific read points
            // if (request.ReadPoint?.Contains("urn:epc:id:sgln:custom") == true)
            //     return true;

            // Default: Use standard dictionary lookup
            return false;
        }

        /// <summary>
        /// Applies custom predefined values based on specific request conditions
        /// </summary>
        /// <param name="dto">The DTO to populate</param>
        /// <param name="step">The BizStep type</param>
        /// <param name="request">The request for conditional logic</param>
        private static void ApplyCustomPredefinedValues(EpcisPredefinedFieldsDto dto, BizStep step, EpcisEventRequest request)
        {
            // TODO: Implement your custom logic here
            // This method is called when ShouldApplyCustomLogic returns true

            // Example custom logic implementation:
            switch (step)
            {
                case BizStep.CommissioningSGTIN:
                    // Custom logic for commissioning
                    dto.Action = "ADD";
                    dto.BizStep = "urn:epcglobal:cbv:bizstep:commissioning";

                    // Example: Change disposition based on lot number
                    // if (request.LotNumber?.StartsWith("SPECIAL-") == true)
                    //     dto.Disposition = "urn:custom:disp:special_active";
                    // else
                    dto.Disposition = "urn:epcglobal:cbv:disp:active";
                    break;

                case BizStep.Shipping:
                    // Custom logic for shipping
                    dto.Action = "OBSERVE";
                    dto.BizStep = "urn:epcglobal:cbv:bizstep:shipping";

                    // Example: Custom disposition based on source type
                    // if (request.SourceType?.Contains("express") == true)
                    //     dto.Disposition = "urn:custom:disp:express_transit";
                    // else
                    dto.Disposition = "urn:epcglobal:cbv:disp:in_transit";
                    break;

                default:
                    // Fallback to dictionary for other cases
                    if (_configs.TryGetValue(step, out var config))
                    {
                        dto.Action = config.Action;
                        dto.BizStep = config.BizStepUrn;
                        dto.Disposition = config.Disposition;
                    }
                    break;
            }
        }
    }
}