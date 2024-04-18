using motorcycle.domain.entities;
using motorcycle.domain.Entities;
using Motorcycle.App.Api.Context.Base;

namespace Motorcycle.App.Api.Features.v1.CreateRental
{
    public interface ICreateRentalRepository : IRepository<RentVehicle>
    {
        Task<Driver> GetDriver(Guid id, CancellationToken cancellationToken);
        Task<Plan> GetPlan(Guid id, CancellationToken cancellationToken);
        Task<Vehicle> GetVehicle(Guid id, CancellationToken cancellationToken);
    }
}
