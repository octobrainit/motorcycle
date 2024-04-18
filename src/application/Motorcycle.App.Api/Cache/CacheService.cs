
using MediatR;
using motorcycle.shared.CreationalBase;
using Motorcycle.App.Api.Blob;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace Motorcycle.App.Api.Cache
{
    public class CacheService : ICacheService
    {
        private readonly CacheConfig _config;
        private readonly ILogger<CacheService> _logger;
        private readonly Lazy<ConnectionMultiplexer> _lazyConnection;
        private readonly IMediator _mediator;

        public CacheService(ILogger<CacheService> logger, CacheConfig config, IMediator mediator)
        {
            _config = config;
            _logger = logger;
            _lazyConnection = new Lazy<ConnectionMultiplexer>(() =>
            {
                return ConnectionMultiplexer.Connect(_config.Connection);
            });
            _mediator = mediator;
        }

        public async Task<IFormFile> GetFile(string key, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Getting file from cache");

            var redis = _lazyConnection.Value;
            var db = redis.GetDatabase();

            string data = await db.StringGetAsync(key);

            if (!string.IsNullOrEmpty(data))
            {
                var fileInfo = JsonConvert.DeserializeObject<FileCacheInfo>(data);

                if (!string.IsNullOrEmpty(fileInfo.Base64Content))
                {
                    byte[] fileContent = Convert.FromBase64String(fileInfo.Base64Content);

                    var memoryStream = new MemoryStream(fileContent);

                    return new FormFile(memoryStream, 0, memoryStream.Length,
                                        fileInfo.Name, fileInfo.FileName);
                }
                else
                {
                    throw BaseError.Create(MessageType.InfraestructureValidation, "File content not found");
                }
            }
            else
            {
                throw BaseError.Create(MessageType.InfraestructureValidation, "File not found");
            }
        }

        public async Task Register(IFormFile data, CancellationToken cancellation)
        {
            _logger.LogInformation("Registering file on Cache");

            // Convert file content to base64
            string base64Content;
            using (var memoryStream = new MemoryStream())
            {
                await data.CopyToAsync(memoryStream);
                byte[] fileBytes = memoryStream.ToArray();
                base64Content = Convert.ToBase64String(fileBytes);
            }

            var fileMetadata = new FileCacheInfo
            {
                Name = data.FileName,
                FileName = data.FileName,
                ContentType = data.ContentType,
                Length = data.Length,
                Base64Content = base64Content
            };

            string jsonData = JsonConvert.SerializeObject(fileMetadata);

            var redis = _lazyConnection.Value;
            var db = redis.GetDatabase();

            await db.StringSetAsync(data.FileName, jsonData, TimeSpan.FromMinutes(25), flags: CommandFlags.None);

            _logger.LogInformation("File cached");

            _logger.LogInformation("Dispatch event to blob");

            _mediator.Publish(new BlobFileMessage { FileName = data.FileName }, cancellation);

            _logger.LogInformation("Blob Event sent");
        }
    }
}
