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

    public void CreateMissingTags(IEnumerable<string> tagNames)
    {
        try
        {
            List<Tag> tags = _tagRepository.GetTags(tagNames);
            List<Tag> missingTags = GetMissingTags(tags);
            CreateTags(missingTags);
        }
        catch (Exception ex)
        {
            throw new Exception("Error in creating missing tags", ex);
        }
    }

    public void RemoveUnusedTags(Guid documentId)
    {
        try
        {
            List<Tag> tags = _tagRepository.GetTagsForDocument(documentId);
            List<Tag> unusedTags = tags.Where(t => !t.Documents.Any()).ToList();

            _tagRepository.RemoveTags(unusedTags);
        }
        catch (Exception ex)
        {
            throw new Exception("Error in removing unused tags", ex);
        }
    }

    private void CreateTags(IEnumerable<Tag> tags)
    {
        _tagRepository.CreateTags(tags);
    }

    private List<Tag> GetMissingTags(IEnumerable<Tag> tags)
    {
        HashSet<Tag> existingTags = _context.Tags.Where(t => tags.Contains(t)).Select(t => t).ToHashSet();
        List<Tag> missingTags = tags.Where(t => !existingTags.Contains(t)).ToList();

        return missingTags;
    }
}
