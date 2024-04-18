using FluentValidation;

namespace Motorcycle.App.Api.Features.v1.UploadImageFile
{
    public class UploadImageFileCommandValidator : AbstractValidator<UploadImageFileCommand>
    {
        public UploadImageFileCommandValidator()
        {
            RuleFor(item => item.DriverId).NotNull().NotEqual(Guid.Empty);
            RuleFor(item => item.File).NotNull();
            RuleFor(item => item.File.Length).LessThanOrEqualTo(5 * 1024 * 1024);
            RuleFor(item => item.File.FileName.ToLowerInvariant()).Custom((item, context) =>
            {
                if (item.Contains(".png") || item.Contains(".bmp"))
                    return;
                context.AddFailure("File extension not accepted");
            });
        }
    }
}
