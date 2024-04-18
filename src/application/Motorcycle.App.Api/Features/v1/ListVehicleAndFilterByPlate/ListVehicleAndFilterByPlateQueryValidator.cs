using FluentValidation;
using System.Text.RegularExpressions;

namespace Motorcycle.App.Api.Features.v1.ListVehicleAndFilterByPlate
{
    public class ListVehicleAndFilterByPlateQueryValidator : AbstractValidator<ListVehicleAndFilterByPlateQuery>
    {
        public ListVehicleAndFilterByPlateQueryValidator()
        {
            RuleFor(item => item.Plate).Custom((item, Context) =>
            {
                if (item is not null)
                {
                    string pattern = @"^(?=(.*[A-Za-z]){3})(?=(.*\d){4})[A-Za-z\d]{7}$";
                    bool isMatch = Regex.IsMatch(item, pattern);
                    if (!isMatch)
                    {
                        Context.AddFailure("Plate is not valid");
                    }
                }
            });
            RuleFor(item => item.PageSize).LessThan(50);
        }
    }
}
