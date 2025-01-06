namespace Yana.Persistence.Configurations;

public class DocumentHasUserConfiguration : IEntityTypeConfiguration<DocumentHasUser>
{
    public void Configure(EntityTypeBuilder<DocumentHasUser> builder)
    {
        builder.Property(x => x.Role)
            .IsRequired()
            .ValueGeneratedNever();
    }
}