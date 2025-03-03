namespace Yana.Application.ViewModels;

public sealed record DocumentVm(string Id, string Title, ICollection<TagVm> Tags, ICollection<TileVm> Tiles);