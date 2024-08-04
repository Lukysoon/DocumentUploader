using DocumentService;

namespace DocumentUploader.DocumentService.Entities;

public class Tag
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public List<DocumentTag> DocumentTags { get; set; } = new List<DocumentTag>();
}
