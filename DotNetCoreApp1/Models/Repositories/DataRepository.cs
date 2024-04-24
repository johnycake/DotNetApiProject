using DotNetCoreApp1.Controllers.Types;
using DotNetCoreApp1.Models.Interfaces;
using DotNetCoreApp1.Models.Types;
using Microsoft.EntityFrameworkCore;

namespace DotNetCoreApp1.Models.Repositories
{
    public class DataRepository : IDataRepository
    {
        private readonly AppDbContext _appDbContext;

        public DataRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<(IEnumerable<DataDto>, PaginationMetadata?)> GetData(string? orderBy, string? searchQuery, bool? descending, int? pageNumber, int? pageSize)
        {
            var collection = _appDbContext.Data as IQueryable<DataDto>;
            PaginationMetadata? paginationMetadata = null;

            if (!string.IsNullOrWhiteSpace(searchQuery))
            {
                searchQuery = searchQuery.Trim();
                collection = collection.Where(c => 
                    c.Title.Contains(searchQuery) 
                    || (!string.IsNullOrEmpty(c.Description) && c.Description.Contains(searchQuery)) 
                );
            }

            if (!string.IsNullOrWhiteSpace(orderBy))
            {
                orderBy = orderBy.Trim();
                collection = (descending ?? false)
                    ? collection.OrderByDescending(c => EF.Property<object>(c, orderBy))
                    : collection.OrderBy(c => EF.Property<object>(c, orderBy));
            }
            else
            {
                collection = collection.OrderBy(c => c.Title);
            }

            if (pageNumber != null && pageSize != null)
            {
                var totalItemCount = await collection.CountAsync();
                paginationMetadata = new PaginationMetadata(totalItemCount, (int)pageSize, (int)pageNumber);

                collection = collection
                    .Skip((int)(pageSize * (pageNumber - 1)))
                    .Take((int)pageSize);
            }

            var collectionToReturn = await collection.ToListAsync();
            return (collectionToReturn, paginationMetadata);
        }

        public async Task<DataDto?> GetDataById(Guid dataId)
        {
            return await _appDbContext.Data.AsNoTracking().FirstOrDefaultAsync(r => r.DataId == dataId);
        }

        public async Task CreateData(DataDto dataTocreate)
        {
            await _appDbContext.Data.AddAsync(dataTocreate);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task UpdateData(DataDto dataToUpdate)
        {
            _appDbContext.Data.Update(dataToUpdate);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task DeleteData(DataDto dataToDelete)
        {
            _appDbContext.Remove(dataToDelete);
            await _appDbContext.SaveChangesAsync();
        }
    }
}
