using System;
using System.ComponentModel.DataAnnotations;
using DocumentUploader.DocumentService.Data;

namespace DocumentService.Validation.CustomAtributes;

public class DocumentExistsAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        var dbContext = (ApplicationDbContext)validationContext.GetService(typeof(ApplicationDbContext))!;
        
        var documentId = (Guid)value!;

        if (!dbContext.Documents.Any(d => d.Id == documentId))
        {
            return new ValidationResult("Document does not exist.");
        }

        return ValidationResult.Success;
    }
}