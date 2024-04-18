using motorcycle.domain.Entities;
using Motorcycle.App.Api.Context;
using Motorcycle.App.Api.Context.Base;

namespace Motorcycle.App.Api.Features.v1.ListPlanActive
{
    public class ListPlanActiveRepository : BaseRepository<Plan, Database>, IListPlanActiveRepository
    {
        public ListPlanActiveRepository(Database database) : base(database) {}
    }
}
