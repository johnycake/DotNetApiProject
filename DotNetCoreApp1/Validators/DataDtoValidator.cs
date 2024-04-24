using DotNetCoreApp1.Models.Interfaces;
using DotNetCoreApp1.Models.Repositories;
using DotNetCoreApp1.Models.Types;
using FluentValidation;

namespace DotNetCoreApp1.Validators
{
    public class DataDtoValidator : AbstractValidator<DataDto>
    {
        public DataDtoValidator(IDataRepository dataRepository)
        {
            RuleFor(r => r.DataId)
                .NotEmpty().WithMessage("Required value!")
                        .MustAsync(async (dataId, _) =>
                        {
                            return await dataRepository.GetDataById(dataId) != null;
                        }).WithMessage("Data ID does not exist");
            RuleFor(r => r.Title)
                .NotEmpty().WithMessage("Required value!");
            RuleFor(r => r.Description)
                .NotEmpty().WithMessage("Required value!");
        }
    }
}
