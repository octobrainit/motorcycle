using motorcycle.domain.entities;
using Motorcycle.App.Api.Context;
using Motorcycle.App.Api.Context.Base;

namespace Motorcycle.App.Api.Features.v1.EditVehiclePlate
{
    public class EditVehiclePlateRepository : BaseRepository<Vehicle, Database>, IEditVehiclePlateRepository
    {
        public EditVehiclePlateRepository(Database database) : base(database)
        { }
    }
}
