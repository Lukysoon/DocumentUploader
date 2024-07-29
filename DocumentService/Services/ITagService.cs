using DocumentUploader.DocumentService.Entities;

namespace DocumentUploader.DocumentService.Services;

public interface ITagService
{
    void CreateMissingTags(IEnumerable<Tag> tags);
    void RemoveUnusedTags(Guid documentId);
}
