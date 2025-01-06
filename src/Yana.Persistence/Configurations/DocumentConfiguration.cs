namespace Yana.Persistence.Configurations;

public class DocumentConfiguration : IEntityTypeConfiguration<Document>
{
    public void Configure(EntityTypeBuilder<Document> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .ValueGeneratedNever()
            .IsRequired();

        builder.Property(x => x.Type)
            .IsRequired();

        builder.Property(x => x.GridSize)
            .IsRequired();

        builder.Property(x => x.CreatedDate)
            .IsRequired();

        builder
            .HasMany(x => x.Users)
            .WithMany(x => x.Documents)
            .UsingEntity<DocumentHasUser>()
            .HasKey(x => new { x.Document, x.User });

        builder
            .HasMany(x => x.Tags)
            .WithMany(x => x.Documents)
            .UsingEntity<DocumentHasTag>()
            .HasKey(x => new { x.Document, x.Tag });

        builder
            .HasMany(x => x.Tiles)
            .WithOne(x => x.Document)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();

        builder
            .HasMany(x => x.Citations)
            .WithOne(x => x.Document)
            .OnDelete(DeleteBehavior.SetNull);

        builder
            .HasMany(x => x.DocumentReferences)
            .WithOne(x => x.ParentDocument)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade)
            .HasForeignKey(x => x.ParentDocument);

        builder
            .HasMany(x => x.DocumentReferences)
            .WithOne(x => x.ChildDocument)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade)
            .HasForeignKey(x => x.ChildDocument);
    }
}