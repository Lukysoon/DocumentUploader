using System.ComponentModel.Design;

namespace DocumentService;

public class TagRepository : ITagRepository
{
    private readonly ApplicationDbContext _context;
    public TagRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public void CreateTags(IEnumerable<Tag> tags)
    {
        _context.Tags.AddRange(tags);
    }
    
}
