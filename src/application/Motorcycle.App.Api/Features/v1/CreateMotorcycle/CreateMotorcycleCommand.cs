using MediatR;
using motorcycle.app.api.Configuration;

namespace Motorcycle.App.Api.Features.v1.CreateMotorcycle
{
    public class CreateMotorcycleCommand : IRequest<Response<CreateMotorcycleOutput>>
    {
        public int Year { get; set; } = int.MinValue;
        public string Model { get; set; } = string.Empty;
        public string Plate { get; set; } = string.Empty;
    }
}
