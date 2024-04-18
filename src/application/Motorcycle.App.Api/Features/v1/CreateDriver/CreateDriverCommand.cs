using MediatR;
using motorcycle.app.api.Configuration;
using motorcycle.domain.Enums;

namespace Motorcycle.App.Api.Features.v1.CreateDriver
{
    public class CreateDriverCommand : IRequest<Response<CreateDriverOutput>>
    {
        public long CNPJ { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateOnly BirthDate { get; set; }
        public long CNHNumber { get; set; }
        public DocumentType CNHType { get; set; }
    }
}
