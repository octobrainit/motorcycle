using FluentValidation;
using motorcycle.domain.Enums;

namespace Motorcycle.App.Api.Features.v1.CreateDriver
{
    public class CreateDriverCommandValidator : AbstractValidator<CreateDriverCommand>
    {
        public CreateDriverCommandValidator()
        {
            RuleFor(item => DateTime.Today.Year - item.BirthDate.Year).GreaterThanOrEqualTo(18);
            RuleFor(item => item.CNPJ).GreaterThan(0);
            RuleFor(item => item.CNHNumber).GreaterThan(0);
            RuleFor(item => item.CNHType).Custom((item, Context) =>
            {
                if (!Enum.IsDefined(typeof(DocumentType), item))
                {
                    Context.AddFailure("Document type is not valid for this Driver");
                }
            });
        }
    }
}
