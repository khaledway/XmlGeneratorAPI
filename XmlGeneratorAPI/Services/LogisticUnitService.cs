using Microsoft.EntityFrameworkCore;
using XmlGeneratorAPI.Contracts;
using XmlGeneratorAPI.Data;
using XmlGeneratorAPI.Enums;
using XmlGeneratorAPI.Models;
using XmlGeneratorAPI.Requests;

namespace XmlGeneratorAPI.Services;

public class LogisticUnitService(ApplicationDbContext  dbContext) : ILogisticUnitService
{
    public async Task<Guid> CreateAsync(CreateLogisticUnitRequest request)
    {
        //create  and Save SGTINs
        var sgtins = request.Sgtins.Select(s => new SGTIN() { Code = s }).ToList();
        dbContext.SGTINs.AddRange(sgtins);
        await dbContext.SaveChangesAsync();

        // create and Save Logistic Unit
        var logisticUnit = new LogisticUnit
        {
            Name = request.Name,
            Type = request.Type,
            ItemsCount = request.ItemsCount,
            WeightInKg = request.WeightInKg,
            LengthInCm = request.LengthInCm,
            WidthInCm = request.WidthInCm,
            HeightInCm = request.HeightInCm,
            Status = LogisticUnitStatus.Created,
        };
        await dbContext.LogisticUnits.AddAsync(logisticUnit);
        await dbContext.SaveChangesAsync();

        //assign SGTINs to Logistic Unit
        var assignments = sgtins.Select(sgtin => new LogisticUnitAssignment
        {
            LogisticUnit = logisticUnit,
            Sgtin = sgtin
        });
        await dbContext.LogisticUnitsAssignments.AddRangeAsync(assignments);
        await dbContext.SaveChangesAsync();

        return logisticUnit.Id;
    }

    public async Task<ICollection<LogisticUnit>> GetAsync(LogisticUnitType type)
    {
        return await dbContext.LogisticUnits.AsNoTracking()
            .Where(l => l.Type == type)
            .ToListAsync();
    } 
}
