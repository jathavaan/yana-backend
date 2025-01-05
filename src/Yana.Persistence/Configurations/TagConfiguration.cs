namespace Yana.Persistence.Configurations;

public class TagConfiguration : IEntityTypeConfiguration<Tag>
{
    public void Configure(EntityTypeBuilder<Tag> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd()
            .IsRequired();

        builder.Property(x => x.Name)
            .IsRequired();

        builder.Property(x => x.DateCreated)
            .IsRequired();

        builder
            .HasOne(x => x.User)
            .WithMany(x => x.Tags)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasMany(x => x.Documents)
            .WithMany(x => x.Tags)
            .UsingEntity<DocumentHasTag>()
            .HasKey(x => new { x.Document, x.Tag });
    }
}