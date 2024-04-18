using FluentValidation;
using System.Text.RegularExpressions;

namespace Motorcycle.App.Api.Features.v1.CreateMotorcycle
{
    public class CreateMotorcycleCommandValidator : AbstractValidator<CreateMotorcycleCommand>
    {
        public CreateMotorcycleCommandValidator()
        {
            RuleFor(item => item.Year).LessThan(DateTime.Today.Year + 1);
            RuleFor(item => item.Model).MinimumLength(3);
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
