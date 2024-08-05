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

    public void Upload(DocumentDto documentDto)
    {
        Document document = ParseDocument(documentDto);
        _documentRepository.UploadDocument(document);
    }

    public bool IsDtoValid(DocumentDto document)
    {
        bool isValid = !IsBase64String(document.DataInBase64);
        return isValid;
    }

    private bool IsBase64String(string base64)
    {
        Span<byte> buffer = new Span<byte>(new byte[base64.Length]);
        return Convert.TryFromBase64String(base64, buffer , out int bytesParsed);
    }

    public IEnumerable<DocumentDto> GetDocuments(IEnumerable<string> tags)
    {
        IEnumerable<Document> documents = _documentRepository.GetDocuments(tags);
        IEnumerable<DocumentDto> documentDtos = ParseDocuments(documents);

        return documentDtos;
    }

    private IEnumerable<DocumentDto> ParseDocuments(IEnumerable<Document> documents)
    {
        IEnumerable<DocumentDto> documentDtos = documents.ToList().Select(d => 
        {
            IEnumerable<string> tagNames = d.Tags.Select(t => t.Name);
            return new DocumentDto(d.FileName, d.DataInBase64, tagNames);
        });

        return documentDtos;
    }

    private Document ParseDocument(DocumentDto documentDto)
    {
        IEnumerable<Tag> tags = documentDto.Tags.Select(t => new Tag(t));
        Document document = new Document(documentDto.FileName, documentDto.DataInBase64, tags);
        return document;
    }
}
