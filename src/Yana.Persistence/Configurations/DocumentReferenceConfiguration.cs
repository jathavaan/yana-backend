namespace Yana.Persistence.Configurations;

public class DocumentReferenceConfiguration : IEntityTypeConfiguration<DocumentReference>
{
    public void Configure(EntityTypeBuilder<DocumentReference> builder)
    {
        // builder.HasKey(x => new { x.ParentDocumentId, x.ChildDocumentId });
        builder.HasAlternateKey(x => x.Id);

        builder.Property(x => x.Id)
            .IsRequired();
    }
}