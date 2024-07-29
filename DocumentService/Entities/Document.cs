namespace DocumentUploader.DocumentService.Entities;

public class Document
{
    public Guid Id { get; set; }
    public string FileName { get; set; } = null!;
    public List<Tag> Tags { get; set; } = new List<Tag>();
    public string DataInBase64 { get; set; } = null!;
}
