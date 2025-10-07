using Microsoft.AspNetCore.Mvc;
using XmlGeneratorAPI.Contracts;
using XmlGeneratorAPI.Requests;

namespace XmlGeneratorAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SSCCController(ISSCCService ssccService) : ControllerBase
{
    /// <summary>
    /// Creates a new SSCC for a Logistic Unit.
    /// </summary>
    /// <param name="logisticUnitId">The Logistic Unit ID to attach SSCC.</param>
    /// <param name="gs1companyPrefix">GS1 Company Prefix.</param>
    /// <param name="extensionDigit">Extension Digit (0–9).</param>
    [HttpPost("create")]
    public async Task<ActionResult<string>> Create(CreateSSCCRequest request)
    {
        return Ok(await ssccService.CreateAsync(request));
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
