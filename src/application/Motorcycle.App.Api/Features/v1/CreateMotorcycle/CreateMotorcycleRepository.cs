using motorcycle.domain.entities;
using Motorcycle.App.Api.Context;
using Motorcycle.App.Api.Context.Base;

namespace Motorcycle.App.Api.Features.v1.CreateMotorcycle
{
    public class CreateMotorcycleRepository : BaseRepository<Vehicle, Database>, ICreateMotorcycleRepository
    {
        public CreateMotorcycleRepository(Database database) : base(database)
        { }
    }
}
