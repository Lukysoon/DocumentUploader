using DocumentService;
using DocumentUploader.DocumentService.Entities;

namespace DocumentUploader.DocumentService.Services;

public interface IDocService
{
    bool Exists(Guid documentId);
    void Remove(Guid documentId);
    void Upload(DocumentDto document);
    bool IsDtoValid(DocumentDto document);
    List<DocumentDto> GetDocuments(IEnumerable<string> tags);
}
