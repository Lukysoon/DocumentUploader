using DocumentUploader.DocumentService.Entities;

namespace DocumentUploader.DocumentService.Services;

public interface ITagService
{
    void RemoveUnusedTags(Guid documentId);
}
