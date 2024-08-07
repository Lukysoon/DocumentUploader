using DocumentService;
using DocumentUploader.DocumentService.Entities;
using DocumentUploader.DocumentService.Services;
using Microsoft.AspNetCore.Mvc;

namespace DocumentUploader.DocumentService.Controllers 
{
    [ApiController]
    [Route("document")]
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
                if (!ModelState.IsValid || !_documentService.IsDtoValid(document)) 
                    return StatusCode(StatusCodes.Status400BadRequest, "Model state is not valid");

                _tagService.CreateMissingTags(document.Tags);
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
        public IActionResult RemoveDocument(Guid documentId)
        {
            try
            {
                if (!ModelState.IsValid || !_documentService.Exists(documentId))
                    return StatusCode(StatusCodes.Status400BadRequest, "Model state is not valid");

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
