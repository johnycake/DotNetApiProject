using DotNetCoreApp1.Controllers.Types;
using DotNetCoreApp1.Models;
using DotNetCoreApp1.Models.Types;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Document = DotNetCoreApp1.Controllers.Types.Document;

namespace DotNetCoreApp1_Test.Controllers.TestData
{
    public class DocumentController_Fixture : IDisposable
    {
        private readonly DbContextOptions<AppDbContext> _options;
        public AppDbContext AppDbContext { get; private set; }
        public static List<DataDto> FakeDataDtoList => [
                new() {
                        DataId = new Guid("801a9493-c0d4-4027-ab19-5943bc965588"),
                        Title = "Elektrotechnika pre zaciatocnikov",
                        Description = "pajkovanie, suciastky atd",
                        Content = "blablabla"
                },
                new() {
                        DataId = new Guid("62b15c71-7109-4eb8-ac14-ee60d7d5575a"),
                        Title = "Medicina pre vsetkych",
                        Description = "ako uzivat acylpirin",
                        Content = "blablabla"
                },
                new() {
                        DataId = new Guid("1faaab6f-fbbb-406e-9a28-c4052ca131a5"),
                        Title = "Programovanie",
                        Description = "Dot Net developement",
                        Content = "blabla"
                },
            ];
        public static List<DocumentDto> FakeDocumentDtoList => [
                new() {
                        DocumentId = new Guid("3728a9c0-4ed0-4584-4fa7-08dc41aa37ae"),
                        DataId = new Guid("801a9493-c0d4-4027-ab19-5943bc965588"),
                        Tags = [
                          "Tag1"
                        ],
                },
                new() {
                        DocumentId = new Guid("8c28c9cf-06e8-4d0f-8ce6-3b84c93fff18"),
                        DataId = new Guid("62b15c71-7109-4eb8-ac14-ee60d7d5575a"),
                        Tags = [
                          "Tag2"
                        ],
                },
                new() {
                        DocumentId = new Guid("b9aece44-1ec7-46f2-86d1-dbe01a1ef6de"),
                        DataId = new Guid("1faaab6f-fbbb-406e-9a28-c4052ca131a5"),
                        Tags = [
                          "Tag3"
                        ],
                },
            ];

        public Document NewDocumentToCreate = new() {
            DataId = new Guid("1faaab6f-fbbb-406e-9a28-c4052ca131a5"),
            Tags = [
               "NewlyAddedTag"
            ],
        };
        public DocumentDto DocumentDtoToUpdate = new() {
            DocumentId = new Guid("b9aece44-1ec7-46f2-86d1-dbe01a1ef6de"),
            DataId = new Guid("1faaab6f-fbbb-406e-9a28-c4052ca131a5"),
            Tags = [
               "NewTag"
            ],
        };
        public Guid DocumentIdToFind = new("b9aece44-1ec7-46f2-86d1-dbe01a1ef6de");
        public Guid DocumentIdToDelete = new("8c28c9cf-06e8-4d0f-8ce6-3b84c93fff18");
        public DocumentController_Fixture()
        {
            _options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "DocumentsTestDatabase")
                .EnableSensitiveDataLogging()
                .Options;

            AppDbContext = new AppDbContext(_options);

            SeedDataToDatabase();
        }

        public AppDbContext GetContext()
        {
            return new AppDbContext(_options);
        }

        public void Dispose()
        {
            AppDbContext.Database.EnsureDeleted();
        }

        private async void SeedDataToDatabase()
        {
            AppDbContext.Database.EnsureDeleted();

            foreach (DataDto dataDto in FakeDataDtoList)
            {
                await AppDbContext.Data.AddAsync(dataDto);
            }


            foreach (DocumentDto documentDto in FakeDocumentDtoList)
            {
                await AppDbContext.Documents.AddAsync(documentDto);
            }

            await AppDbContext.SaveChangesAsync();
        }
    }
}
