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
}
