using MediatR;
using motorcycle.app.api.Configuration;

namespace Motorcycle.App.Api.Features.v1.CreateRental
{
    public class CreateRentalCommand : IRequest<Response<CreateRentalOutput>>
    {
        public Guid PlanId { get; set; }
        public Guid DriverId { get; set; }
        public Guid VehicleId { get; set; }
    }
}
