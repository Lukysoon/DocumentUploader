using System.ComponentModel.Design;
using DocumentUploader.DocumentService.Data;
using DocumentUploader.DocumentService.Entities;
using Microsoft.EntityFrameworkCore;

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
        _context.SaveChanges();
    }

    public List<Tag> GetTagsForDocument(Guid documentId)
    {
       Document? document = 
        _context.Documents
        .Include(d => d.Tags)
        .FirstOrDefault(d => d.Id == documentId);

        if (document == null) throw new Exception();

        return document.Tags;
    }

    public void RemoveTags(List<Tag> unusedTags)
    {
        _context.Tags.RemoveRange(unusedTags);
    }

    public List<Tag> GetTags(IEnumerable<string> tagNames)
    {
        List<Tag> tags = _context.Tags.Where(t => tagNames.Contains(t.Name)).ToList();
        return tags;
    }
}
