using FluentValidation;
using System.Text.RegularExpressions;

namespace Motorcycle.App.Api.Features.v1.EditVehiclePlate
{
    public class EditVehiclePlateCommandValidator : AbstractValidator<EditVehiclePlateCommand>
    {
        public EditVehiclePlateCommandValidator()
        {
            RuleFor(item => item.Id).NotNull().NotEqual(Guid.Empty);
            RuleFor(item => item.Plate).Custom((item, Context) =>
            {
                string pattern = @"^(?=(.*[A-Za-z]){3})(?=(.*\d){4})[A-Za-z\d]{7}$";
                bool isMatch = Regex.IsMatch(item, pattern);
                if (!isMatch)
                {
                    Context.AddFailure("Plate is not valid for this vehicle");
                }
            });
        }
    }
}
