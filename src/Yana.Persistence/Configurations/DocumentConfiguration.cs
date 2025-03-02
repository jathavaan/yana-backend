namespace Yana.Persistence.Configurations;

public class DocumentConfiguration : IEntityTypeConfiguration<Document>
{
    public void Configure(EntityTypeBuilder<Document> builder)
    {
        builder.ToTable(x => x.IsTemporal());

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .ValueGeneratedNever()
            .IsRequired();

        builder.Property(x => x.Type)
            .IsRequired();

        builder.Property(x => x.Title)
            .IsRequired();

        builder.Property(x => x.CreatedDate)
            .IsRequired();

        builder
            .HasMany(x => x.Users)
            .WithMany(x => x.Documents)
            .UsingEntity<DocumentHasUser>()
            .HasKey(x => new { x.DocumentId, x.UserId });

        builder
            .HasMany(x => x.Tags)
            .WithMany(x => x.Documents)
            .UsingEntity<DocumentHasTag>()
            .HasKey(x => new { x.DocumentId, x.TagId });

        builder
            .HasMany(x => x.Tiles)
            .WithOne(x => x.Document)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();

        builder
            .HasMany(x => x.ParentDocuments)
            .WithMany(x => x.ChildDocuments)
            .UsingEntity<DocumentReference>(
                r => r
                    .HasOne(x => x.ParentDocument)
                    .WithMany()
                    .HasForeignKey(x => x.ParentDocumentId)
                    .OnDelete(DeleteBehavior.Restrict),
                l => l
                    .HasOne(x => x.ChildDocument)
                    .WithMany()
                    .HasForeignKey(x => x.ChildDocumentId)
                    .OnDelete(DeleteBehavior.Restrict)
            )
            .HasKey(x => new { x.ParentDocumentId, x.ChildDocumentId });
    }
}