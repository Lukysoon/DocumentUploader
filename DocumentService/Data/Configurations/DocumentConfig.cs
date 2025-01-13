using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using DocumentUploader.DocumentService.Entities;

namespace DocumentService
{
    internal class DocumentConfig : IEntityTypeConfiguration<Document>
    {
        public void Configure(EntityTypeBuilder<Document> builder)
        {
            builder.Property(d => d.FileName).IsRequired();
            builder.Property(d => d.DataInBase64).IsRequired();

            builder
                .HasMany(d => d.Tags)
                .WithMany(t => t.Documents);
        }
    }
}
