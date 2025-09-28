using System;

namespace XmlGeneratorAPI.Dtos
{
    public class UploadResultDto
    {
        public Guid Id { get; set; }
        public string Folder { get; set; } = string.Empty;
        public int RowCount { get; set; }
        public string Message { get; set; } = "Uploaded";
    }
}
