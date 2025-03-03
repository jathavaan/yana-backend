using Yana.Application.Contracts.TagService;

namespace Yana.Infrastructure.Services.TagService;

public class TagRepositoryService : ITagRepositoryService
{
    private readonly YanaDbContext _dbContext;

    public TagRepositoryService(YanaDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ICollection<Tag>> GetTags(ICollection<int> tagIds)
        => await _dbContext.Tags
            .Where(x => tagIds.Contains(x.Id))
            .ToListAsync();
}