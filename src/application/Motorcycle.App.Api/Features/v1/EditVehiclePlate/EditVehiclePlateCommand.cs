using MediatR;
using motorcycle.app.api.Configuration;

namespace Motorcycle.App.Api.Features.v1.EditVehiclePlate
{
    public class EditVehiclePlateCommand : IRequest<Response<EditVehiclePlateOutput>>
    {
        public Guid Id { get; set; } = Guid.Empty;
        public string Plate { get; set; } = string.Empty;
    }
}
