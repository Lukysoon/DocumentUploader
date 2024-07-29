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
        throw new NotImplementedException();
    }

    public void UploadDocument(Document document)
    {
        _context.Documents.Add(document);
    }

    public bool Exists(Guid documentId)
    {
        throw new NotImplementedException();
    }
}
