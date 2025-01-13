using System.ComponentModel.DataAnnotations;
using DocumentService.Validation;

namespace DocumentService;

public class DocumentDto
{
    [Required]
    [MaxLength(50)]
    public string FileName { get; set; } = null!;
    [IsBase64]
    public string DataInBase64 { get; set; } = null!;
    [Required, MinLength(1, ErrorMessage = "Add at least one tag please.")]
    public List<string> Tags { get; set; } = new List<string>();

    public DocumentDto() {}

    public DocumentDto(string fileName, string dataInBase64, List<string> tags)
    {
        FileName = fileName;
        DataInBase64 = dataInBase64;
        Tags = tags;
    }
}
