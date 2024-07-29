using DocumentUploader.DocumentService.Data;
using DocumentUploader.DocumentService.Entities;
using DocumentUploader.DocumentService.Repositories;

namespace DocumentUploader.DocumentService.Services;

public class TagService : ITagService
{
    private readonly ApplicationDbContext _context;
    private readonly ITagRepository _tagRepository;
    public TagService(ApplicationDbContext context, ITagRepository tagRepository)
    {
        _context = context;
        _tagRepository = tagRepository;
    }

    public void CreateMissingTags(IEnumerable<Tag> tags)
    {
        List<Tag> missingTags = GetTagsWhichNotExists(tags);
        CreateTags(missingTags);
    }

    private void CreateTags(IEnumerable<Tag> tags)
    {
        _tagRepository.CreateTags(tags);
    }

    private List<Tag> GetTagsWhichNotExists(IEnumerable<Tag> tags)
    {
        HashSet<Tag> existingTags = _context.Tags.Where(t => tags.Contains(t)).Select(t => t).ToHashSet();
        List<Tag> missingTags = tags.Where(t => !existingTags.Contains(t)).ToList();

        return missingTags;
    }
}
