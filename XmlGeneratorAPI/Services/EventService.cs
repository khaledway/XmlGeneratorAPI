using Microsoft.EntityFrameworkCore;
using System.Text;
using XmlGeneratorAPI.Data;
using XmlGeneratorAPI.Dtos;
using XmlGeneratorAPI.Enums;
using XmlGeneratorAPI.Requests;
using XmlGeneratorAPI.Strategies;

namespace XmlGeneratorAPI.Services
{
    public class EventService : IEventService
    {
        private readonly ApplicationDbContext _db;
        private readonly BizStepStrategyFactory _strategyFactory;
        private readonly IWebHostEnvironment _env;

        public EventService(ApplicationDbContext db, BizStepStrategyFactory strategyFactory, IWebHostEnvironment env)
        {
            _db = db;
            _strategyFactory = strategyFactory;
            _env = env;
        }

        public async Task<string> CreateEvent(BizStep bizStep, EpcisEventRequest request, EpcisPredefinedFieldsDto predefinedFields, List<string> sgtinList)
        {
            // Get the appropriate strategy based on BizStep
            var strategy = _strategyFactory.GetStrategy(bizStep);

            // Generate XML using the strategy
            var xml = strategy.GenerateXml(request, predefinedFields, sgtinList);

            return xml;
        }

        public async Task<List<string>> ExtractSgtinListFromFile(Guid fileId)
        {
            var fileRecord = await _db.FilesUploads
                .AsNoTracking()
                .FirstOrDefaultAsync(f => f.Id == fileId);

            if (fileRecord == null)
                throw new FileNotFoundException($"File with ID {fileId} not found.");

            var fullPath = Path.Combine(fileRecord.Path, fileRecord.Folder, fileRecord.StoredFileName);

            if (!File.Exists(fullPath))
                throw new FileNotFoundException($"Physical file not found at {fullPath}");

            var content = await File.ReadAllTextAsync(fullPath, Encoding.UTF8);
            var lines = content.Replace("\r\n", "\n").Replace("\r", "\n").Split('\n');

            var sgtinList = new List<string>();
            for (int i = 1; i < lines.Length; i++)
            {
                var trimmed = lines[i].Trim();
                if (!string.IsNullOrWhiteSpace(trimmed))
                {
                    sgtinList.Add(trimmed);
                }
            }
            return sgtinList;
        }

        public async Task<string> SaveGeneratedXml(string xml, string fileName)
        {
            var id = Guid.NewGuid();
            var basePath = Path.Combine(_env.WebRootPath ?? "wwwroot", "generated");
            Directory.CreateDirectory(basePath);

            var fullFileName = $"{fileName}_{id}.xml";
            var filePath = Path.Combine(basePath, fullFileName);

            await File.WriteAllTextAsync(filePath, xml, Encoding.UTF8);

            return fullFileName;
        }
    }
}