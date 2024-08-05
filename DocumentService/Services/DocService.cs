using DocumentService;
using DocumentUploader.DocumentService.Data;
using DocumentUploader.DocumentService.Entities;
using DocumentUploader.DocumentService.Repositories;

namespace DocumentUploader.DocumentService.Services;

public class DocService : IDocService
{
    private readonly ApplicationDbContext _context;
    private readonly IDocumentRepository _documentRepository;
    public DocService(ApplicationDbContext context, DocumentRepository documentRepository)
    {
        _context = context;    
        _documentRepository = documentRepository;
    }

    public bool Exists(Guid documentId)
    {
        bool documentExists = _documentRepository.Exists(documentId);
        return documentExists;
    }

    public void Remove(Guid documentId)
    {
        _documentRepository.Remove(documentId);
    }

    public void Upload(DocumentDto document)
    {

        _documentRepository.UploadDocument(document);
    }

    public bool IsValid(DocumentDto document)
    {
        bool isValid = !IsBase64String(document.DataInBase64);
        return isValid;
    }

    private bool IsBase64String(string base64)
    {
        Span<byte> buffer = new Span<byte>(new byte[base64.Length]);
        return Convert.TryFromBase64String(base64, buffer , out int bytesParsed);
    }

    private Document ParseDocument(DocumentDto documentDto)
    {
        List<Tag> tags = ParseTags(documentDto.Tags);
        Document document = new Document(documentDto.FileName, documentDto.DataInBase64, tags);
        return document;
    }

    private List<Tag> ParseTags(IEnumerable<string> tagNames)
    {
        List<Tag> tags = tagNames.Select(t => new Tag(t)).ToList();
        return tags;
    }
}
