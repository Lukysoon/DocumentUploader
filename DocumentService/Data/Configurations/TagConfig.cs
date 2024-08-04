using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using DocumentUploader.DocumentService.Entities;

namespace DocumentService
{
    internal class TagConfig : IEntityTypeConfiguration<Tag>
    {
        public void Configure(EntityTypeBuilder<Tag> builder)
        {
            builder.Property(t => t.Id).HasDefaultValueSql("NEWID()").IsRequired();
            builder.Property(t => t.Name);
        }
    }
}
