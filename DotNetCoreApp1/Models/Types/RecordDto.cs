using System.ComponentModel.DataAnnotations;

namespace DotNetCoreApp1.Models.Types
{
    public class DocumentDto
    {
        [Key]
        public required Guid DocumentId { get; set; }
        public required Guid DataId { get; set; }
        public string[]? Tags { get; set; }
    }
}
