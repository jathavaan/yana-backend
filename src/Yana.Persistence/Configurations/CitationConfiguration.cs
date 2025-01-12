namespace Yana.Persistence.Configurations;

public class CitationConfiguration : IEntityTypeConfiguration<Citation>
{
    public void Configure(EntityTypeBuilder<Citation> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd()
            .IsRequired();

        builder.Property(x => x.PublicationYear)
            .IsRequired();

        builder.Property(x => x.Title)
            .IsRequired();

        builder.Property(x => x.SourceType)
            .IsRequired();

        builder.Property(x => x.RetrievedDate)
            .IsRequired();

        builder
            .HasOne(x => x.UserProfile)
            .WithMany(x => x.Citations)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasOne(x => x.Document)
            .WithMany(x => x.Citations)
            .OnDelete(DeleteBehavior.SetNull);
    }
}