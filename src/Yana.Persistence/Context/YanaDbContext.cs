namespace Yana.Persistence.Context;

public class YanaDbContext(DbContextOptions<YanaDbContext> options) : DbContext
{
    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(YanaDbContext).Assembly);
    }
}