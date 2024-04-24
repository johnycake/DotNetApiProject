using DotNetCoreApp1.Models.Types;

namespace DotNetCoreApp1.Models.Interfaces
{
    public interface IDocumentRepository
    {
        public Task<IEnumerable<DocumentDto>> GetAllDocuments();
        public Task<DocumentDto?> GetDocument(Guid uuid);
        public Task CreateDocument(DocumentDto documentToCreate);
        public Task UpdateDocument(DocumentDto documentToUpdate);
        public Task DeleteDocument(DocumentDto documentToDelete);

    }
}
