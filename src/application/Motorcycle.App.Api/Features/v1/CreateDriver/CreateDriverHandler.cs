using motorcycle.app.api.Configuration;
using motorcycle.domain.entities;
using motorcycle.domain.ValueObject;

namespace Motorcycle.App.Api.Features.v1.CreateDriver
{
    public class CreateDriverHandler : Handler<CreateDriverCommand, CreateDriverOutput>, ICreateDriverHandler
    {
        private readonly ICreateDriverRepository _repository;

        public CreateDriverHandler(ILogger<CreateDriverCommand> logger, ICreateDriverRepository repository) :
            base(logger, new CreateDriverCommandValidator())
        {
            _repository = repository;
        }

        public override async Task ExecutionAsync(CreateDriverCommand input, CancellationToken cancellation)
        {
            if (_repository.Find(driver => driver.CNPJ.Equals(input.CNPJ) || driver.CNH.DocumentNumber.Equals(input.CNHNumber)).Result.Any())
                AddValidation("Driver already exists, operation not valid");

            var cnh = CNH.Create(input.CNHNumber, input.CNHType);
            var driver = Driver.Create(cnh, input.CNPJ, input.BirthDate.ToDateTime(TimeOnly.MinValue, DateTimeKind.Utc), input.Name);

            await _repository.Add(driver);
            await _repository.SaveChanges();
        }
    }
}
