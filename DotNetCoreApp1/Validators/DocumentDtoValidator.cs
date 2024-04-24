using DotNetCoreApp1.Models.Interfaces;
using DotNetCoreApp1.Models.Types;
using FluentValidation;

namespace DotNetCoreApp1.Validators
{
    public class DocumentDtoValidator : AbstractValidator<DocumentDto>
    {
        public DocumentDtoValidator(IDataRepository dataRepository, IDocumentRepository documentRepository)
        {
            RuleFor(r => r.DocumentId)
                .NotEmpty().WithMessage("Required value!")
                .MustAsync(async (documentId, _) =>
                {
                    return await documentRepository.GetDocument(documentId) != null;
                }).WithMessage("Document ID does not exist");
            RuleFor(r => r.DataId)
                .NotEmpty().WithMessage("Required value!")
                .MustAsync(async (dataId, _) =>
                {
                    return await dataRepository.GetDataById(dataId) != null;
                }).WithMessage("Data ID does not exist");
        }
    }
}
