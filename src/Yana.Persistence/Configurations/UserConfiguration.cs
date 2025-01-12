namespace Yana.Persistence.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<UserProfile>
{
    public void Configure(EntityTypeBuilder<UserProfile> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .ValueGeneratedNever()
            .IsRequired();

        builder.Property(x => x.Email)
            .IsRequired()
            .ValueGeneratedNever();

        builder.Property(x => x.FirstName)
            .IsRequired();

        builder.Property(x => x.LastName)
            .IsRequired();

        builder.Property(x => x.CreatedDate)
            .IsRequired();

        builder
            .HasMany(x => x.Documents)
            .WithMany(x => x.Users)
            .UsingEntity<DocumentHasUser>()
            .HasKey(x => new { x.DocumentId, x.UserId });

        builder
            .HasMany(x => x.Tiles)
            .WithMany(x => x.Users)
            .UsingEntity<TileHasUser>()
            .HasKey(x => new { x.TileId, x.UserId });

        builder
            .HasMany(x => x.Tags)
            .WithOne(x => x.UserProfile)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasMany(x => x.Citations)
            .WithOne(x => x.UserProfile)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasMany(x => x.ExternalUserProfiles)
            .WithOne(x => x.User)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();
    }
}