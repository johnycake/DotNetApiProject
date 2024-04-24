using DotNetCoreApp1.Controllers.Types;
using DotNetCoreApp1.Models;
using DotNetCoreApp1.Models.Types;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace DotNetCoreApp1_Test.Controllers.TestData
{
    public class DataController_Fixture : IDisposable
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
                        Title = "Dot Net developement",
                        Description = "Pre tych s tuzbou vela trpiet :-D",
                        Content = "blabla"
                },
            ];

        public Data NewDataToCreate = new()  {
            Title = "Zahrakarcenie",
            Description = "stepenie stromcekov",
            Genre = "Dom a zahrada",
            Content = "blabla"
        };
        public DataDto DataDtoToUpdate = new() {
            DataId = new Guid("1faaab6f-fbbb-406e-9a28-c4052ca131a5"),
            Title = "Programovanie .NET",
            Description = "Dot Net developement na kazdy den",
            Content = "treba kavu.... a vela"
        };
        public Guid dataIdToFind = new("62b15c71-7109-4eb8-ac14-ee60d7d5575a");
        public Guid dataIdToDelete = new("801a9493-c0d4-4027-ab19-5943bc965588");

        public DataController_Fixture()
        {
            _options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "DataTestDatabase")
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

            await AppDbContext.SaveChangesAsync();
        }
    }
}
