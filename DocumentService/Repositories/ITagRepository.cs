
using DocumentUploader.DocumentService.Entities;

namespace DocumentUploader.DocumentService.Repositories;

public interface ITagRepository
{
    void CreateTags(IEnumerable<Tag> tags);
    List<Tag> GetTagsForDocument(Guid documentId);
    void RemoveTags(IEnumerable<Tag> unusedTags);
    List<Tag> GetTags(IEnumerable<string> tagNames);
    List<Tag> GetTagsIfExists(IEnumerable<string> tagNames);
}
