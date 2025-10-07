namespace XmlGeneratorAPI.Models;

public class LogisticUnitAssignment : BaseEntity
{
    public Guid LogisticUnitId { get; set; }
    public Guid SgtinId { get; set; }
    public DateTime AssignedAt { get; set; } = DateTime.Now;
    public DateTime? UnassignedAt { get; set; } // null = still inside this LU

    //navigation properties
    public SGTIN Sgtin { get; set; }
    public LogisticUnit LogisticUnit { get; set; }
}
