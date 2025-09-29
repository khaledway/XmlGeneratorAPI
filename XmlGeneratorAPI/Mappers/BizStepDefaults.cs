using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using XmlGeneratorAPI.Dtos;
using XmlGeneratorAPI.Enums;

namespace  XmlGeneratorAPI.Mappers
{
    public static class BizStepDefaults
    {
        // Optional: special-token overrides where the CBV token doesn't match a simple snake_case of the enum name
        private static readonly IReadOnlyDictionary<BizStep, string> BizStepTokenOverrides =
      new Dictionary<BizStep, string>
      {
        { BizStep.Commissioning, "commissioning" },
        { BizStep.Packing, "packing" },
        { BizStep.Shipping, "shipping" },
        { BizStep.VoidShipping, "void_shipping" },
        { BizStep.Receiving, "receiving" },
        { BizStep.ReceivingReturning, "receiving_returning" },
        { BizStep.Unpacking, "unpacking" },
        { BizStep.Sampling, "sampling" },
        { BizStep.Missing, "missing" },
        { BizStep.Destroy, "destroy" },
        { BizStep.ErrorDeclaration, "error_declaration" }
      };

        private static readonly Dictionary<BizStep, Action<EpcisPredefinedFieldsDto>> _map
            = new()
            {
                [BizStep.Commissioning] = dto =>
                {
                    dto.RecordTime = DateTime.UtcNow.ToString();
                    dto.EventTimeZoneOffset = DateTimeOffset.Now.ToString("zzz");
                    dto.Action = PrepareAction(BizStep.Commissioning);
                    dto.BizStep = PrepareBizStep(BizStep.Commissioning);
                    dto.Disposition = PrepareDisposition(BizStep.Commissioning);
                },

                [BizStep.Packing] = dto =>
                {
                    dto.RecordTime = DateTime.UtcNow.ToString();
                    dto.EventTimeZoneOffset = DateTimeOffset.Now.ToString("zzz");
                    dto.Action = PrepareAction(BizStep.Packing);
                    dto.BizStep = PrepareBizStep(BizStep.Packing);
                    dto.Disposition = PrepareDisposition(BizStep.Packing);
                },

                [BizStep.Shipping] = dto =>
                {
                    dto.RecordTime = DateTime.UtcNow.ToString();
                    dto.EventTimeZoneOffset = DateTimeOffset.Now.ToString("zzz");
                    dto.Action = PrepareAction(BizStep.Shipping);
                    dto.BizStep = PrepareBizStep(BizStep.Shipping);
                    dto.Disposition = PrepareDisposition(BizStep.Shipping);
                },

                [BizStep.VoidShipping] = dto =>
                {
                    dto.RecordTime = DateTime.UtcNow.ToString();
                    dto.EventTimeZoneOffset = DateTimeOffset.Now.ToString("zzz");
                    dto.Action = PrepareAction(BizStep.VoidShipping);
                    dto.BizStep = PrepareBizStep(BizStep.VoidShipping);
                    dto.Disposition = PrepareDisposition(BizStep.VoidShipping);
                },

                [BizStep.Receiving] = dto =>
                {
                    dto.RecordTime = DateTime.UtcNow.ToString();
                    dto.EventTimeZoneOffset = DateTimeOffset.Now.ToString("zzz");
                    dto.Action = PrepareAction(BizStep.Receiving);
                    dto.BizStep = PrepareBizStep(BizStep.Receiving);
                    dto.Disposition = PrepareDisposition(BizStep.Receiving);
                },

                [BizStep.ReceivingReturning] = dto =>
                {
                    dto.RecordTime = DateTime.UtcNow.ToString();
                    dto.EventTimeZoneOffset = DateTimeOffset.Now.ToString("zzz");
                    dto.Action = PrepareAction(BizStep.ReceivingReturning);
                    dto.BizStep = PrepareBizStep(BizStep.ReceivingReturning);
                    dto.Disposition = PrepareDisposition(BizStep.ReceivingReturning);
                },

                [BizStep.Unpacking] = dto =>
                {
                    dto.RecordTime = DateTime.UtcNow.ToString();
                    dto.EventTimeZoneOffset = DateTimeOffset.Now.ToString("zzz");
                    dto.Action = PrepareAction(BizStep.Unpacking);
                    dto.BizStep = PrepareBizStep(BizStep.Unpacking);
                    dto.Disposition = PrepareDisposition(BizStep.Unpacking);
                },

                [BizStep.Sampling] = dto =>
                {
                    dto.RecordTime = DateTime.UtcNow.ToString();
                    dto.EventTimeZoneOffset = DateTimeOffset.Now.ToString("zzz");
                    dto.Action = PrepareAction(BizStep.Sampling);
                    dto.BizStep = PrepareBizStep(BizStep.Sampling);
                    dto.Disposition = PrepareDisposition(BizStep.Sampling);
                },

                [BizStep.Missing] = dto =>
                {
                    dto.RecordTime = DateTime.UtcNow.ToString();
                    dto.EventTimeZoneOffset = DateTimeOffset.Now.ToString("zzz");
                    dto.Action = PrepareAction(BizStep.Missing);
                    dto.BizStep = PrepareBizStep(BizStep.Missing);
                    dto.Disposition = PrepareDisposition(BizStep.Missing);
                },

                [BizStep.Destroy] = dto =>
                {
                    dto.RecordTime = DateTime.UtcNow.ToString();
                    dto.EventTimeZoneOffset = DateTimeOffset.Now.ToString("zzz");
                    dto.Action = PrepareAction(BizStep.Destroy);
                    dto.BizStep = PrepareBizStep(BizStep.Destroy);
                    dto.Disposition = PrepareDisposition(BizStep.Destroy);
                },

                [BizStep.ErrorDeclaration] = dto =>
                {
                    dto.RecordTime = DateTime.UtcNow.ToString();
                    dto.EventTimeZoneOffset = DateTimeOffset.Now.ToString("zzz");
                    dto.Action = PrepareAction(BizStep.ErrorDeclaration);
                    dto.BizStep = PrepareBizStep(BizStep.ErrorDeclaration);
                    dto.Disposition = PrepareDisposition(BizStep.ErrorDeclaration);
                }
            };

