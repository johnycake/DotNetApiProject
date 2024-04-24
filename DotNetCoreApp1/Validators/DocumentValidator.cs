using DotNetCoreApp1.Controllers.Types;
using DotNetCoreApp1.Models.Interfaces;
using FluentValidation;

namespace DotNetCoreApp1.Validators
{
    public class DocumentValidator : AbstractValidator<Document>
    {
        public DocumentValidator(IDataRepository dataRepository)
        {
            RuleFor(r => r.DataId)
                .NotEmpty().WithMessage("Required value!")
                .MustAsync(async (dataId, _) =>
                    {
                        return await dataRepository.GetDataById(dataId) != null;
                    }).WithMessage("Data ID does not exist");
        }
    }
}
