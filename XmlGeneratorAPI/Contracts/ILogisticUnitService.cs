using XmlGeneratorAPI.Enums;
using XmlGeneratorAPI.Models;
using XmlGeneratorAPI.Requests;

namespace XmlGeneratorAPI.Contracts;

public interface ILogisticUnitService
{
    Task<Guid> CreateAsync(CreateLogisticUnitRequest request);
    Task<ICollection<LogisticUnit>> GetAsync(LogisticUnitType type);
}