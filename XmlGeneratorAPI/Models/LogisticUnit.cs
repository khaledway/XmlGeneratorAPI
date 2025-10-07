using XmlGeneratorAPI.Enums;

namespace XmlGeneratorAPI.Models;

public class LogisticUnit : BaseEntity
{
    public string Name { get; set; }
    public LogisticUnitType Type { get; set; }
    public decimal? WeightInKg { get; set; }
    public decimal? LengthInCm { get; set; }
    public decimal? WidthInCm { get; set; }
    public decimal? HeightInCm { get; set; }
    public int ItemsCount { get; set; } // number of items (SGTINs) contained in this logistic unit

    #region commented code

    //// Hierarchical packaging
    //public Guid? ParentUnitId { get; set; }
    //public LogisticUnit? ParentUnit { get; set; }
    //public List<LogisticUnit> ChildUnits { get; set; } = new();

    //public string? Location { get; set; }
    #endregion

    public LogisticUnitStatus Status { get; set; }

    //navigation properties
    public SSCC Sscc { get; set; }
    //public ICollection<SGTIN> Sgtins { get; set; } 
    public ICollection<LogisticUnitAssignment> LogisticUnitAssignments { get; set; }
}