namespace Yana.Persistence.Configurations;

public class DocumentReferenceConfiguration : IEntityTypeConfiguration<DocumentReference>
{
    public void Configure(EntityTypeBuilder<DocumentReference> builder)
    {
        builder.HasKey(x => new { x.ParentDocument, x.ChildDocument });
        builder.HasAlternateKey(x => x.Id);

        builder.Property(x => x.Id)
            .IsRequired();

        builder
            .HasOne(x => x.ParentDocument)
            .WithMany(x => x.DocumentReferences)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();

        builder
            .HasOne(x => x.ChildDocument)
            .WithMany(x => x.DocumentReferences)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();
    }
}