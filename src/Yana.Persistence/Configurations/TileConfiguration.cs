namespace Yana.Persistence.Configurations;

public class TileConfiguration : IEntityTypeConfiguration<Tile>
{
    public void Configure(EntityTypeBuilder<Tile> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd()
            .IsRequired();

        builder.Property(x => x.Content)
            .ValueGeneratedNever()
            .HasColumnType("ntext");

        builder.Property(x => x.XPosition)
            .ValueGeneratedNever()
            .IsRequired();

        builder.Property(x => x.YPosition)
            .ValueGeneratedNever()
            .IsRequired();

        builder.Property(x => x.Height)
            .ValueGeneratedNever()
            .IsRequired();

        builder.Property(x => x.Width)
            .ValueGeneratedNever();

        builder.Property(x => x.CreatedDate)
            .IsRequired();

        builder
            .HasOne(x => x.Document)
            .WithMany(x => x.Tiles)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();

        builder
            .HasMany(x => x.Users)
            .WithMany(x => x.Tiles)
            .UsingEntity<TileHasUser>()
            .HasKey(x => new { x.Tile, x.User });
    }
}