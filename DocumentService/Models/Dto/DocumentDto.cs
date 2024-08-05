namespace DocumentService;

public class DocumentDto
{
    public string FileName { get; set; } = null!;
    public string DataInBase64 { get; set; } = null!;
    public List<string> Tags { get; set; } = new List<string>();

    public DocumentDto() {}

    public DocumentDto(string fileName, string dataInBase64, List<string> tags)
    {
        FileName = fileName;
        DataInBase64 = dataInBase64;
        Tags = tags;
    }
}
