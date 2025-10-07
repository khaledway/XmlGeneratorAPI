using BarcodeStandard;
using Microsoft.EntityFrameworkCore;
using NanoidDotNet;
using SkiaSharp;
using XmlGeneratorAPI.Contracts;
using XmlGeneratorAPI.Data;
using XmlGeneratorAPI.Models;
using XmlGeneratorAPI.Requests;
using static XmlGeneratorAPI.Models.Constants;

namespace XmlGeneratorAPI.Services
{
    public class SSCCService(ApplicationDbContext dbContext) : ISSCCService
    {
        public async Task<string> CreateAsync(CreateSSCCRequest request)
        {
            var sscc = await CreateSSCC(request);

            dbContext.SSCCs.Add(sscc);
            await dbContext.SaveChangesAsync();

            return sscc.Code;
        }

        private async Task<SSCC> CreateSSCC(CreateSSCCRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Gs1CompanyPrefix) || !request.Gs1CompanyPrefix.All(char.IsDigit))
                throw new ArgumentException("gs1companyPrefix must be numeric", nameof(request.Gs1CompanyPrefix) + " " + request.Gs1CompanyPrefix);

            if (request.Gs1CompanyPrefix.Length > Business.MaxCompanyPrefixLength)
                throw new ArgumentException("companyPrefix too long for SSCC", nameof(request.Gs1CompanyPrefix));

            if (request.ExtensionDigit < Business.MinExtensionDigit || request.ExtensionDigit > Business.MaxExtensionDigit)
                throw new ArgumentOutOfRangeException(nameof(request.ExtensionDigit));

            // serial number length = 18 - (1 extension) - (1 check digit ) - companyPrefix.Length 
            int serialLength = Business.SsccCodeLength - Business.ExtensionDigitLength - Business.CheckDigitLength - request.Gs1CompanyPrefix.Length;

            if (serialLength < Business.MinSerialLength)
                throw new ArgumentException("companyPrefix leaves no room for serial reference", nameof(request.Gs1CompanyPrefix));

            int serialNumber;
            // Ensure the generated serial number is unique
            do
            {
                // Generate a random numeric serial number with Nanoid
                serialNumber = int.Parse(Nanoid.Generate(Business.NanoidAlphabets, serialLength));
            }
            while (await dbContext.SSCCs.AnyAsync(s => s.SerialNumber == serialNumber && s.Gs1CompanyPrefix == request.Gs1CompanyPrefix));

            string serialAsString = serialNumber.ToString();
            if (serialAsString.Length > serialLength)
                throw new ArgumentException($"serial {serialNumber} exceeds allocated length {serialLength}", nameof(serialNumber));

            string serialNumberPaddedZeros = serialAsString.PadLeft(serialLength, Business.ZeroCharacter);

            string first17Digits = $"{request.ExtensionDigit}{request.Gs1CompanyPrefix}{serialNumberPaddedZeros}";
            int checkDigit = CalculateCheckDigit(first17Digits);
            string ssccFullCode = $"{first17Digits}{checkDigit}";

            return new SSCC
            {
                Code = ssccFullCode,
                ExtensionDigit = request.ExtensionDigit.ToString(),
                Gs1CompanyPrefix = request.Gs1CompanyPrefix,
                SerialNumber = serialNumber,
                SerialNumberPaddedZeros = serialNumberPaddedZeros,
                CheckDigit = checkDigit.ToString(),
                LogisticUnitId = request.LogisticUnitId
            };
        }

        // Validate a full SSCC string (18 digits)
        public bool IsValid(string sscc)
        {
            if (string.IsNullOrWhiteSpace(sscc) || sscc.Length != 18 || !sscc.All(char.IsDigit)) return false;
            var raw17 = sscc.Substring(0, 17);
            var expected = CalculateCheckDigit(raw17);
            var actual = sscc[17] - '0';
            return expected == actual;
        }

        /// <summary>
        /// Calculates the GS1 check digit for an SSCC based on the first 17 digits.
        /// </summary>
        /// <param name="first17DigitsOfSsccCode">A string of exactly 17 numeric characters.</param>
        /// <returns>The check digit (0–9).</returns>
        /// <exception cref="ArgumentException">Thrown if input is invalid.</exception>
        public int CalculateCheckDigit(string first17DigitsOfSsccCode)
        {
            if (string.IsNullOrWhiteSpace(first17DigitsOfSsccCode)
                || first17DigitsOfSsccCode.Length != 17
                || !first17DigitsOfSsccCode.All(char.IsDigit))
                throw new ArgumentException("Input must be exactly 17 digits.", nameof(first17DigitsOfSsccCode));

            int sum = 0;

            // Process digits from right to left
            for (int i = 0; i < first17DigitsOfSsccCode.Length; i++)
            {
                int positionFromRight = first17DigitsOfSsccCode.Length - i;
                int digit = first17DigitsOfSsccCode[i] - '0'; // C# trick to convert a char digit into its int value.

                // GS1 rule: odd positions (from right) weight = 3, even positions weight = 1
                // GS1 modulo-10 (weights 3 and 1 from rightmost to left)
                int weight = positionFromRight % 2 == 0 ? 1 : 3;

                sum += digit * weight;
            }

            // Check digit = number that makes sum a multiple of 10
            int mod = sum % 10;
            return (10 - mod) % 10;
        }
        public string ExportSsccBarcodeLabel(string ssccCode)
        {
            //1️ add Application Identifier(00) before the SSCC
            string data = $"{Business.Gs1SsccAI}{ssccCode}";

            // 2️  Initialize BarcodeLib generator
            Barcode barcode = new()
            {
                IncludeLabel = true,
            };

            // 3️  Generate Code128 (GS1-128 is subset of Code128)
            SKImage image = barcode.Encode(BarcodeStandard.Type.Code128, data, Business.SsccBarcodeWidth, Business.SsccBarcodeHeight);

            // define padding (around all sides)

            // calculate new dimensions
            int newWidth = image.Width + Business.Padding * 2;
            int newHeight = image.Height + Business.Padding * 2;

            // create surface with white background
            var surface = SKSurface.Create(new SKImageInfo(newWidth, newHeight));
            var canvas = surface.Canvas;
            canvas.Clear(SKColors.White);

            // draw the barcode in the center with padding
            canvas.DrawImage(image, Business.Padding, Business.Padding);

            // finalize and encode to PNG
            SKImage paddedImage = surface.Snapshot();
            SKData pngData = paddedImage.Encode(Business.ImageExtension, Business.Quality);

            // create folder if not exists
            string labelsFolderPath = Environment.CurrentDirectory + Business.Slash + Business.LabelsFolderName;
            Directory.CreateDirectory(labelsFolderPath);

            // save to file
            string outputPath = Path.Combine(labelsFolderPath, $"SSCC_{ssccCode}.{Business.ImageExtension.ToString().ToLower()}");
            var fileStream = File.OpenWrite(outputPath);
            pngData.SaveTo(fileStream);
            fileStream.Close();

            Console.WriteLine($"✅ SSCC barcode saved to: {outputPath}");

            return outputPath;
        }
    }
}