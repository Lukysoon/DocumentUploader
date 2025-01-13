﻿using DocumentService;
using DocumentUploader.DocumentService.Data;
using DocumentUploader.DocumentService.Entities;
using DocumentUploader.DocumentService.Repositories;

namespace DocumentUploader.DocumentService.Services;

public class DocService : IDocService
{
    private readonly ApplicationDbContext _context;
    private readonly IDocumentRepository _documentRepository;
    private readonly ITagRepository _tagRepository;
    public DocService(ApplicationDbContext context, IDocumentRepository documentRepository, ITagRepository tagRepository)
    {
        _context = context;    
        _documentRepository = documentRepository;
        _tagRepository = tagRepository;
    }

    public bool Exists(Guid documentId)
    {
        try
        {
            return _documentRepository.Exists(documentId);
        }
        catch (Exception ex)
        {
            throw new Exception("Error checking document existence.", ex);
        }
    }

    public void Remove(Guid documentId)
    {
        try
        {
            if (!_documentRepository.Exists(documentId))
            {
                throw new Exception("File with id " + documentId.ToString() + " not found.");
            }

            _documentRepository.Remove(documentId);
        }
        catch (Exception ex)
        {
            throw new Exception("Error removing document.", ex);
        }
    }

    public void Upload(DocumentDto documentDto)
    {
        try
        {
            Document document = ParseDocument(documentDto);
            _documentRepository.UploadDocument(document);
        }
        catch (Exception ex)
        {
            throw new ArgumentException("Document object is not valid.", ex);
        }
    }

    public List<DocumentDto> GetDocuments(IEnumerable<string> tags)
    {
        if (tags == null)
        {
            throw new ArgumentNullException(nameof(tags));
        }

        try
        {
            List<Document> documents = _documentRepository.GetDocuments(tags);
            return ParseDocuments(documents);
        }
        catch (Exception ex)
        {
            throw new Exception("Error retrieving documents.", ex);
        }
    }

    private List<DocumentDto> ParseDocuments(IEnumerable<Document> documents)
    {
        try
        {
            return documents.Select(d =>
            {
                IEnumerable<string> tagNames = d.Tags?.Select(t => t.Name) ?? Enumerable.Empty<string>();
                return new DocumentDto(d.FileName, d.DataInBase64, tagNames.ToList());
            }).ToList();
        }
        catch (Exception ex)
        {
            throw new Exception("Error parsing documents to DTOs.", ex);
        }
    }

    private Document ParseDocument(DocumentDto documentDto)
    {
        try
        {
            if (_tagRepository == null)
            {
                throw new InvalidOperationException("Tag repository is not initialized.");
            }

            List<Tag> existingTags = _tagRepository.GetTagsIfExists(documentDto.Tags ?? new List<string>());
            
            var newTagsNames = documentDto.Tags?
                .Where(t => !existingTags
                    .Select(et => et.Name)
                    .Contains(t))
                .ToList() ?? new List<string>();

            var newTags = newTagsNames
                .Where(t => t != string.Empty)
                .Select(t => new Tag(t))
                .ToList();

            return new Document(documentDto.FileName, documentDto.DataInBase64)
            {
                Tags = existingTags.Concat(newTags).ToList()
            };
        }
        catch (Exception ex)
        {
            throw new Exception("Error parsing document data.", ex);
        }
    }
}
