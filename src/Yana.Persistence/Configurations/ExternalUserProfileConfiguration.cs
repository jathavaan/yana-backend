namespace Yana.Persistence.Configurations;

public class ExternalUserProfileConfiguration : IEntityTypeConfiguration<ExternalUserProfile>
{
    public void Configure(EntityTypeBuilder<ExternalUserProfile> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .IsRequired()
            .ValueGeneratedNever();

        builder.Property(x => x.AuthProvider)
            .IsRequired();

        builder.Property(x => x.CreatedDate)
            .IsRequired()
            .ValueGeneratedNever();

        builder
            .HasOne(x => x.User)
            .WithMany(x => x.ExternalUserProfiles)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();
    }
}