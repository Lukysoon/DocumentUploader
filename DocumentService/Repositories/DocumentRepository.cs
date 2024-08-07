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
        try
        {
            Document? document = _context.Documents.FirstOrDefault(d => d.Id == documentId);
            if (document != null)
                _context.Documents.Remove(document);
                _context.SaveChanges();
        }
        catch (Exception ex)
        {
            throw new Exception("Remove document failed", ex);
        }
    }

    public void UploadDocument(Document document)
    {
        try
        {
            _context.Documents.Add(document);
            _context.SaveChanges();
        }
        catch (Exception ex)
        {
            throw new Exception("Uploading document failed", ex);
        }
    }

    public bool Exists(Guid documentId)
    {
        try
        {
            bool exists = _context.Documents.Any();
            return exists;
        }
        catch (Exception ex)
        {
            throw new Exception("Does file exists? failed", ex);
        }
    }

    public IEnumerable<Document> GetDocuments(IEnumerable<string> tagNames)
    {
        try
        {
            IEnumerable<Document> documents = 
                _context.Documents
                .Where(d => d.Tags
                    .Any(t => tagNames.Contains(t.Name)));
            
            return documents;
        }
        catch (Exception ex)
        {
            throw new Exception("Getting documents failed", ex);
        }
    }
}
