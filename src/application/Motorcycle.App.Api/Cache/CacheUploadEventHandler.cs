using MediatR;

namespace Motorcycle.App.Api.Cache
{
    public class CacheUploadEventHandler : INotificationHandler<CacheUploadEvent>
    {
        private readonly ILogger<CacheUploadEvent> _logger;
        private readonly ICacheService _cache;

        public CacheUploadEventHandler(ILogger<CacheUploadEvent> logger,ICacheService cache, IMediator mediator)
        {
            _logger = logger;
            _cache = cache;
        }
        public async Task Handle(CacheUploadEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("starting the process to cache file");

            await _cache.Register(notification.File, cancellationToken);

            _logger.LogInformation("file cached");
        }
    }
}
