using motorcycle.app.api.Configuration;

namespace Motorcycle.App.Api.Features.v1.CheckRentalValue
{
    public class CheckRentalValueHandler : Handler<CheckRentalValueQuery, CheckRentalValueOutput>, ICheckRentalValueHandler
    {
        private readonly ICheckRentalValueRepository _repository;

        public CheckRentalValueHandler(ILogger<CheckRentalValueQuery> logger, ICheckRentalValueRepository repository) :
            base(logger, new CheckRentalValueQueryValidator())
        {
            _repository = repository;
        }
        public override async Task ExecutionAsync(CheckRentalValueQuery input, CancellationToken cancellation)
        {
            var rental = await _repository.GetRentVehicleAsync(input.DriverId, input.EndDate.ToDateTime(TimeOnly.MinValue, DateTimeKind.Utc), cancellation);

            Data.Data = new CheckRentalValueOutput { Value = rental.FinalValue };
            Data.StatusCode = 200;
        }
    }
}
