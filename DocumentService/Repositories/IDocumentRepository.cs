 namespace DocumentService;

public interface IDocumentRepository
{
    void UploadDocument(Document document);
    bool Exists(Guid documentId);
    void Remove(Guid documentId);
}
