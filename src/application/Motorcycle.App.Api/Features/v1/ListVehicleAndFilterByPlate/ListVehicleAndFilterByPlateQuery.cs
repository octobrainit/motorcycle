using MediatR;
using motorcycle.app.api.Configuration;

namespace Motorcycle.App.Api.Features.v1.ListVehicleAndFilterByPlate
{
    public class ListVehicleAndFilterByPlateQuery : IRequest<Response<ListVehicleAndFilterByPlateOutput>>
    {
        public string Plate { get; set; } = string.Empty;
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
