using MediatR;
using motorcycle.app.api.Configuration;

namespace Motorcycle.App.Api.Features.v1.UploadImageFile
{
    public class UploadImageFileCommand  : IRequest<Response<UploadImageFileOutput>>
    {
        public Guid DriverId { get; set; }
        public IFormFile File { get; set; }
    }
}
