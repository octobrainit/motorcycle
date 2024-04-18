using motorcycle.domain.entities;

namespace Motorcycle.App.Api.Features.v1.ListVehicleAndFilterByPlate
{
    public class ListVehicleAndFilterByPlateOutput
    {
        public List<Vehicle> Vehicles { get; set; } = new();
        public int AmountData { get; set; } = 0;
        public int Page { get; set; } = 0;
        public int PageSize { get; set; } = 0;
    }
}
