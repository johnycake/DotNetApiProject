using System.ComponentModel.DataAnnotations;

namespace DotNetCoreApp1.Models.Types
{
    public class DataDto
    {
        [Key]
        public Guid DataId { get; set; }
        public required string Title { get; set; }
        public required string Description { get; set; }
        public string? Content { get; set; }
    }
}
