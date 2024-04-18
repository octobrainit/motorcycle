using motorcycle.domain.entities;
using Motorcycle.App.Api.Context;
using Motorcycle.App.Api.Context.Base;

namespace Motorcycle.App.Api.Features.v1.ListVehicleAndFilterByPlate
{
    public class ListVehicleAndFilterByPlateRepository : BaseRepository<Vehicle, Database>, IListVehicleAndFilterByPlateRepository
    {
        public ListVehicleAndFilterByPlateRepository(Database database) : base(database) { }
    }
}
