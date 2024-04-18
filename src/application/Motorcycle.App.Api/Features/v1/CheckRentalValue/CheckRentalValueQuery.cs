using MediatR;
using motorcycle.app.api.Configuration;

namespace Motorcycle.App.Api.Features.v1.CheckRentalValue
{
    public class CheckRentalValueQuery : IRequest<Response<CheckRentalValueOutput>>
    {
        public Guid DriverId { get; set; }
        public DateOnly EndDate { get; set; }
    }
}
