using DotNetCoreApp1.Controllers.Types;
using FluentValidation;

namespace DotNetCoreApp1.Validators
{
    public class DataValidator : AbstractValidator<Data>
    {
        public DataValidator()
        {
            RuleFor(r => r.Title)
                .NotEmpty().WithMessage("Required value!");
            RuleFor(r => r.Description)
                .NotEmpty().WithMessage("Required value!");
            RuleFor(r => r.Genre)
                .NotEmpty().WithMessage("Required value!");
        }
    }
}
