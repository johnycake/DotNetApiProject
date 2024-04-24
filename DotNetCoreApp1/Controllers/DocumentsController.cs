using AutoMapper;
using DotNetCoreApp1.Controllers.Types;
using DotNetCoreApp1.Models;
using DotNetCoreApp1.Models.Interfaces;
using DotNetCoreApp1.Models.Types;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System;

namespace DotNetCoreApp1.Controllers
{
    [ApiController]
    [Authorize(Policy = "MustBeUser")]
    [Route("api/documents")]

    public class DocumentsController : ControllerBase
    {
        private readonly IDocumentRepository _documentRepository;
        private readonly IMapper _mapper;
        private readonly IValidator<Document> _documentValidator;
        private readonly IValidator<DocumentDto> _documentDtoValidator;

        public DocumentsController(
            IDocumentRepository documentRepository,
            IMapper mapper,
            IValidator<Document> documentDtoValidator,
            IValidator<DocumentDto> documentValidator
            )
        {
            _documentRepository = documentRepository;
            _documentValidator = documentDtoValidator;
            _documentDtoValidator = documentValidator;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DocumentDto>>> GetDocuments()
        {
            var allDocuments = await _documentRepository.GetAllDocuments();
            return Ok(allDocuments);
        }

        [HttpGet("{documentId}")]
        public async Task<ActionResult<DocumentDto>> GetDocument(Guid documentId)
        {
            var foundDocument = await _documentRepository.GetDocument(documentId);
            if (foundDocument == null) { return BadRequest("Document with this ID not found"); }
            return Ok(foundDocument);
        }

        [HttpPost("create")]
        public async Task<ActionResult<Guid>> CreateDocument(Document documentToCreate)
        {
            if (documentToCreate == null) { return ValidationProblem("document is null"); }

            var result = await _documentValidator.ValidateAsync(documentToCreate);

            if (!result.IsValid)
            {
                return ValidationProblem(new ValidationProblemDetails(result.ToDictionary()));
            }

            var newDocument = _mapper.Map<DocumentDto>(documentToCreate);

            await _documentRepository.CreateDocument(newDocument);
            return Ok(newDocument.DocumentId);
        }

        [HttpPut]
        public async Task<ActionResult<Guid>> UpdateDocument(DocumentDto documentToUpdate)
        {
            if (documentToUpdate == null) { return BadRequest("document is null"); }

            var result = await _documentDtoValidator.ValidateAsync(documentToUpdate);

            if (!result.IsValid)
            {
                return ValidationProblem(new ValidationProblemDetails(result.ToDictionary()));
            }

            await _documentRepository.UpdateDocument(documentToUpdate);
            return Ok(documentToUpdate.DocumentId);
        }

        [HttpPatch("{documentId}")]
        public async Task<ActionResult<Guid>> PartiallyUpdateDocument(Guid documentId, JsonPatchDocument<DocumentDto> documentPatchDocument)
        {
            if (documentPatchDocument == null) { return BadRequest("document is null"); }
            var documentToPatch = await _documentRepository.GetDocument(documentId);
            if (documentToPatch == null) { return BadRequest("document not found"); }

            documentPatchDocument.ApplyTo<DocumentDto>(documentToPatch, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!TryValidateModel(documentToPatch))
            {
                return BadRequest(ModelState);
            }

            await _documentRepository.UpdateDocument(documentToPatch);
            return Ok(documentToPatch.DocumentId);
        }

        [HttpDelete("{documentId}")]
        public async Task<ActionResult<Guid>> DeleteDocument(Guid documentId)
        {
            var documentToDelete = await _documentRepository.GetDocument(documentId);
            if (documentToDelete == null) { return BadRequest("document ID no found"); }
            await _documentRepository.DeleteDocument(documentToDelete);
            return Ok(documentToDelete.DocumentId);
        }
    }
}
