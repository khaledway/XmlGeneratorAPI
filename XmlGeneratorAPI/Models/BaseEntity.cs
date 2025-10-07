namespace XmlGeneratorAPI.Models;

public abstract class BaseEntity
{

    public Guid Id { get; set; }

    public DateTime CreatedAt { get; set; }
    public int? CreatedBy { get; set; }

    public DateTime? UpdatedAt { get; set; }
    public int? UpdatedBy { get; set; }

    public bool IsDeleted { get; set; } = false;
}