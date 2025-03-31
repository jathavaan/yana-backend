namespace Yana.Persistence.Configurations;

public class DocumentHasUserConfiguration : IEntityTypeConfiguration<DocumentHasUser>
{
    public void Configure(EntityTypeBuilder<DocumentHasUser> builder)
    {
        builder.HasKey(x => new { x.DocumentId, x.UserId });

        builder.Property(x => x.Role)
            .IsRequired()
            .ValueGeneratedNever();
    }
}