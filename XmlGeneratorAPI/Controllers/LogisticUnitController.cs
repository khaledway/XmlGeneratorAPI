using Microsoft.AspNetCore.Mvc;
using XmlGeneratorAPI.Contracts;
using XmlGeneratorAPI.Enums;
using XmlGeneratorAPI.Models;
using XmlGeneratorAPI.Requests;

namespace XmlGeneratorAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LogisticUnitController(ILogisticUnitService logisticUnitService) : ControllerBase
{
    /// <summary>
    /// Creates a new Logistic Unit.
    /// <param name="request">Create Logistic Unit Request.</param>
    /// <returns>Logistic Unit ID.</returns>
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<int>> Create(CreateLogisticUnitRequest request)
    {
        return Ok(await logisticUnitService.CreateAsync(request));
    }

    /// <summary>
    /// get Logistic Units.
    /// <param name="request">Update Logistic Unit Request.</param>
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<ICollection<LogisticUnit>>> Get(LogisticUnitType type)
    {
        return Ok(await logisticUnitService.GetAsync(type));
    }
}