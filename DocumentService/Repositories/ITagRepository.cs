
namespace DocumentService;

public interface ITagRepository
{
    void CreateTags(IEnumerable<Tag> tags);
}
