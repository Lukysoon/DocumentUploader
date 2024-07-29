namespace DocumentUploader.DocumentService.Entities;

public class Tag
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public List<Document> documents { get; set; } = new List<Document>();
}
