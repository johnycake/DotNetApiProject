using DotNetCoreApp1.Controllers.Types;
using DotNetCoreApp1.Models.Types;

namespace DotNetCoreApp1.Models.Interfaces
{
    public interface IDataRepository
    {
        public Task<(IEnumerable<DataDto>, PaginationMetadata?)> GetData(string? orderBy, string? searchQuery, bool? descending, int? pageNumber, int? pageSize);
        public Task<DataDto?> GetDataById(Guid dataId);
        public Task CreateData(DataDto dataTocreate);
        public Task UpdateData(DataDto dataToUpdate);
        public Task DeleteData(DataDto dataToDelete);
    }
}
