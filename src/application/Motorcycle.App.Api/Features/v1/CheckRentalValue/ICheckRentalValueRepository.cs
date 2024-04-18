using motorcycle.domain.Entities;
using Motorcycle.App.Api.Context.Base;

namespace Motorcycle.App.Api.Features.v1.CheckRentalValue
{
    public interface ICheckRentalValueRepository : IRepository<RentVehicle>
    {
        Task<RentVehicle> GetRentVehicleAsync(Guid driverId, DateTime endDate, CancellationToken cancellationToken);
    }
}
