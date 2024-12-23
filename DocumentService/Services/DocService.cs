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
    public DocService(ApplicationDbContext context, IDocumentRepository documentRepository)
    {
        _context = context;    
        _documentRepository = documentRepository;
    }

    public bool Exists(Guid documentId)
    {
        try
        {
            bool documentExists = _documentRepository.Exists(documentId);
            return documentExists;
        }
        catch (Exception ex)
        {
            throw new Exception("Error in determining whether document exists", ex);
        }
    }

    public void Remove(Guid documentId)
    {
        try
        {
            _documentRepository.Remove(documentId);
        }
        catch (Exception ex)
        {
            throw new Exception("Error in removing document", ex);
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
            throw new Exception("Error in uploading document", ex);
        }
    }

    public bool IsDtoValid(DocumentDto document)
    {
        try
        {
            bool isValid = IsBase64String(document.DataInBase64);
            return isValid;
        }
        catch (Exception ex)
        {
            throw new Exception("Error in validating document", ex);
        }
    }

    private bool IsBase64String(string base64)
    {
        Span<byte> buffer = new Span<byte>(new byte[base64.Length]);
        return Convert.TryFromBase64String(base64, buffer , out int bytesParsed);
    }

    public List<DocumentDto> GetDocuments(IEnumerable<string> tags)
    {
        try
        {
            List<Document> documents = _documentRepository.GetDocuments(tags);
            List<DocumentDto> documentDtos = ParseDocuments(documents);

            return documentDtos;
        }
        catch (Exception ex)
        {
            throw new Exception("Error in getting documents", ex);
        }
    }

    private List<DocumentDto> ParseDocuments(IEnumerable<Document> documents)
    {
        List<DocumentDto> documentDtos = documents.Select(d => 
        {
            IEnumerable<string> tagNames = d.Tags.Select(t => t.Name);
            return new DocumentDto(d.FileName, d.DataInBase64, tagNames.ToList());
        }).ToList();

        return documentDtos;
    }

    private Document ParseDocument(DocumentDto documentDto)
    {
        List<Tag> existingTags = _tagRepository.GetTagsIfExists(documentDto.Tags);

        List<string> newTagsNames = documentDto.Tags.Where(t => existingTags.Select(t => t.Name).Contains(t)).ToList();
        List<Tag> newTags = newTagsNames.Select(tn => new Tag(tn)).ToList(); 

        Document document = new Document(documentDto.FileName, documentDto.DataInBase64)
        {
            Tags = existingTags.Concat(newTags).ToList()
        };

        return document;
    }
}
