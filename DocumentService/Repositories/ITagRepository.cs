
using DocumentUploader.DocumentService.Entities;

namespace DocumentUploader.DocumentService.Repositories;

public interface ITagRepository
{
    void CreateTags(IEnumerable<Tag> tags);
}
