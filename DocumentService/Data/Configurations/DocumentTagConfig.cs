using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using DocumentUploader.DocumentService.Entities;

namespace DocumentService
{
    internal class DocumentTagConfig : IEntityTypeConfiguration<DocumentTag>
    {
        public void Configure(EntityTypeBuilder<DocumentTag> builder)
        {
            builder
                .HasKey(dt => new { dt.DocumentId, dt.TagId });  

            builder
                .HasOne(dt => dt.Document)
                .WithMany(d => d.DocumentTags	)
                .HasForeignKey(dt => dt.DocumentId);

            builder
                .HasOne(dt => dt.Tag)
                .WithMany(t => t.DocumentTags)
                .HasForeignKey(dt => dt.TagId);

        }
    }
}
