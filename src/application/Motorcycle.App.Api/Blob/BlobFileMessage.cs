using MediatR;

namespace Motorcycle.App.Api.Blob
{
    public class BlobFileMessage : INotification
    {
        public string FileName { get; set; }
    }
}
