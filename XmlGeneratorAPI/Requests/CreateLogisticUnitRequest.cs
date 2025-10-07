using XmlGeneratorAPI.Enums;

namespace XmlGeneratorAPI.Requests;

public record CreateLogisticUnitRequest(
string Name,
LogisticUnitType Type,
int ItemsCount,
decimal? WeightInKg,
decimal? LengthInCm,
decimal? WidthInCm,
decimal? HeightInCm,
ICollection<string> Sgtins
);
