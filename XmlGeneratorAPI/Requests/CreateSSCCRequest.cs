namespace XmlGeneratorAPI.Requests;

/// <summary>
/// Request to create a new SSCC for a logistic unit
/// </summary>
public record CreateSSCCRequest(
    Guid LogisticUnitId,
    string Gs1CompanyPrefix,
    int ExtensionDigit
);
