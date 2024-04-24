using DotNetCoreApp1.Models.Interfaces;
using DotNetCoreApp1.Models.Types;
using Microsoft.EntityFrameworkCore;

namespace DotNetCoreApp1.Models.Repositories
{
    public class DocumentRepository : IDocumentRepository
    {
        private readonly AppDbContext _appDbContext;

        public DocumentRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<IEnumerable<DocumentDto>> GetAllDocuments()
        {
            return await _appDbContext.Documents.ToListAsync();
        }

        public async Task<DocumentDto?> GetDocument(Guid uuid)
        {
            return await _appDbContext.Documents.AsNoTracking().FirstOrDefaultAsync(r => r.DocumentId == uuid);
        }

        public async Task CreateDocument(DocumentDto documentToCreate)
        {
            await _appDbContext.Documents.AddAsync(documentToCreate);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task UpdateDocument(DocumentDto documentToUpdate)
        {
            _appDbContext.Documents.Update(documentToUpdate);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task DeleteDocument(DocumentDto documentToDelete)
        {
            _appDbContext.Remove(documentToDelete);
            await _appDbContext.SaveChangesAsync();
        }
    }
}
