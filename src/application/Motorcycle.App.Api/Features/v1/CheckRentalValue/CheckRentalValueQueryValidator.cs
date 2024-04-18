using FluentValidation;

namespace Motorcycle.App.Api.Features.v1.CheckRentalValue
{
    public class CheckRentalValueQueryValidator : AbstractValidator<CheckRentalValueQuery>
    {
        public CheckRentalValueQueryValidator()
        {
            RuleFor(item => item.EndDate.ToDateTime(TimeOnly.MinValue, DateTimeKind.Utc)).LessThan(DateTime.Today.AddDays(51).Date);
            RuleFor(item => item.DriverId).NotEqual(Guid.Empty);
        }
    }
}
