using DocumentService;

namespace DocumentUploader.DocumentService.Entities;

public class Document
{
    public Guid Id { get; set; }
    public string FileName { get; set; } = null!;
    public string DataInBase64 { get; set; } = null!;
    public List<DocumentTag> DocumentTags { get; set; } = new List<DocumentTag>();
}
