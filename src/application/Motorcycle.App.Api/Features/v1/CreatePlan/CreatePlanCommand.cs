using MediatR;
using motorcycle.app.api.Configuration;

namespace Motorcycle.App.Api.Features.v1.CreatePlan
{
    public class CreatePlanCommand : IRequest<Response<CreatePlanOutput>>
    {
        public int DayWithVehicle { get; set; }
        public decimal Price { get; set; } = decimal.Zero;
    }
}
