using Microsoft.AspNetCore.Mvc;
using XmlGeneratorAPI.Contracts;
using XmlGeneratorAPI.Requests;

namespace XmlGeneratorAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SSCCController(ISSCCService ssccService) : ControllerBase
{
    /// <summary>
    /// Creates  new SSCC for a Logistic Unit.
    /// </summary>
    /// <param name="logisticUnitId">The Logistic Unit ID to attach SSCC.</param>
    /// <param name="gs1companyPrefix">GS1 Company Prefix.</param>
    /// <param name="extensionDigit">Extension Digit (0–9).</param>
    [HttpPost("create-sscc-for-logistic-unit")]
    public async Task<ActionResult<string>> CreateSsccForLogisticUnit(CreateSSCCForLogisticUnitRequest request)
    {
        return Ok(await ssccService.CreateSsccAndAssignToLogisticUnitAsync(request));
    }

    /// <summary>
    /// Creates new SSCC 
    /// </summary>
    /// <param name="gs1companyPrefix">GS1 Company Prefix.</param>
    /// <param name="extensionDigit">Extension Digit (0–9).</param>
    [HttpPost("create-sscc")]
    public async Task<ActionResult<string>> CreateSscc(CreateSsccRequest request)
    {
        return Ok(await ssccService.CreateSsccAsync(request));
    }


    /// <summary>
    /// Export SSCC Label as PNG Image
    /// </summary>
    /// <param name="ssccCode"></param>
    /// <returns>the path of the exported image</returns>
    [HttpPost("export-label")]
    public async Task<ActionResult<string>> ExportSSCCLabelAsImage(string ssccCode)
    {
        if (!ssccService.IsValid(ssccCode))
            return BadRequest("Invalid SSCC code.");

        return Ok(ssccService.ExportSsccBarcodeLabel(ssccCode));
    }
}
