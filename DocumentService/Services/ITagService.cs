namespace DocumentService;

public interface ITagService
{
    void CreateMissingTags(IEnumerable<Tag> tags);
}
