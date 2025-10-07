namespace XmlGeneratorAPI.Models;

public class SGTIN : BaseEntity
{
    public string Code { get; set; }  // Serialized GTIN

    //if sgtin consists of gtin format and serial number, we can split them into separate properties
    //public string Gs1CompanyPrefix { get; set; }
    //public string ItemNumber { get; set; }
    //public string SerialNumber { get; set; }

    //navigation properties
    //public ICollection<LogisticUnit> LogisticUnits { get; set; }
    public ICollection<LogisticUnitAssignment> LogisticUnitAssignments { get; set; }
}