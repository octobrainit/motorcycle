using FluentValidation;

namespace Motorcycle.App.Api.Features.v1.CreatePlan
{
    public class CreatePlanCommandValidator : AbstractValidator<CreatePlanCommand>
    {
        public CreatePlanCommandValidator()
        {
            RuleFor(item => item.Price).GreaterThan(0);
            RuleFor(item => item.DayWithVehicle).GreaterThan(0);
        }
    }
}
