using motorcycle.app.api.Configuration;

namespace Motorcycle.App.Api.Features.v1.EditVehiclePlate
{
    public class EditVehiclePlateHandler : Handler<EditVehiclePlateCommand, EditVehiclePlateOutput>, IEditVehiclePlateHandler
    {
        private readonly IEditVehiclePlateRepository _repository;

        public EditVehiclePlateHandler(ILogger<EditVehiclePlateCommand> logger, IEditVehiclePlateRepository repository) :
            base(logger, new EditVehiclePlateCommandValidator())
        {
            _repository = repository;
        }
        public override async Task ExecutionAsync(EditVehiclePlateCommand input, CancellationToken cancellation)
        {
            if (_repository.Find(item => item.RentVehicles.Any(rent => !rent.EndDate.HasValue)).Result.Any())
                AddValidation("This vehicle has rent available, this operation is not valid");

            var vehicle = _repository.Find(item => item.Id == input.Id).Result.First();

            vehicle.ChangePlate(input.Plate);

            await _repository.Update(vehicle);

            await _repository.SaveChanges();
        }
    }
}
