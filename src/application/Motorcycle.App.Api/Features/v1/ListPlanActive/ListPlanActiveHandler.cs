using motorcycle.app.api.Configuration;
using System.Net;

namespace Motorcycle.App.Api.Features.v1.ListPlanActive
{
    public class ListPlanActiveHandler : Handler<ListPlanActiveQuery, ListPlanActiveOutput>, IListPlanActiveHandler
    {
        private readonly IListPlanActiveRepository _repository;

        public ListPlanActiveHandler(ILogger<ListPlanActiveQuery> logger, IListPlanActiveRepository repository) : 
            base(logger, new ListPlanActiveQueryValidator())
        {
            _repository = repository;
        }
        public override async Task ExecutionAsync(ListPlanActiveQuery input, CancellationToken cancellation)
        {
            Data.Data = new ListPlanActiveOutput();
            Data.Data.Plans = _repository
                                .Find(item => item.IsActive.Equals(input.IsActive), true).Result
                                .Take(input.PageSize)
                                .Skip((input.Page - 1) * input.PageSize)
                                .ToList();
            Data.Data.PageSize = input.PageSize;
            Data.Data.Page = input.Page;
            Data.Data.AmountData = _repository.Find(item => true, true).Result.Count();
            Data.StatusCode = (int)HttpStatusCode.OK;
        }
    }
}
