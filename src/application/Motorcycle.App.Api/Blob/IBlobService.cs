namespace Motorcycle.App.Api.Blob
{
    public interface IBlobService
    {
        Task UploadFile(IFormFile file, CancellationToken cancellationToken);
    }
}
