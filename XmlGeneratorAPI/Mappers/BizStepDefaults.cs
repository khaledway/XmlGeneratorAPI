using System;
using System.Collections.Generic;
using XmlGeneratorAPI.Dtos;
using XmlGeneratorAPI.Enums;

namespace XmlGeneratorAPI.Mappers
{
    public static class BizStepDefaults
    {
        private record BizStepConfig(string Action, string BizStepUrn, string Disposition);

        private static readonly Dictionary<BizStep, BizStepConfig> _configs = new()
        {
            [BizStep.Commissioning] = new("ADD", "urn:epcglobal:cbv:bizstep:commissioning", "urn:epcglobal:cbv:disp:active"),
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

        public static EpcisPredefinedFieldsDto Resolve(BizStep step)
        {
            // Create DTO with dynamic values (calculated each time this method is called)
            var dto = new EpcisPredefinedFieldsDto
            {
                RecordTime = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ssZ"),
                EventTimeZoneOffset = DateTimeOffset.Now.ToString("zzz")
            };

            // Add static values from configuration
            if (_configs.TryGetValue(step, out var config))
            {
                dto.Action = config.Action;
                dto.BizStep = config.BizStepUrn;
                dto.Disposition = config.Disposition;
            }

            return dto;
        }
    }
}