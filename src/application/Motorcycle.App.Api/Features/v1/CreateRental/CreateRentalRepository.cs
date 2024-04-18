using Microsoft.EntityFrameworkCore;
using motorcycle.domain.entities;
using motorcycle.domain.Entities;
using motorcycle.shared.CreationalBase;
using Motorcycle.App.Api.Context;
using Motorcycle.App.Api.Context.Base;

namespace Motorcycle.App.Api.Features.v1.CreateRental
{
    public class CreateRentalRepository : BaseRepository<RentVehicle, Database>, ICreateRentalRepository
    {
        public CreateRentalRepository(Database database) : base(database) { }

        public async Task<Driver> GetDriver(Guid id, CancellationToken cancellationToken) =>
            await Context.Drivers.Include(item => item.CNH).SingleOrDefaultAsync(item => item.Id.Equals(id)) ??
            throw BaseError.Create(MessageType.ApplicationValidation, "Driver not found");

        public async Task<Plan> GetPlan(Guid id, CancellationToken cancellationToken) =>
            await Context.Plans.SingleOrDefaultAsync(item => item.Id.Equals(id)) ??
            throw BaseError.Create(MessageType.ApplicationValidation, "Plan not found");

        public async Task<Vehicle> GetVehicle(Guid id, CancellationToken cancellationToken) =>
            await Context.Vehicles.SingleOrDefaultAsync(item => item.Id.Equals(id)) ??
            throw BaseError.Create(MessageType.ApplicationValidation, "Vehicle not found");
    }
}
