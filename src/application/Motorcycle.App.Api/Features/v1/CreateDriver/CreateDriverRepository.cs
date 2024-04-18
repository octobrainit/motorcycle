using motorcycle.domain.entities;
using Motorcycle.App.Api.Context;
using Motorcycle.App.Api.Context.Base;

namespace Motorcycle.App.Api.Features.v1.CreateDriver
{
    public class CreateDriverRepository : BaseRepository<Driver, Database>, ICreateDriverRepository
    {
        public CreateDriverRepository(Database database) : base(database)
        { }
    }
}
