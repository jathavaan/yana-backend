namespace Yana.Persistence.Context;

public class YanaDbContext(DbContextOptions<YanaDbContext> options) : DbContext(options)
{
    public virtual DbSet<Citation> Citations { get; set; }
    public virtual DbSet<Document> Documents { get; set; }
    public virtual DbSet<DocumentHasTag> DocumentHasTag { get; set; }
    public virtual DbSet<DocumentHasUser> DocumentHasUser { get; set; }
    public virtual DbSet<DocumentReference> DocumentReferences { get; set; }
    public virtual DbSet<Tag> Tags { get; set; }
    public virtual DbSet<Tile> Tiles { get; set; }
    public virtual DbSet<TileHasUser> TileHasUser { get; set; }
    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(YanaDbContext).Assembly);
    }
}