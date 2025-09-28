using System.Text.RegularExpressions;

namespace XmlGeneratorAPI.Validators
{
    public static class SgtinValidator
    {
        // EPC Pure Identity URI for SGTIN: urn:epc:id:sgtin:CompanyPrefix.ItemRef.Serial
        // CompanyPrefix and ItemRef: numeric; Serial: alphanumeric up to 20 (typical), allow -, _, . and : conservatively.
        private static readonly Regex EpcPureId = new Regex(
            @"^urn:epc:id:sgtin:\d+\.\d+\.[A-Za-z0-9._:-]{1,36}$",
            RegexOptions.Compiled);

        // GS1 Application Identifier format: (01)GTIN14(21)Serial(any non-space up to ~20)
        private static readonly Regex Gs1AI = new Regex(
            @"^\(01\)\d{14}\(21\)[^\s,;]{1,36}$",
            RegexOptions.Compiled);

        public static bool IsValidSgtin(string value)
        {
            if (string.IsNullOrWhiteSpace(value)) return false;
            var s = value.Trim();
            return EpcPureId.IsMatch(s) || Gs1AI.IsMatch(s);
        }
    }
}
