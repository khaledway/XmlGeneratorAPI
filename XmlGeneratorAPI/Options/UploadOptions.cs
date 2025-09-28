namespace XmlGeneratorAPI.Options
{
    public class UploadOptions
    {
        public string BasePath { get; set; } = "wwwroot";
        public long MaxFileSizeBytes { get; set; } = 5 * 1024 * 1024; // default 5MB
    }
}
