namespace Yana.Persistence.Configurations;

public class DocumentLayoutConfiguration : IEntityTypeConfiguration<DocumentLayout>
{
    public void Configure(EntityTypeBuilder<DocumentLayout> builder)
    {
        builder.ToTable(x => x.IsTemporal());

        builder.HasKey(x => new { x.TileId, x.LayoutSize });

        builder.Property(x => x.LayoutSize)
            .IsRequired()
            .ValueGeneratedNever();

        builder.Property(x => x.XPosition)
            .IsRequired();

        builder.Property(x => x.YPosition)
            .IsRequired();

        builder.Property(x => x.Width)
            .IsRequired();

        builder.Property(x => x.Height)
            .IsRequired();

        builder.HasOne(x => x.Tile)
            .WithMany(x => x.DocumentLayouts)
            .OnDelete(DeleteBehavior.Cascade)
            .HasForeignKey(x => x.TileId)
            .HasPrincipalKey(x => x.Id)
            .IsRequired();

        builder.HasOne(x => x.Document)
            .WithMany(x => x.DocumentLayouts)
            .OnDelete(DeleteBehavior.ClientCascade)
            .IsRequired();
    }
}