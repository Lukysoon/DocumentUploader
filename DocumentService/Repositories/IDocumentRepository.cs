using DocumentUploader.DocumentService.Entities;

namespace DocumentUploader.DocumentService.Repositories;

public interface IDocumentRepository
{
    void UploadDocument(Document document);
    bool Exists(Guid documentId);
    void Remove(Guid documentId);
}
