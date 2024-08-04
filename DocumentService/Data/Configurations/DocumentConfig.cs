using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using DocumentUploader.DocumentService.Entities;

namespace DocumentService
{
    internal class DocumentConfig : IEntityTypeConfiguration<Document>
    {
        public void Configure(EntityTypeBuilder<Document> builder)
        {
            builder.Property(d => d.Id).HasDefaultValueSql("NEWID()").IsRequired();
            builder.Property(d => d.FileName);
            builder.Property(d => d.DataInBase64);
        }
    }
}
