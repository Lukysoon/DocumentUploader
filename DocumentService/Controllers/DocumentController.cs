using DocumentService;
using DocumentService.Validation.CustomAtributes;
using DocumentUploader.DocumentService.Entities;
using DocumentUploader.DocumentService.Services;
using Microsoft.AspNetCore.Mvc;

namespace DocumentUploader.DocumentService.Controllers 
{
    [ApiController]
    [Route("api/document")]
    public class DocumentController : Controller
    {
        private readonly ITagService _tagService;
        private readonly IDocService _documentService;

        public DocumentController(ITagService tagService, IDocService documentService)
        {
            _tagService = tagService;
            _documentService = documentService;
        }

        [HttpPost]
        [Route("upload")]
        public IActionResult UploadDocument(DocumentDto document)
        {
            try
            {
                _documentService.Upload(document);

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);                
            }
        }

        [HttpDelete]
        [Route("delete")]
        public IActionResult RemoveDocument([DocumentExists] Guid documentId)
        {
            try
            {
                _documentService.Remove(documentId);
                _tagService.RemoveUnusedTags(documentId);

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);   
            }
        }

        [HttpGet]
        public IActionResult GetDocuments([FromQuery] IEnumerable<string> tags)
        {
            try
            {
                IEnumerable<DocumentDto> documents = _documentService.GetDocuments(tags);

                return Ok(documents);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);               
            }
        }
    }
}
