using System.ComponentModel.Design;
using DocumentUploader.DocumentService.Data;
using DocumentUploader.DocumentService.Entities;

namespace DocumentUploader.DocumentService.Repositories;

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
