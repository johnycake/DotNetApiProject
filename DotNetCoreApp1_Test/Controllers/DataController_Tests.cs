using AutoMapper;
using DotNetCoreApp1.AutoMapperProfiles;
using DotNetCoreApp1.Controllers;
using DotNetCoreApp1.Models;
using DotNetCoreApp1.Models.Repositories;
using DotNetCoreApp1.Models.Types;
using DotNetCoreApp1.Validators;
using DotNetCoreApp1_Test.Controllers.TestData;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using Moq.EntityFrameworkCore;

namespace UnitTesting.Controllers
{
    public class Data : IClassFixture<DataController_Fixture>
    {
        private readonly DataController_Fixture fixture;
        private DataValidator DataValidator { get; }
        private IMapper Mapper { get; }
        public Data(DataController_Fixture dataController_Fixture)
        {
            fixture = dataController_Fixture;
            DataValidator = new DataValidator();
            Mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile(new DataProfile())));
        }

        [Fact]
        public async Task Create_Data_Async()
        {
            // Arrange
            using var myAppDbContext = fixture.GetContext();
            var myDataRepository = new DataRepository(myAppDbContext);
            DataDtoValidator myDataDtoValidator = new(myDataRepository);
            DataController sut = new(myDataRepository, Mapper, DataValidator, myDataDtoValidator);

            // Act
            var actionResult = await sut.CreateData(fixture.NewDataToCreate);

            // Assert
            Assert.NotNull(actionResult);
            var result = (actionResult.Result as OkObjectResult)?.Value as Guid?;
            var foundNewlyAddedData = myAppDbContext.Data.AsNoTracking().FirstOrDefault(u => u.DataId == result);
            Assert.Equal("Zahrakarcenie", foundNewlyAddedData?.Title);
        }

        [Fact]
        public async void Find_Data_By_ID()
        {
            // Arrange
            using var myAppDbContext = fixture.GetContext();
            var myDataRepository = new DataRepository(myAppDbContext);
            DataDtoValidator myDataDtoValidator = new(myDataRepository);
            DataController sut = new(myDataRepository, Mapper, DataValidator, myDataDtoValidator);


            // Act
            var actionResult = await sut.GetData(fixture.dataIdToFind);

            // Assert
            Assert.NotNull(actionResult);
            var result = actionResult.Result as OkObjectResult;
            var userDtoSutResult = result?.Value as DataDto;
            Assert.Equal("Medicina pre vsetkych", userDtoSutResult?.Title);
        }

        [Fact]
        public async Task Delete_Data_Async()
        {
            // Arrange
            using var myAppDbContext = fixture.GetContext();
            var myDataRepository = new DataRepository(myAppDbContext);
            DataDtoValidator myDataDtoValidator = new(myDataRepository);
            DataController sut = new(myDataRepository, Mapper, DataValidator, myDataDtoValidator);
            
            // Act
            var actionResult = await sut.DeleteData(fixture.dataIdToDelete);

            // Assert
            Assert.NotNull(actionResult);
            var result = (actionResult.Result as OkObjectResult)?.Value as Guid?;
            Assert.Equal(fixture.dataIdToDelete, result);
            var deletedDataSearchResult = myAppDbContext.Data.AsNoTracking().FirstOrDefault(u => u.DataId == result);
            Assert.Null(deletedDataSearchResult);
        }

        [Fact]
        public async Task Update_Data_Async()
        {
            // Arrange
            using var myAppDbContext = fixture.GetContext();
            var myDataRepository = new DataRepository(myAppDbContext);
            DataDtoValidator myDataDtoValidator = new(myDataRepository);
            DataController sut = new(myDataRepository, Mapper, DataValidator, myDataDtoValidator);

            // Act
            var actionResult = await sut.UpdateData(fixture.DataDtoToUpdate);

            // Assert
            Assert.NotNull(actionResult);
            var result = (actionResult.Result as OkObjectResult)?.Value as Guid?;
            var foundNewlyUpdatedData = myAppDbContext.Data.AsNoTracking().FirstOrDefault(u => u.DataId== result);
            Assert.Equal("Programovanie .NET", foundNewlyUpdatedData?.Title);
        }
    }
}