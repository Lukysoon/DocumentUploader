namespace DocumentService;

public class DocumentDto
{
    public string FileName { get; set; } = null!;
    public string DataInBase64 { get; set; } = null!;
    public IEnumerable<string> Tags { get; set; } = Enumerable.Empty<string>();

    public DocumentDto(string fileName, string dataInBase64, IEnumerable<string> tags)
    {
        FileName = fileName;
        DataInBase64 = dataInBase64;
        Tags = tags;
    }
}
