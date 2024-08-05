using DocumentService;

namespace DocumentUploader.DocumentService.Entities;

public class Document
{
    public Guid Id { get; set; }
    public string FileName { get; set; } = null!;
    public string DataInBase64 { get; set; } = null!;
    public List<Tag> Tags { get; set; } = new List<Tag>();

    public Document(string fileName, string dataInBase64)
    {
        FileName = fileName;
        DataInBase64 = dataInBase64;
    }
}
