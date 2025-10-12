using SkiaSharp;

namespace XmlGeneratorAPI.Models;

public class Constants
{
    public static class Business
    {
        public const string TestGs1CompanyPrefix = "622500005980";// business need to use gln without check digit as gs1 company prefix //Gln with check digit was : 6225000059801 ,
        public const int TestExtensionDigit = 0;
        public const int ExtensionDigitLength = 1;
        public const int MinExtensionDigit = 0;
        public const int MaxExtensionDigit = 9;
        public const int MaxCompanyPrefixLength = 16;
        public const int SsccLength = 18;
        public const int SsccWithoutCheckDigitLength = 17;
        public const int CheckDigitLength = 1;
        public const int MinSerialLength = 1;
        public const char ZeroCharacter = '0';
        public const string NanoidAlphabets = "0123456789";
        public const string Gs1SsccAI = "(00)"; //gs1 sscc application identifier
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
