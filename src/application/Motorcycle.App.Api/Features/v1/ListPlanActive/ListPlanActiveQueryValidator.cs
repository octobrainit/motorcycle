using FluentValidation;

namespace Motorcycle.App.Api.Features.v1.ListPlanActive
{
    public class ListPlanActiveQueryValidator : AbstractValidator<ListPlanActiveQuery>
    {
        public ListPlanActiveQueryValidator()
        {
            RuleFor(item => item.Page).GreaterThan(0);
            RuleFor(item => item.PageSize).GreaterThan(0);
        }
    }
}
