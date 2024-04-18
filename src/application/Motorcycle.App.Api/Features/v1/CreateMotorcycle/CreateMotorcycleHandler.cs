using motorcycle.app.api.Configuration;
using motorcycle.domain.entities;
using motorcycle.domain.Enums;
using System.Net;

namespace Motorcycle.App.Api.Features.v1.CreateMotorcycle
{
    public class CreateMotorcycleHandler : Handler<CreateMotorcycleCommand, CreateMotorcycleOutput>, ICreateMotorcycleHandler
    {
        private readonly ICreateMotorcycleRepository _repository;

        public CreateMotorcycleHandler(
            ILogger<CreateMotorcycleCommand> logger,
            ICreateMotorcycleRepository repository
            ) : base(logger, new CreateMotorcycleCommandValidator())
        {
            _repository = repository;
        }

        public async override Task ExecutionAsync(CreateMotorcycleCommand input, CancellationToken cancellation)
        {
            Vehicle vehicle = Vehicle.Create(VehicleType.Motorcycle, input.Model, input.Plate, input.Year);
            if (_repository.Find(item => item.Plate == input.Plate).Result.Any())
                AddValidation("Vehicle plate already exists");

            await _repository.Add(vehicle);
            await _repository.SaveChanges();

            Data.StatusCode = (int)HttpStatusCode.Created;
        }
    }
}
