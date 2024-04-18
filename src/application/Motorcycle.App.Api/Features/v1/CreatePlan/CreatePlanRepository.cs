using motorcycle.domain.Entities;
using Motorcycle.App.Api.Context;
using Motorcycle.App.Api.Context.Base;

namespace Motorcycle.App.Api.Features.v1.CreatePlan
{
    public class CreatePlanRepository : BaseRepository<Plan, Database>, ICreatePlanRepository
    {
        public CreatePlanRepository(Database database) : base(database)
        {
            
        }
    }
}
