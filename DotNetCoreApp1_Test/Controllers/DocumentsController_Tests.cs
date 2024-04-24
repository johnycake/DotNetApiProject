using AutoMapper;
using DotNetCoreApp1.AutoMapperProfiles;
using DotNetCoreApp1.Controllers;
using DotNetCoreApp1.Models.Repositories;
using DotNetCoreApp1.Models.Types;
using DotNetCoreApp1.Validators;
using DotNetCoreApp1_Test.Controllers.TestData;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace UnitTesting.Controllers
{
    public class Documents : IClassFixture<DocumentController_Fixture>
    {
        private readonly DocumentController_Fixture fixture;
        private IMapper Mapper { get; }
        public Documents(DocumentController_Fixture documentController_Fixture)
        {
            fixture = documentController_Fixture;
            Mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile(new DocumentProfile())));
        }

        [Fact]
        public async Task Create_Document_Async()
        {
            // Arrange
            using var myAppDbContext = fixture.GetContext();
            var myDataRepository = new DataRepository(myAppDbContext);
            var myDocumentRepository = new DocumentRepository(myAppDbContext);
            DocumentValidator myDocumentValidator = new(myDataRepository);
            DocumentDtoValidator myDocumentDtoValidator = new(myDataRepository, myDocumentRepository);
            DocumentsController sut = new(myDocumentRepository, Mapper, myDocumentValidator, myDocumentDtoValidator);

            // Act
            var actionResult = await sut.CreateDocument(fixture.NewDocumentToCreate);

            // Assert
            Assert.NotNull(actionResult);
            var result = (actionResult.Result as OkObjectResult)?.Value as Guid?;
            var foundNewlyAddedDocument = myAppDbContext.Documents.AsNoTracking().FirstOrDefault(u => u.DocumentId == result);
            Assert.Equal(fixture.NewDocumentToCreate.DataId, foundNewlyAddedDocument?.DataId);
        }

        [Fact]
        public async void Find_Document_By_ID()
        {
            // Arrange
            using var myAppDbContext = fixture.GetContext();
            var myDataRepository = new DataRepository(myAppDbContext);
            var myDocumentRepository = new DocumentRepository(myAppDbContext);
            DocumentValidator myDocumentValidator = new(myDataRepository);
            DocumentDtoValidator myDocumentDtoValidator = new(myDataRepository, myDocumentRepository);
            DocumentsController sut = new(myDocumentRepository, Mapper, myDocumentValidator, myDocumentDtoValidator);

            // Act
            var actionResult = await sut.GetDocument(fixture.DocumentIdToFind);

            // Assert
            Assert.NotNull(actionResult);
            var result = actionResult.Result as OkObjectResult;
            var documentDtoSutResult = result?.Value as DocumentDto;
            Assert.Equal(fixture.DocumentIdToFind, documentDtoSutResult?.DocumentId);
        }

        [Fact]
        public async Task Delete_Document_Async()
        {
            // Arrange
            using var myAppDbContext = fixture.GetContext();
            var myDataRepository = new DataRepository(myAppDbContext);
            var myDocumentRepository = new DocumentRepository(myAppDbContext);
            DocumentValidator myDocumentValidator = new(myDataRepository);
            DocumentDtoValidator myDocumentDtoValidator = new(myDataRepository, myDocumentRepository);
            DocumentsController sut = new(myDocumentRepository, Mapper, myDocumentValidator, myDocumentDtoValidator);

            // Act
            var actionResult = await sut.DeleteDocument(fixture.DocumentIdToDelete);

            // Assert
            Assert.NotNull(actionResult);
            var result = (actionResult.Result as OkObjectResult)?.Value as Guid?;
            Assert.Equal(fixture.DocumentIdToDelete, result);
            var deletedDataSearchResult = myAppDbContext.Data.AsNoTracking().FirstOrDefault(u => u.DataId == result);
            Assert.Null(deletedDataSearchResult);
        }

        [Fact]
        public async Task Update_Document_Async()
        {
            // Arrange
            using var myAppDbContext = fixture.GetContext();
            var myDataRepository = new DataRepository(myAppDbContext);
            var myDocumentRepository = new DocumentRepository(myAppDbContext);
            DocumentValidator myDocumentValidator = new(myDataRepository);
            DocumentDtoValidator myDocumentDtoValidator = new(myDataRepository, myDocumentRepository);
            DocumentsController sut = new(myDocumentRepository, Mapper, myDocumentValidator, myDocumentDtoValidator);

            // Act
            var actionResult = await sut.UpdateDocument(fixture.DocumentDtoToUpdate);

            // Assert
            Assert.NotNull(actionResult);
            var result = (actionResult.Result as OkObjectResult)?.Value as Guid?;
            var foundNewlyUpdatedDocument = myAppDbContext.Documents.AsNoTracking().FirstOrDefault(u => u.DocumentId == result);
            Assert.Equal(fixture.DocumentDtoToUpdate.DocumentId, foundNewlyUpdatedDocument?.DocumentId);
        }
    }
}