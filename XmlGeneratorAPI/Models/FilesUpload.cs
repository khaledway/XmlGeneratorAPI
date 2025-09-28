using System;
using System.ComponentModel.DataAnnotations;

namespace XmlGeneratorAPI.Models
{
    public class FilesUpload
    {
        [Key]
        public Guid Id { get; set; }

        /// <summary>
        /// Base path that contains the 'uploads' folder (e.g., wwwroot)
        /// </summary>
        [Required]
        [MaxLength(1024)]
        public string Path { get; set; } = string.Empty;

        /// <summary>
        /// Relative folder name where the file is stored (uploads/{Id})
        /// </summary>
        [Required]
        [MaxLength(1024)]
        public string Folder { get; set; } = string.Empty;

        /// <summary>
        /// Original file name as uploaded by the client
        /// </summary>
        [Required]
        [MaxLength(512)]
        public string OriginalFileName { get; set; } = string.Empty;

        /// <summary>
        /// Stored file name on disk (e.g., data.csv)
        /// </summary>
        [Required]
        [MaxLength(256)]
        public string StoredFileName { get; set; } = "data.csv";

        /// <summary>
        /// UTC upload time
        /// </summary>
        public DateTime UploadedAtUtc { get; set; } = DateTime.UtcNow;
    }
}
