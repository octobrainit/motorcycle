using FluentValidation;

namespace Motorcycle.App.Api.Features.v1.CreateRental
{
    public class CreateRentalCommandValidator : AbstractValidator<CreateRentalCommand>
    {
        public CreateRentalCommandValidator()
        {
            RuleFor(item => item.VehicleId).NotEmpty().NotEqual(Guid.Empty);
            RuleFor(item => item.DriverId).NotEmpty().NotEqual(Guid.Empty);
            RuleFor(item => item.PlanId).NotEmpty().NotEqual(Guid.Empty);
        }
    }
}
