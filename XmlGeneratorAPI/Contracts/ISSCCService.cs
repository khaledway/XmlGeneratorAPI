using XmlGeneratorAPI.Requests;

namespace XmlGeneratorAPI.Contracts;

public interface ISSCCService
{
    Task<string> CreateSsccAndAssignToLogisticUnitAsync(CreateSSCCForLogisticUnitRequest request);
    Task<string> CreateSsccAsync(CreateSsccRequest request);
    public int CalculateCheckDigit(string first17DigitsOfSsccCode);
    public bool IsValid(string ssccCode);
    public string ExportSsccBarcodeLabel(string ssccCode);
}

