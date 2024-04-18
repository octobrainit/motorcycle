namespace Motorcycle.App.Api.Cache
{
    public interface ICacheService
    {
        Task Register(IFormFile data, CancellationToken cancellation);
        Task<IFormFile> GetFile(string key, CancellationToken cancellationToken);
    }
}
