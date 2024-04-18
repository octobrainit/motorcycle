using Microsoft.EntityFrameworkCore;
using motorcycle.domain.Entities;
using motorcycle.shared.CreationalBase;
using Motorcycle.App.Api.Context;
using Motorcycle.App.Api.Context.Base;

namespace Motorcycle.App.Api.Features.v1.CheckRentalValue
{
    public class CheckRentalValueRepository : BaseRepository<RentVehicle, Database>, ICheckRentalValueRepository
    {
        public CheckRentalValueRepository(Database database) : base(database){}

        public async Task<RentVehicle> GetRentVehicleAsync(Guid driverId, DateTime endDate, CancellationToken cancellationToken)
        {

            var rental = await Context.RentVehicles
                        .AsNoTracking()
                        .Include(item => item.Plan)
                        .SingleOrDefaultAsync(item => !item.EndDate.HasValue && item.Driver.Id.Equals(driverId));

            if (endDate < rental.DateToRent)
                throw BaseError.Create(MessageType.InfraestructureValidation, $"Rental date end is invalid, must be greather than {rental.DateToRent.ToShortDateString()}");

            rental.CancelRent(endDate, "user solicitaion");

            return rental ?? throw BaseError.Create(MessageType.InfraestructureValidation, "Rental not found, response is invalid");
        }
    }
}
