using AutoMapper;
using DotNetCoreApp1.Controllers.Types;
using DotNetCoreApp1.Models;
using DotNetCoreApp1.Models.Interfaces;
using DotNetCoreApp1.Models.Types;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.Json;

namespace DotNetCoreApp1.Controllers
{
    [ApiController]
    [Authorize(Policy = "MustBeUser")]
    [Route("api/data")]

    public class DataController : ControllerBase
    {
        private readonly IDataRepository _dataRepository;
        private readonly IMapper _mapper;
        private readonly IValidator<Data> _dataDtoValidator;
        private readonly IValidator<DataDto> _dataValidator;
        private const int maximumPageSize = 20;

        public DataController(
            IDataRepository dataRepository,
            IMapper mapper,
            IValidator<Data> dataDtoValidator,
            IValidator<DataDto> dataValidator
            )
        {
            _dataRepository = dataRepository;
            _dataDtoValidator = dataDtoValidator;
            _dataValidator = dataValidator;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<DocumentDto>> GetData(
            string? orderBy,
            [FromQuery(Name = "fullTextSearchQuery")] string? searchQuery,
            [FromQuery(Name = "sortOrder")] string? order,
            int? pageNumber,
            int? pageSize)
        {
            if (pageSize > maximumPageSize)
            {
                pageSize = maximumPageSize;
            }

            var (foundData, paginationMetadata) = await _dataRepository.GetData(orderBy, searchQuery, order?.Equals("DESC"), pageNumber, pageSize);

            if (paginationMetadata != null)
            {
                Response.Headers["X-Pagination"] = JsonSerializer.Serialize(paginationMetadata);
            }

            return Ok(foundData);
        }

        [HttpGet("{dataId}")]
        public async Task<ActionResult<DataDto>> GetData(Guid dataId)
        {
            var foundData = await _dataRepository.GetDataById(dataId);
            if (foundData == null) { return BadRequest("Data not found"); }
            return Ok(foundData);
        }

        [HttpPost("create")]
        public async Task<ActionResult<DataDto>> CreateData(Data? dataToCreate)
        {
            if (dataToCreate == null) { return BadRequest("Data is null"); }

            var result = await _dataDtoValidator.ValidateAsync(dataToCreate);

            if (!result.IsValid)
            {
                return ValidationProblem(new ValidationProblemDetails(result.ToDictionary()));
            }

            var newData = _mapper.Map<DataDto>(dataToCreate);

            await _dataRepository.CreateData(newData);
            return Ok(newData.DataId);
        }

        [HttpPut]
        public async Task<ActionResult<DataDto>> UpdateData(DataDto? dataToUpdate)
        {
            if (dataToUpdate == null) { return BadRequest("Data is null"); }

            var result = await _dataValidator.ValidateAsync(dataToUpdate);

            if (!result.IsValid)
            {
                return ValidationProblem(new ValidationProblemDetails(result.ToDictionary()));
            }

            await _dataRepository.UpdateData(dataToUpdate);
            return Ok(dataToUpdate.DataId);
        }

        [HttpDelete("{dataId}")]
        public async Task<ActionResult<Guid>> DeleteData(Guid dataId)
        {
            var dataToDelete = await _dataRepository.GetDataById(dataId);
            if (dataToDelete == null) { return BadRequest("Data not found"); }
            await _dataRepository.DeleteData(dataToDelete);
            return Ok(dataToDelete.DataId);
        }
    }
}
