
namespace DocumentService;

public interface IDocService
{
    bool Exists(Guid documentId);
    void Remove(Guid documentId);
    void Upload(Document document);
    bool IsValid(Document document);
}
