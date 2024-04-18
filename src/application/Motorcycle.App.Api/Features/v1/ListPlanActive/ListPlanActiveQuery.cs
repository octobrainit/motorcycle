using MediatR;
using motorcycle.app.api.Configuration;

namespace Motorcycle.App.Api.Features.v1.ListPlanActive
{
    public class ListPlanActiveQuery : IRequest<Response<ListPlanActiveOutput>>
    {
        public bool IsActive { get; set; }
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }

}
