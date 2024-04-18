using motorcycle.domain.Entities;

namespace Motorcycle.App.Api.Features.v1.ListPlanActive
{
    public class ListPlanActiveOutput
    {
        public List<Plan> Plans { get; set; } = new();
        public int AmountData { get; set; } = 0;
        public int Page { get; set; } = 0;
        public int PageSize { get; set; } = 0;
    }
}
