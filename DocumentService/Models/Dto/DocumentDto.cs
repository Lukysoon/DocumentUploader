namespace DocumentService;

public class DocumentDto
{
    public string FileName { get; set; } = null!;
    public string DataInBase64 { get; set; } = null!;
    public List<string> Tags { get; set; } = new List<string>();
}
