using SkiaSharp;

namespace XmlGeneratorAPI.Models;

public class Constants
{
    public static class Business
    {
        public const string TestGs1CompanyPrefix = "50000555";
        public const int TestExtensionDigit = 0;
        public const int ExtensionDigitLength = 1;
        public const int MinExtensionDigit = 0;
        public const int MaxExtensionDigit = 9;
        public const int MaxCompanyPrefixLength = 16;
        public const int SsccCodeLength = 18;
        public const int CheckDigitLength = 1;
        public const int MinSerialLength = 1;
        public const char ZeroCharacter = '0';
        public const string NanoidAlphabets = "0123456789";
        public const string Gs1SsccAI = "(00)";
        public const int SsccBarcodeWidth = 600;
        public const int SsccBarcodeHeight = 200;
        public const int Padding = 100;
        public const int Quality = 100;
        public const SKEncodedImageFormat ImageExtension = SKEncodedImageFormat.Png;
        public const string LabelsFolderName = "SSCC_Lables";
        public const string Slash = "/";


    }
    public static class DatabaseColumnTypes
    {
        public const string VARCHAR = "VARCHAR";
    }
}