        public static EpcisPredefinedFieldsDto Resolve(BizStep step)
        {
            var dto = new EpcisPredefinedFieldsDto();
            if (_map.TryGetValue(step, out var apply))
                apply(dto);
            return dto;
        }

        // ─────────────────────────────────────────────────────────────────────
        // Helpers
        // ─────────────────────────────────────────────────────────────────────

        private static string PrepareAction(BizStep step) => step switch
        {
            BizStep.Commissioning => "ADD",
            BizStep.Packing => "ADD",
            BizStep.Shipping => "OBSERVE",
            BizStep.VoidShipping => "DELETE",
            BizStep.Receiving => "OBSERVE",
            BizStep.ReceivingReturning => "OBSERVE",
            BizStep.Unpacking => "DELETE",
            BizStep.Sampling => "OBSERVE",
            BizStep.Missing => "OBSERVE",
            BizStep.Destroy => "DELETE",
            BizStep.ErrorDeclaration => "OBSERVE",
            _ => "ADD"
        };

        /// <summary>
        /// Returns urn:epcglobal:cbv:bizstep:{token}
        /// Token is either an explicit override or the snake_case of the enum name.
        /// </summary>
        private static string PrepareBizStep(BizStep step)
        {
            string token = BizStepTokenOverrides.TryGetValue(step, out var t)
                ? t
                : ToSnakeCase(step.ToString());
            return $"urn:epcglobal:cbv:bizstep:{token}";
        }

        private static string PrepareDisposition(BizStep step) => step switch
        {
            BizStep.Commissioning => "urn:epcglobal:cbv:disp:active",
            BizStep.Packing => "urn:epcglobal:cbv:disp:active",
            BizStep.Shipping => "urn:epcglobal:cbv:disp:in_transit",
            BizStep.VoidShipping => "urn:epcglobal:cbv:disp:inactive",
            BizStep.Receiving => "urn:epcglobal:cbv:disp:active",
            BizStep.ReceivingReturning => "urn:epcglobal:cbv:disp:returned",
            BizStep.Unpacking => "urn:epcglobal:cbv:disp:active",
            BizStep.Sampling => "urn:epcglobal:cbv:disp:active", //http://epcis.gs1eg.org/moh/disp/recall_pending

            BizStep.Missing => "urn:epcglobal:cbv:disp:missing",
            BizStep.Destroy => "urn:epcglobal:cbv:disp:destroyed",
            BizStep.ErrorDeclaration => "urn:epcglobal:cbv:disp:error",
            _ => "urn:epcglobal:cbv:disp:active"
        };

        /// <summary>
        /// Converts "PascalCase" or "camelCase" enum names to "snake_case".
        /// Example: "ErrorDeclaration" -> "error_declaration"
        /// </summary>
        private static string ToSnakeCase(string name)
        {
            if (string.IsNullOrWhiteSpace(name)) return string.Empty;

            // Insert underscores between lower-to-upper and number-to-letter boundaries
            var s = Regex.Replace(name, @"([a-z0-9])([A-Z])", "$1_$2");
            s = Regex.Replace(s, @"([A-Z])([A-Z][a-z])", "$1_$2");
            return s.Replace('-', '_').Replace(' ', '_').ToLowerInvariant();
        }
    }
}
