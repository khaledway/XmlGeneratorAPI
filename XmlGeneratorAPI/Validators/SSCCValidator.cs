namespace XmlGeneratorAPI.Validators
{
    public class SSCCValidator
    {
        public static bool IsValidSscc(string sscc)
        {
            if (string.IsNullOrWhiteSpace(sscc))
                return false;

            sscc = sscc.Trim();

            // Must be exactly 18 numeric digits
            return sscc.Length == 18 && sscc.All(char.IsDigit);
        }
    }
}
