using MediatR;

namespace Motorcycle.App.Api.Cache
{
    public class CacheUploadEvent : INotification
    {
        public IFormFile File { get; set; }
    }
}
