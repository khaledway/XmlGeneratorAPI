using System.ComponentModel.DataAnnotations;
namespace SSCCProject.Domain.Entities;

public sealed class SSCC : BaseEntity
{
    public string Code { get; init; }  // 18-digit full SSCC code 
    public string ExtensionDigit { get; init; } // "0"-"9"

    [MinLength(4), MaxLength(12)]

    //need to add composite unique index on Gs1CompanyPrefix + SerialNumber 
    public string Gs1CompanyPrefix { get; init; } // 0 13 3  1
    public string SerialNumberPaddedZeros { get; init; }
    public int SerialNumber { get; init; }
    public string CheckDigit { get; init; } // "0"-"9" = last digit of Code = computed from first 17 digits
    public Guid LogisticUnitId { get; set; }

    //navigation property
    public LogisticUnit LogisticUnit { get; set; }
}