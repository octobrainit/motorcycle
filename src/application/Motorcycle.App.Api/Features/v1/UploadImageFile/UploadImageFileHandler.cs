using MediatR;
using motorcycle.app.api.Configuration;
using Motorcycle.App.Api.Cache;

namespace Motorcycle.App.Api.Features.v1.UploadImageFile
{
    public class UploadImageFileHandler : Handler<UploadImageFileCommand, UploadImageFileOutput>, IUploadImageFileHandler
    {
        private readonly IUploadImageFileRepository _repository;
        private readonly IMediator _mediator;

        public UploadImageFileHandler(ILogger<UploadImageFileCommand> logger, IUploadImageFileRepository repository, IMediator mediator) :
            base(logger, new UploadImageFileCommandValidator())
        {
            _repository = repository;
            _mediator = mediator;
        }
        public override async Task ExecutionAsync(UploadImageFileCommand input, CancellationToken cancellation)
        {
            LogInformation("Starting the handler to upload file");

            var driver = await _repository.FindDriverWithCNH(input.DriverId, cancellation);
            var CNH = driver.CNH;
            string newName = string.Concat(CNH.Id.ToString(), ".", input.File.FileName.Split('.')[1]);
            var contentType = GetContentTypeFromFileName(newName);


            using (var memoryStream = new MemoryStream())
            {
                await input.File.CopyToAsync(memoryStream);

                memoryStream.Position = 0;

                var newFile = new FormFile(memoryStream, 0, memoryStream.Length,
                                           newName.Split('.')[0], newName)
                {
                    Headers = new HeaderDictionary(),
                    ContentType = contentType
                };

                await _mediator.Publish(new CacheUploadEvent { File = newFile });
            }

            Data.Data = new();
            Data.StatusCode = 201;
        }

        private string GetContentTypeFromFileName(string fileName)
        {
            if (fileName.EndsWith(".png", StringComparison.OrdinalIgnoreCase))
            {
                return "image/png";
            }
            else if (fileName.EndsWith(".bmp", StringComparison.OrdinalIgnoreCase) || fileName.EndsWith(".bmp", StringComparison.OrdinalIgnoreCase))
            {
                return "image/bmp";
            }
            else
            {
                return "application/octet-stream";
            }
        }
    }
}
