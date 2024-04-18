
using MediatR;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Motorcycle.App.Api.Cache;
using Motorcycle.App.Api.Features.v1.UploadImageFile;

namespace Motorcycle.App.Api.Blob
{
    public class BlobFileMessageService : IBlobService, INotificationHandler<BlobFileMessage>
    {
        private readonly BlobConfig _config;
        private readonly IUploadImageFileRepository _repository;
        private readonly ICacheService _cache;

        public BlobFileMessageService(BlobConfig config, IUploadImageFileRepository repository, ICacheService cache)
        {
            _config = config;
            _repository = repository;
            _cache = cache;
        }

        public async Task Handle(BlobFileMessage notification, CancellationToken cancellationToken) 
        {

            var file = await _cache.GetFile(notification.FileName, cancellationToken);
            await UploadFile(file, cancellationToken);
        }
            

        public async Task UploadFile(IFormFile file, CancellationToken cancellationToken)
        {
            string storageConnectionString = _config.Connection;
            string containerName = "motorcyclefiles";
            string blobName = file.FileName;

            CloudStorageAccount storageAccount;

            if (CloudStorageAccount.TryParse(storageConnectionString, out storageAccount))
            {
                CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
                CloudBlobContainer container = blobClient.GetContainerReference(containerName);

                await container.CreateIfNotExistsAsync();

                CloudBlockBlob blob = container.GetBlockBlobReference(blobName);

                var fileStream = file.OpenReadStream();
                await blob.UploadFromStreamAsync(fileStream);

                Console.WriteLine("File uploaded to Azure Blob Storage.");

                // Update driver CNH image path
                var driver = await _repository.FindCNHById(Guid.Parse(file.FileName.Split('.')[0]), cancellationToken);
                driver.CNH.ChangeImagePath(blob.Uri.ToString());
                await _repository.Update(driver);
                await _repository.SaveChanges();
                Console.WriteLine("File path changed on CNH");
            }
            else
            {
                Console.WriteLine("Invalid storage connection string.");
            }
        }
    }
}
