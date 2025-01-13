using System;
using System.ComponentModel.DataAnnotations;

namespace DocumentService.Validation;

public class IsBase64Attribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        var data = validationContext.ObjectInstance;
        if (!IsBase64String((string)data))
        {
            return new ValidationResult($"Property {validationContext.DisplayName} must be in base64.");
        }

        return ValidationResult.Success;
    }

    private bool IsBase64String(string base64)
    {
        if (string.IsNullOrWhiteSpace(base64))
        {
            throw new Exception("Document data in base64 format is null.");
        }

        Span<byte> buffer = new Span<byte>(new byte[base64.Length]);
        return Convert.TryFromBase64String(base64, buffer, out int bytesParsed);
    }
}
