namespace Yana.Application.Contracts.TagService;

public interface ITagRepositoryService
{
    public Task<ICollection<Tag>> GetTags(ICollection<int> tagIds);
}