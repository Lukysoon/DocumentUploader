using DocumentUploader.DocumentService.Data;
using DocumentUploader.DocumentService.Entities;

namespace DocumentUploader.DocumentService.Repositories;

public class DocumentRepository : IDocumentRepository
{
    private readonly ApplicationDbContext _context;

    public DocumentRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public void Remove(Guid documentId)
    {
        Document? document = _context.Documents.FirstOrDefault(d => d.Id == documentId);
        if (document != null)
            _context.Documents.Remove(document);
    }

    public void UploadDocument(Document document)
    {
        _context.Documents.Add(document);
    }

    public bool Exists(Guid documentId)
    {
        bool exists = _context.Documents.Any();
        return exists;
    }

    public IEnumerable<Document> GetDocuments(IEnumerable<string> tagNames)
    {
        IEnumerable<Document> documents = 
            _context.Documents
            .Where(d => d.Tags
                .Any(t => tagNames.Contains(t.Name)));
        
        return documents;
    }
}
