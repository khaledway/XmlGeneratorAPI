namespace XmlGeneratorAPI.Dtos
{
    public class FileUploadForm
    {
        // The field name "file" will appear in Swagger UI
        public IFormFile File { get; set; } = default!;
    }
}
