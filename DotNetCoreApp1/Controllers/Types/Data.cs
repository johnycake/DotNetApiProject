namespace DotNetCoreApp1.Controllers.Types
{
    public class Data
    {
        public required string Title { get; set; }
        public required string Description { get; set; }
        public required string Genre { get; set; }
        public string? Content { get; set; }
    }
}
