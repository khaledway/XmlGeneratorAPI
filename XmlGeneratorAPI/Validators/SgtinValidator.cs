using System.Text.RegularExpressions;

namespace XmlGeneratorAPI.Validators
{
    /// <summary>
    /// Validates SGTINs in the strict dot-delimited format ONLY:
    /// {CompanyPrefix}.{ItemRef}.{Serial}
    /// - CompanyPrefix: digits (one or more)
    /// - ItemRef: digits (one or more)
    /// - Serial: uppercase A–Z and 0–9 (length 1..36)
    /// Example: 622300297.0774.J79UFNRCFN7F1QR
    /// </summary>
    public static class SgtinValidator
    {
        private static readonly Regex StrictDotFormat = new Regex(
            @"^\d+\.\d+\.[A-Z0-9]{1,36}$",
            RegexOptions.Compiled);

        public static bool IsValidSgtin(string value)
        {
            if (string.IsNullOrWhiteSpace(value)) return false;
            var s = value.Trim();
            return StrictDotFormat.IsMatch(s);
        }
    }
}
