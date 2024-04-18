using motorcycle.app.api.Configuration;
using motorcycle.domain.entities;
using System.Net;

namespace Motorcycle.App.Api.Features.v1.ListVehicleAndFilterByPlate
{
    public class ListVehicleAndFilterByPlateQueryHandler :
        Handler<ListVehicleAndFilterByPlateQuery, ListVehicleAndFilterByPlateOutput>,
        IListVehicleAndFilterByPlateQueryHandler
    {
        private readonly IListVehicleAndFilterByPlateRepository _repository;

        public ListVehicleAndFilterByPlateQueryHandler(
            ILogger<ListVehicleAndFilterByPlateQuery> logger,
            IListVehicleAndFilterByPlateRepository repository) : base(logger, new ListVehicleAndFilterByPlateQueryValidator())
        {
            _repository = repository;
        }
        public override async Task ExecutionAsync(ListVehicleAndFilterByPlateQuery input, CancellationToken cancellation)
        {
            List<Vehicle> data = new();
            Data.Data = new ListVehicleAndFilterByPlateOutput();

            if (string.IsNullOrEmpty(input.Plate))
                data = _repository.Find(item => true, true).Result
                                    .Take(input.PageSize)
                                    .Skip((input.Page - 1) * input.PageSize).ToList();
            else
                data = _repository.Find(item => item.Plate.Equals(input.Plate), true).Result
                                    .Take(input.PageSize)
                                    .Skip((input.Page - 1) * input.PageSize).ToList();


            Data.Data.Vehicles.AddRange(data);
            Data.Data.Page = input.Page;
            Data.Data.PageSize = input.PageSize;
            Data.Data.AmountData = _repository.Find(item => true, true).Result.Count();
            Data.StatusCode = (int)HttpStatusCode.OK;
        }
    }
}
