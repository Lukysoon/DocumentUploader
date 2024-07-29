﻿using DocumentUploader.DocumentService.Entities;
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
        public IActionResult UploadDocument(Document document)
        {
            if (!ModelState.IsValid) return StatusCode(400);

            if (!_documentService.IsValid(document)) return StatusCode(400);
            
            _tagService.CreateMissingTags(document.Tags);
            _documentService.Upload(document);

            return StatusCode(200);
        }

        [HttpDelete]
        [Route("delete")]
        public IActionResult RemoveDocument(Guid documentId)
        {
            if (!ModelState.IsValid) return StatusCode(400);

            if (!_documentService.Exists(documentId)) return StatusCode(404);

            _documentService.Remove(documentId);

            return StatusCode(200);
        }
    }
}