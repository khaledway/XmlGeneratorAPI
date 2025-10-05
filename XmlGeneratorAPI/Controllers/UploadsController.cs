using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Text;
using XmlGeneratorAPI.Data;
using XmlGeneratorAPI.Dtos;
using XmlGeneratorAPI.Enums;
using XmlGeneratorAPI.Models;
using XmlGeneratorAPI.Options;
using XmlGeneratorAPI.Validators;

namespace XmlGeneratorAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UploadsController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        private readonly UploadOptions _options;
        private readonly IWebHostEnvironment _env;

        public UploadsController(ApplicationDbContext db, IOptions<UploadOptions> options, IWebHostEnvironment env)
        {
            _db = db;
            _options = options.Value;
            _env = env;
        }
        [HttpPost]
        [Consumes("multipart/form-data")]
        [RequestSizeLimit(long.MaxValue)]
        public async Task<ActionResult<UploadResultDto>> Upload([FromForm] FileUploadForm form)
        {
            var file = form.File;
            if (file == null || file.Length == 0)
                return BadRequest("No file uploaded.");

            var ext = Path.GetExtension(file.FileName).ToLowerInvariant();
            if (ext != ".csv")
                return BadRequest("Only .csv files are allowed.");

            if (file.Length > _options.MaxFileSizeBytes)
                return BadRequest($"File exceeds size limit of {_options.MaxFileSizeBytes} bytes.");

            // Read file content
            string content;
            using (var reader = new StreamReader(file.OpenReadStream(), Encoding.UTF8, detectEncodingFromByteOrderMarks: true))
            {
                content = await reader.ReadToEndAsync();
            }

            if (string.IsNullOrWhiteSpace(content))
                return BadRequest("File is empty.");

            // Normalize line endings and split
            var lines = content.Replace("\r\n", "\n").Replace("\r", "\n").Split('\n');
            var cleanedLines = lines.Where(l => !string.IsNullOrWhiteSpace(l)).ToList();

            if (cleanedLines.Count == 0)
                return BadRequest("No content found in file.");

            // ? Step 1: Validate header
            var header = cleanedLines[0].Trim().ToUpperInvariant();

            if (header != "SGTIN" && header != "SSCC")
                return BadRequest("Invalid or missing header. The first line must be either 'SGTIN' or 'SSCC'.");

            if ((form.Type == FileType.SSCC && header != "SSCC") ||
                (form.Type == FileType.SGTIN && header != "SGTIN"))
            {
                return BadRequest($"File type mismatch: you selected '{form.Type}' but file title is '{header}'.");
            }

            // ? Step 2: Validate data rows
            int validCount = 0;
            for (int i = 1; i < cleanedLines.Count; i++)
            {
                var line = cleanedLines[i].Trim();

                if (string.IsNullOrWhiteSpace(line)) continue;

                if (line.Contains(','))
                    return BadRequest($"Invalid CSV structure at line {i + 1}: only one column (no commas) is allowed.");

                bool isValid = header switch
                {
                    "SGTIN" => SgtinValidator.IsValidSgtin(line),
                    "SSCC" => SSCCValidator.IsValidSscc(line),
                    _ => false
                };

                if (!isValid)
                    return BadRequest($"Invalid {header} format at line {i + 1}: '{line}'.");

                validCount++;
            }

            if (validCount == 0)
                return BadRequest($"No valid {header} rows found.");

            // ? Save file (keep original content as uploaded)
            var id = Guid.NewGuid();
            var basePath = string.IsNullOrWhiteSpace(_options.BasePath) ? "wwwroot" : _options.BasePath;
            var webRoot = Path.IsPathRooted(basePath) ? basePath : Path.Combine(Directory.GetCurrentDirectory(), basePath);
            var folderRel = Path.Combine("uploads", id.ToString());
            var folderAbs = Path.Combine(webRoot, folderRel);
            Directory.CreateDirectory(folderAbs);

            var storedFileName = "data.csv";
            var savePath = Path.Combine(folderAbs, storedFileName);
            await System.IO.File.WriteAllTextAsync(savePath, content.Trim(), Encoding.UTF8);

            // ? Save metadata
            var record = new FilesUpload
            {
                Id = id,
                Path = webRoot,
                Folder = folderRel.Replace('\\', '/'),
                OriginalFileName = file.FileName,
                StoredFileName = storedFileName,
                UploadedAtUtc = DateTime.UtcNow,
                FileType = header
            };

            _db.FilesUploads.Add(record);
            await _db.SaveChangesAsync();

            return CreatedAtAction(nameof(Download), new { id }, new UploadResultDto
            {
                Id = id,
                Folder = record.Folder,
                RowCount = validCount,
                Message = $"Uploaded and validated successfully as {header}."
            });
        }


        [HttpGet("{id:guid}")]
        public async Task<IActionResult> Download([FromRoute] Guid id)
        {
            var record = await _db.FilesUploads.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            if (record == null) return NotFound("File id not found.");

            var fullPath = Path.Combine(record.Path, record.Folder, record.StoredFileName);
            if (!System.IO.File.Exists(fullPath))
                return NotFound("Stored file not found on disk.");

            var bytes = await System.IO.File.ReadAllBytesAsync(fullPath);
            return File(bytes, "text/csv", fileDownloadName: record.StoredFileName);
        }
    }
}
