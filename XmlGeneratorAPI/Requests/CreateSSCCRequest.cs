namespace XmlGeneratorAPI.Requests;

/// <summary>
/// Request to create a new SSCC 
/// </summary>
public record CreateSsccRequest(
    string Gs1CompanyPrefix,
    int ExtensionDigit
);
