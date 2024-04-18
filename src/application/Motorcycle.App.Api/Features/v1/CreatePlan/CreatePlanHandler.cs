using motorcycle.app.api.Configuration;
using motorcycle.domain.Entities;
using System.Net;

namespace Motorcycle.App.Api.Features.v1.CreatePlan
{
    public class CreatePlanHandler : Handler<CreatePlanCommand, CreatePlanOutput>, ICreatePlanHandler
    {
        private readonly ICreatePlanRepository _repository;

        public CreatePlanHandler(ILogger<CreatePlanCommand> logger, ICreatePlanRepository repository) :
            base(logger, new CreatePlanCommandValidator())
        {
            _repository = repository;
        }
        public override async Task ExecutionAsync(CreatePlanCommand input, CancellationToken cancellation)
        {
            if (_repository.Find(item => item.DaysWithVehicle.Equals(input.DayWithVehicle) && item.Price.Equals(input.Price)).Result.Any())
                AddValidation("Plan already exists, this operation is invalid");

            await _repository.Add(Plan.Create(input.DayWithVehicle, input.Price));
            await _repository.SaveChanges();
            Data.StatusCode = (int)HttpStatusCode.Created;
        }
    }
}
