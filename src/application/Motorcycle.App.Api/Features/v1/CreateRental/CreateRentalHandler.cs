using motorcycle.app.api.Configuration;
using motorcycle.domain.Entities;

namespace Motorcycle.App.Api.Features.v1.CreateRental
{
    public class CreateRentalHandler : Handler<CreateRentalCommand, CreateRentalOutput>, ICreateRentalHandler
    {
        private readonly ICreateRentalRepository _repository;

        public CreateRentalHandler(ILogger<CreateRentalCommand> logger,
            ICreateRentalRepository repository) :
            base(logger, new CreateRentalCommandValidator())
        {
            _repository = repository;
        }
        public override async Task ExecutionAsync(CreateRentalCommand input, CancellationToken cancellation)
        {
            var driverTask = await _repository.GetDriver(input.DriverId, cancellation);
            var planTask = await _repository.GetPlan(input.PlanId, cancellation);
            var vehicleTask = await _repository.GetVehicle(input.VehicleId, cancellation);

            if (!driverTask.CNH.CNHisValid)
                AddValidation("CNH is not valid for this rental");

            var rental = RentVehicle.Create(
                planTask,
                driverTask,
                vehicleTask,
                DateTime.UtcNow);

            _repository.Add(rental);
            _repository.SaveChanges();
        }
    }
}
