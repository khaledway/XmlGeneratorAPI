using Microsoft.AspNetCore.Mvc;
using System.Text;
using XmlGeneratorAPI.Data;
using XmlGeneratorAPI.Dtos;
using XmlGeneratorAPI.Enums;
using XmlGeneratorAPI.Mappers;
using XmlGeneratorAPI.Requests;
using XmlGeneratorAPI.Services;

namespace XmlGeneratorAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class XMLHandlingController : ControllerBase
    {
        private readonly IEventService _eventService;
        private readonly ApplicationDbContext _db;
        private readonly IWebHostEnvironment _env;

        public XMLHandlingController(ApplicationDbContext db, IEventService eventService, IWebHostEnvironment env)
        {
            _db = db;
            _eventService = eventService;
            _env = env;
        }

        /// <summary>
        /// Generate EPCIS XML based on BizStep and request parameters
        /// </summary>
        [HttpPost("generate")]
        public async Task<IActionResult> GenerateXML([FromBody] EpcisEventRequest request)
        {
            try
            {
                // Get predefined fields based on BizStep
                var predefinedFields = BizStepDefaults.Resolve(request.BizStep);

                // Extract SGTIN list from uploaded CSV file
                var sgtinList = await _eventService.ExtractSgtinListFromFile(request.SGITNCsvFileID);

                if (sgtinList == null || !sgtinList.Any())
                {
                    return BadRequest("No valid SGTIN data found in the uploaded file.");
                }

                // Generate XML using the event service
                var xml = await _eventService.CreateEvent(request.BizStep, request, predefinedFields, sgtinList);

                // Save generated XML to disk
                var fileName = $"{request.BizStep}_{DateTime.UtcNow:yyyyMMddHHmmss}";
                var savedFileName = await _eventService.SaveGeneratedXml(xml, fileName);

                return Ok(new
                {
                    success = true,
                    message = "XML generated successfully",
                    fileName = savedFileName,
                    downloadUrl = $"/api/XMLHandling/download/{savedFileName}",
                    xmlContent = xml,
                    sgtinCount = sgtinList.Count
                });
            }
            catch (FileNotFoundException ex)
            {
                return NotFound(new { success = false, message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = $"Error generating XML: {ex.Message}" });
            }
        }

        /// <summary>
        /// Download generated XML file
        /// </summary>
        [HttpGet("download/{fileName}")]
        public async Task<IActionResult> DownloadXML([FromRoute] string fileName)
        {
            try
            {
                var basePath = Path.Combine(_env.WebRootPath ?? "wwwroot", "generated");
                var filePath = Path.Combine(basePath, fileName);

                if (!System.IO.File.Exists(filePath))
                {
                    return NotFound(new { success = false, message = "Generated XML file not found." });
                }

                var bytes = await System.IO.File.ReadAllBytesAsync(filePath);
                return File(bytes, "application/xml", fileName);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = $"Error downloading file: {ex.Message}" });
            }
        }

        /// <summary>
        /// Get list of all generated XML files
        /// </summary>
        [HttpGet("list")]
        public IActionResult ListGeneratedFiles()
        {
            try
            {
                var basePath = Path.Combine(_env.WebRootPath ?? "wwwroot", "generated");

                if (!Directory.Exists(basePath))
                {
                    return Ok(new { success = true, files = new List<object>() });
                }

                var files = Directory.GetFiles(basePath, "*.xml")
                    .Select(f => new FileInfo(f))
                    .OrderByDescending(f => f.CreationTime)
                    .Select(f => new
                    {
                        fileName = f.Name,
                        createdAt = f.CreationTime,
                        size = f.Length,
                        downloadUrl = $"/api/XMLHandling/download/{f.Name}"
                    })
                    .ToList();

                return Ok(new { success = true, files });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = $"Error listing files: {ex.Message}" });
            }
        }

        /// <summary>
        /// Preview XML without saving
        /// </summary>
        [HttpPost("preview")]
        public async Task<IActionResult> PreviewXML([FromBody] EpcisEventRequest request)
        {
            try
            {
                var predefinedFields = BizStepDefaults.Resolve(request.BizStep);
                var sgtinList = await _eventService.ExtractSgtinListFromFile(request.SGITNCsvFileID);

                if (sgtinList == null || !sgtinList.Any())
                {
                    return BadRequest("No valid SGTIN data found in the uploaded file.");
                }

                var xml = await _eventService.CreateEvent(request.BizStep, request, predefinedFields, sgtinList);

                return Ok(new
                {
                    success = true,
                    xmlContent = xml,
                    sgtinCount = sgtinList.Count,
                    bizStep = request.BizStep.ToString()
                });
            }
            catch (FileNotFoundException ex)
            {
                return NotFound(new { success = false, message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = $"Error previewing XML: {ex.Message}" });
            }
        }

        /// <summary>
        /// Delete a generated XML file
        /// </summary>
        [HttpDelete("delete/{fileName}")]
        public IActionResult DeleteGeneratedFile([FromRoute] string fileName)
        {
            try
            {
                var basePath = Path.Combine(_env.WebRootPath ?? "wwwroot", "generated");
                var filePath = Path.Combine(basePath, fileName);

                if (!System.IO.File.Exists(filePath))
                {
                    return NotFound(new { success = false, message = "File not found." });
                }

                System.IO.File.Delete(filePath);
                return Ok(new { success = true, message = "File deleted successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = $"Error deleting file: {ex.Message}" });
            }
        }
    }
}