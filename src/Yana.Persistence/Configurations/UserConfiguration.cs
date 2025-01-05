namespace Yana.Persistence.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .ValueGeneratedNever()
            .IsRequired();

        builder.Property(x => x.AuthProvider)
            .HasDefaultValue(AuthProvider.Google);

        builder
            .HasMany(x => x.Documents)
            .WithMany(x => x.Users)
            .UsingEntity<DocumentHasUser>()
            .HasKey(x => new { x.Document, x.User });

        builder
            .HasMany(x => x.Tiles)
            .WithMany(x => x.Users)
            .UsingEntity<TileHasUser>()
            .HasKey(x => new { x.Tile, x.User });

        builder
            .HasMany(x => x.Tags)
            .WithOne(x => x.User)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasMany(x => x.Citations)
            .WithOne(x => x.User)
            .OnDelete(DeleteBehavior.Cascade);
    }
}