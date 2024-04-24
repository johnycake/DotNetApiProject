using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DotNetCoreApp1.Controllers.Types
{
    public class Document
    {
        public string[]? Tags { get; set; }
        public required Guid DataId { get; set; }
    }
}
